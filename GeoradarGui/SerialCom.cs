using System;
using System.IO.Ports;
using System.Linq;

namespace GeoradarGui
{
    /// <summary>
    /// This enumerator is used to designation of request type.
    /// </summary>
    enum RequestType
    {
        Read,
        Write,
    };

    /// <summary>
    /// This enumerator is used to designation of response type.
    /// </summary>
    enum ResponseType
    {
        None,
        Answer,
        WriteOk,
        Error,
        ParceOK,
    };

    /// <summary>
    /// This struct is used to return received data.
    /// </summary>
    struct ReceivedData
    {
        public uint Address { get; set; }
        public byte[] Data { get; set; }
        public string Text { get; set; }
    }


    /// <summary>
    /// This abstract class is used as a common parent for serial link communication.
    /// </summary>
    abstract class SerialCom
    {
        /// <summary>
        /// Logging using log4net.
        /// </summary>
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Serial port resource.
        /// </summary>
        protected SerialPort serialPort;

        /// <summary>
        /// This object is used for locking the buffer.
        /// </summary>
        private object bufferLock;

        public SerialCom()
        {
            serialPort = new SerialPort();
            bufferLock = new object();

            serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
        }

        public object BufferLock
        {
            get { return bufferLock; }
        }

        public bool IsOpen
        {
            get { return serialPort.IsOpen; }
        }

        public void Open()
        {
            serialPort.Open();
        }

        public void Close()
        {
            serialPort.Close();
        }

        /// <summary>
        /// Sets parameters of serial port.
        /// </summary>
        public void SetParameters(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            serialPort.Parity = parity;
            serialPort.DataBits = dataBits;
            serialPort.StopBits = stopBits;
            serialPort.ReadBufferSize = 8192;
        }

        #region Abstract Methods
        /// <summary>
        /// Sets items to default values.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Saves received data to buffer.
        /// </summary>
        public abstract bool SaveReceivedData();

        /// <summary>
        /// This method is used for processinf graph data ansver.
        /// </summary>
        public abstract ResponseType processGraphDataAnswer(out byte[] receivedData, out byte[] command_set);

        // <summary>
        /// This method sends a read request to the serial line.
        /// </summary>
        public abstract string SendReadingRequest(uint address, Type type);

        // <summary>
        /// This method sends a command request to serial line.
        /// </summary>
        public abstract string SendCommandRequest(UInt16 command, UInt16 parameter, byte[] data_load);

        /// <summary>
        /// This method sends a write request to the serial line.
        /// </summary>
        public abstract string SendWritingRequest(uint address, byte[] data, Type type);
        #endregion

        #region Event Handlers
        /// <summary>
        /// Represents the method that will handle the data received event of a SerialPort object. 
        /// </summary>
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read the data from serialPort and store it appropriately.
            lock (BufferLock)
            {
                if (!SaveReceivedData())
                {
                    log.Warn("The receive buffer overflow.");
                }
            }
        }
        #endregion

        #region Protected Static Methods
        /// <summary>
        /// This method converts byte address to string address (for example: "0xFF " or "0xFF,").
        /// </summary>
        protected static string Get8bitAddress(byte data)
        {
            var format = "0x{0:X2}" + Constants.stringCommandSeparator;
            return string.Format(format, data);
        }

        /// <summary>
        /// Calculates checksum (CRC).
        /// </summary>
        protected static byte CalcCRC(byte[] array)
        {
            byte sum = 0;
            Array.ForEach(array, delegate (byte i) { sum += i; });
            return (byte)(0xFF - sum);
        }


        protected static byte[] bytesU16CalcCRC(byte[] array)
        {
            byte[] bytesCRC;
            UInt16 sum = 0;
            Array.ForEach(array, delegate (byte i) { sum += i; });
            bytesCRC = BitConverter.GetBytes((UInt16)sum);
            return bytesCRC;
        }


