using System;
using System.Linq;

namespace GeoradarGui
{
    /// <summary>
    /// This class provides functionality for communication using byte array via serial line.
    /// </summary>
    class ByteArrayCom : SerialCom
    {
        /// <summary>
        /// Byte array buffer to hold the received data.
        /// </summary>
        byte[] byteBuffer;

        /// <summary>
        /// Number of valid bytes in byteBuffer.
        /// </summary>
        int bytesRead;

        public event StartDataProcessing startDataProcessing;
        public delegate void StartDataProcessing();


        public ByteArrayCom()
        {
            this.Init();
        }

        #region Public Override Methods
        /// <summary>
        /// Sets items to default values.
        /// </summary>
        public override void Init()
        {
            byteBuffer = new byte[serialPort.ReadBufferSize];
            bytesRead = 0;
        }

        /// <summary>
        /// Saves received data to buffer.
        /// </summary>
        public override bool SaveReceivedData()
        {
            try
            {
                if (serialPort.BytesToRead <= (byteBuffer.Length - bytesRead))
                {
                    bytesRead += serialPort.Read(byteBuffer, bytesRead, serialPort.BytesToRead);
                    if (startDataProcessing != null)
                    {
                        startDataProcessing();
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Error("Saving the received data failed.", ex);
            }
            return true;
        }



        /// <summary>
        /// This method is used for processing one graphic answer buffer.
        /// </summary>
        public override ResponseType processGraphDataAnswer(out byte[] parsedData, out byte[] command_set)
        {

            command_set = null;
            //ReceivedData receivedData = new ReceivedData();

            parsedData = null;

            // If buffer is null or empty.
            if ((byteBuffer == null) || (bytesRead == 0))
            {
                return ResponseType.None;
            }

            // Here we have all received data:

            // Looking for HEAD.
            if (byteBuffer.First() != PacketDefinition.HEAD)
            {
                var index = 0;
                for (; index < bytesRead; index++)
                {
                    if (byteBuffer[index] == PacketDefinition.HEAD)
                    {
                        break;
                    }
                }
                ShiftBufferLeft(ref byteBuffer, ref bytesRead, index);
            }

            // If the buffer does not contain valid data.
            if (bytesRead == 0)
            {
                return ResponseType.None;
            }

            // Checking minimal buffer length ( Input packet validation).
            if (bytesRead < (PacketDefinition.HEADER_LENGTH + PacketDefinition.CRC_LENGTH)) // 
            {
                return ResponseType.None;
            }

            // Parsing input packet:
            // 1) Read SIZE.
            UInt16 size = BitConverter.ToUInt16(byteBuffer, 1);

            var answerSize = size + (PacketDefinition.HEAD_LENGH + PacketDefinition.LENGTH_FIELD_LENGHT + PacketDefinition.CRC_LENGTH); // Header + lsbLength + msbLength ... + lsbCRC + msbCRC

            // 2) Check if we received full packet, if not, read port again:
            if (bytesRead < answerSize)
            {
                return ResponseType.None;
            }

            // 3) Read checksum (CRC).
            UInt16 receivedChecksum = BitConverter.ToUInt16(byteBuffer, answerSize - 2);

            // 4) // Copy packets data to buffer for CRC checking:
            var dataBufferForCRCChecking = new byte[answerSize - 2];
            Array.Copy(byteBuffer, 0, dataBufferForCRCChecking, 0, dataBufferForCRCChecking.Length);

            // 5) Delete processed packets from buffer.
            ShiftBufferLeft(ref byteBuffer, ref bytesRead, answerSize);

            // 6) Checking a real CRC of packet:
            var expectedCRC = BitConverter.ToUInt16(bytesU16CalcCRC(dataBufferForCRCChecking), 0);
            
            // 7)If wrong packet, return: 
            if (expectedCRC != receivedChecksum)
            {
                return ResponseType.None;
            }

            // 8) Extract command field:
            command_set = new byte[4];
            command_set[0] = dataBufferForCRCChecking[3];
            command_set[1] = dataBufferForCRCChecking[4];
            command_set[2] = dataBufferForCRCChecking[5];
            command_set[3] = dataBufferForCRCChecking[6];

            // 9) Extract data_load from packet:
            var processedData = new byte[dataBufferForCRCChecking.Length - PacketDefinition.HEADER_LENGTH];
            Array.Copy(dataBufferForCRCChecking, PacketDefinition.HEADER_LENGTH, processedData, 0, processedData.Length);
            parsedData = processedData;

            // 10) return result of processing:
            if (parsedData != null)
            { 
                return ResponseType.ParceOK; 
            }
            return ResponseType.None;
        }


        /// <summary>
        /// This method sends a read request to the serial line.
        /// </summary>
        public override string SendReadingRequest(uint address, Type type)
        {
            return SendRequest(RequestType.Read, ConstructRequest(GetReadingDataForCRC(address, type)));
        }

        public override string SendCommandRequest(UInt16 command, UInt16 parameter, byte[] data_load) 
        {
            byte[] packet_command;
            byte[] packet_parameter;

            // 1) Convert command value and parameter value to byte arrays:
            packet_command = BitConverter.GetBytes((ushort)command);
            packet_parameter = BitConverter.GetBytes((ushort)parameter);

            // 2) Get data_load size, if it is exist al all:
            int data_load_size = 0;
            if (data_load != null)
            {
                data_load_size = data_load.Length;
            }
            // 3) Create suitable buffer for packet load:
            byte[] packet_data_load_buffer = new byte[packet_command.Length + packet_parameter.Length + data_load_size];

            // 4) Copy command, parametr and data to packet load data buffer: 
            packet_data_load_buffer[0] = packet_command[0];
            packet_data_load_buffer[1] = packet_command[1];
            packet_data_load_buffer[2] = packet_parameter[0];
            packet_data_load_buffer[3] = packet_parameter[1];
            if (data_load_size > 0)
            {
                Array.Copy(data_load, 0, packet_data_load_buffer, 4, data_load_size);
            }
            
            //var command_bytes = getCommandDataForRequest(command);

            var request_bytes = ConstructRequest(packet_data_load_buffer);
            return SendRequest(RequestType.Read, request_bytes);
        }



        /// <summary>
        /// This method sends a write request to the serial line.
        /// </summary>
        public override string SendWritingRequest(uint address, byte[] data, Type type)
        {
            return SendRequest(RequestType.Write, ConstructRequest(GetWritingDataForCRC(address, data, type)));
        }
        #endregion

        /// <summary>
        /// This method sends a request to the serial line.
        /// </summary>
        private string SendRequest(RequestType requestType, byte[] byteMessage)
        {
            var message = string.Empty;
            try
            {
                if (byteMessage == null)
                {
                    return message;
                }
                serialPort.Write(byteMessage, 0, byteMessage.Length);

                // Convert byte array to string message:
                Array.ForEach(byteMessage, delegate (byte i) { message += Get8bitAddress(i); });
            }
            catch (Exception ex)
            {
                log.Error("Sending the request failed.", ex);
                return string.Empty;
            }

            var requestString = (requestType == RequestType.Read) ? "READ" : "WRITE";
            return string.Format("Send {0} request: {1}\r\n", requestString, message);
        }

        #region Private Static Methods
        /// <summary>
        /// This static method inserts HEAD and SIZE at the beginning of the byte array and CRC at the end.
        /// </summary>
        private static byte[] ConstructRequest(byte[] array)
        {
            if (array == null)
            {
                return null;
            }
            //1) Create packet buffer:
            //var bytes = new byte[array.Length + 5]; // Header + lsbLength + msbLength ... + lsbCRC + msbCRC
            var bytes = new byte[array.Length + PacketDefinition.HEAD_LENGH + PacketDefinition.LENGTH_FIELD_LENGHT + PacketDefinition.CRC_LENGTH];

            var length_field = BitConverter.GetBytes((UInt16)array.Length);

            //2) Build the command request packet header:
            bytes[0] = PacketDefinition.HEAD;
            bytes[1] = length_field[0];
            bytes[2] = length_field[1];

            //3) Copy the command value to the packet:
            Array.Copy(array, 0, bytes, 3, array.Length);
            
            //4) Add checksum (CRC) to the packet.
            var crc_bytes = bytesU16CalcCRC(bytes);
            bytes[bytes.Length - 2] = crc_bytes[0];
            bytes[bytes.Length - 1] = crc_bytes[1];

            //5) Send ready-to-go packet to Führer:
            return bytes;
        }

        /// <summary>
        /// This method creates string for display in richTextBox from array of bytes.
        /// </summary>
        private static string ToString(byte[] request)
        {
            var text = string.Empty;
            Array.ForEach(request, delegate (byte i) { text += Get8bitAddress(i); });
            return text;
        }

        /// <summary>
        /// This method is used for deleting values ​​from the buffer by shifting left.
        /// </summary>
        private static void ShiftBufferLeft(ref byte[] buffer, ref int bytesRead, int shiftCount)
        {
            Array.Copy(buffer, shiftCount, buffer, 0, buffer.Length - shiftCount);
            bytesRead -= shiftCount;
        }
        #endregion
    }
}