        /// <summary>
        /// This method is used for parsing received data.
        /// </summary>
        protected static byte[] parseGraphData(byte[] checkedData, UInt16 checksum, ref ReceivedData receivedData)
        {
            // Checking CRC.
            //var expectedCRC = CalcCRC(checkedData);
            var expectedCRC = BitConverter.ToUInt16(bytesU16CalcCRC(checkedData), 0);

            if (checksum != expectedCRC)
            {
                var message = string.Format("Invalid checksum {0}( expected {1}) for packet: {2}",
                    checksum, expectedCRC, receivedData.Text);

                log.Warn(message);
                return null;
            }

            // Read index data_pointer:
            UInt32 DataPointer = (UInt32)BitConverter.ToUInt64(checkedData, checkedData.Length - 8);
            return checkedData;
        }


        /// <summary>
        /// This method is used for parsing received data.
        /// </summary>
        protected static ResponseType ParseReceivedData(byte[] checkedData, UInt16 checksum, ref ReceivedData receivedData)
        {
            // Checking CRC.
            //var expectedCRC = CalcCRC(checkedData);
            var expectedCRC = BitConverter.ToUInt16(bytesU16CalcCRC(checkedData),0);

            if (checksum != expectedCRC)
            {
                var message = string.Format("Invalid checksum {0}( expected {1}) for packet: {2}",
                    checksum, expectedCRC, receivedData.Text);

                log.Warn(message);
                return ResponseType.None;
            }

            // Read index data_pointer:
            UInt32 DataPointer = (UInt32)BitConverter.ToUInt64(checkedData, checkedData.Length - 8);




            //           // Execute a COMMAND.
            //           byte command = checkedData.First();
            //           if (command == Command.ANSWER)
            //           {
            //               // Determine address.
            //               /* receivedData.Address = BitConverter.ToUInt32(checkedData, 1); */
            //               receivedData.Address = BitConverter.ToUInt16(checkedData, 1);
            //               // Determine value - copy to byte array.
            //               /* receivedData.Data = new byte[checkedData.Length - 5]; */
            //               receivedData.Data = new byte[checkedData.Length - 3];
            //               /* Array.Copy(checkedData, 5, receivedData.Data, 0, receivedData.Data.Length); */
            //               Array.Copy(checkedData, 3, receivedData.Data, 0, receivedData.Data.Length);
            //
            //                return ResponseType.Answer;
            //            }
            //            else if (command == Command.WRITE_OK)
            //            {
            //                return ResponseType.WriteOk;
            //            }
            //            else if (command == Command.ERROR)
            //            {
            //                return ResponseType.Error;
            //            }
            return ResponseType.ParceOK;
        }

        /// <summary>
        /// This method is used for build reading request.
        /// </summary>
        protected static byte[] GetReadingDataForCRC(uint address, Type type)
        {
            byte command = Command.READ;
            if (type != null && Properties.Settings.Default.CommandBySize)
            {
                if (Types.readCommands.ContainsKey(type))
                {
                    command = Types.readCommands[type];
                }
            }

            var bytes = BitConverter.GetBytes((UInt16)address);

            // Array to calculate the checksum (CRC).
            var array = new byte[bytes.Length + 1];

            // Set the prepended value.
            array[0] = command;

            // Copy the old values.
            Array.Copy(bytes, 0, array, 1, bytes.Length);

            return array;
        }


        /// <summary>
        /// This method is used for build reading request.
        /// </summary>
        protected static byte[] getCommandDataForRequest(uint command)
        {
            var bytes = BitConverter.GetBytes((UInt32)command);
            return bytes;
        }

        /// <summary>
        /// This method is used for build writing request.
        /// </summary>
        protected static byte[] GetWritingDataForCRC(uint address, byte[] data, Type type)
        {
            if (data == null)
            {
                return null;
            }

            byte command = Command.WRITE;
            if (type != null && Properties.Settings.Default.CommandBySize)
            {
                if (Types.writeCommands.ContainsKey(type))
                {
                    command = Types.writeCommands[type];
                }
            }

            var addressBytes = BitConverter.GetBytes(((ushort)address));

            // Array to calculate the checksum (CRC).
            var array = new byte[addressBytes.Length + data.Length + 1];

            // Set the prepended value.
            array[0] = command;

            // Copy the old values.
            Array.Copy(addressBytes, 0, array, 1, addressBytes.Length);
            Array.Copy(data, 0, array, addressBytes.Length + 1, data.Length);

            return array;
        }
        #endregion
    }
}