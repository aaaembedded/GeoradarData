using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using ZedGraph;

namespace GeoradarGui
{

    static class PacketDefinition
    {
        public const byte HEAD_LENGH = 0x01;
        public const byte LENGTH_FIELD_LENGHT = 0x02;
        public const byte COMMAND_LENGTH = 0x04;    // command is uint32 or 2x uint16(u16 command, u16 parameter) -
                                                    // if request 0x43 , answer is request + 1 = 0x44
        public const byte HEADER_LENGTH = (HEAD_LENGH) + (LENGTH_FIELD_LENGHT) + (COMMAND_LENGTH);

        public const byte CRC_LENGTH = 0x02;

        public const byte HEAD = 0x73;
        
        // command:
        // ID of Georadar data packet:
        public const UInt16 GEORADAR_DATA_PACKET        = 0x0048;


        public const UInt16 GEORADAR_LOAD_PARAMETERS_REQUEST = 0x0049;
        public const UInt16 GEORADAR_LOAD_PARAMETERS_RESPOND = 0x004A;

        public const UInt16 GEORADAR_SAVE_PARAMETERS_REQUEST = 0x004B;
        public const UInt16 GEORADAR_SAVE_PARAMETERS_RESPOND = 0x004C;

        public const UInt16 GEORADAR_START_REQUEST      = 0x004D;
        public const UInt16 GEORADAR_START_RESPOND      = 0x004E;

        public const UInt16 GEORADAR_STOP_REQUEST       = 0x004F;
        public const UInt16 GEORADAR_STOP_RESPOND       = 0x0050;




        public const UInt32 DEFAULT_FLASH_DATA_DOTS_NUMBER = 1000;
    }

    /// <summary>
    /// This static class defines constants for communication protocol.
    /// </summary>
    static class Command
    {
 //       public const byte HEAD_LENGH = 0x01;
 //       public const byte LENGTH_FIELD_LENGH = 0x02;
 //       public const byte COMMAND_LENGTH = 0x04;    // command is uint32 or 2x uint16(u16 command, u16 parameter) -
 //                                                   // if request 0x43 , answer is request + 1 = 0x44
 //       public const byte HEADER_LENGTH = (HEAD_LENGH) + (LENGTH_FIELD_LENGH) + (COMMAND_LENGTH);
//
//        public const byte CRC_LENGTH = 0x02;
//
//        public const byte HEAD = 0x73;
//        public const byte GET_FLASHED_DATA_REQUEST = 0x43;
//        public const byte GET_FLASHED_DATA_ANSWER = ((GET_FLASHED_DATA_REQUEST) + 1);
//        public const UInt32 DEFAULT_FLASH_DATA_DOTS_NUMBER = 1000;

        public const byte READ = 0x00;
        public const byte READ8 = 0x10;
        public const byte READ16 = 0x20;
        public const byte READ32 = 0x30;
        public const byte READ64 = 0x40;
        public const byte READQUAT = 0x50;
        public const byte READVECTOR3D = 0x60;


        public const byte WRITE = 0x01;
        public const byte WRITE8 = 0x11;
        public const byte WRITE16 = 0x21;
        public const byte WRITE32 = 0x31;
        public const byte WRITE64 = 0x41;

        public const byte ANSWER = 0x02;
        public const byte WRITE_OK = 0x03;
        public const byte ERROR = 0x04;
    }

    #region Definition of attributes
    // Attributes of cards.
    static class CardAttrName
    {
        public static readonly string name = "Name";
    }
    static class CardAttrAddress
    {
        public static readonly string name = "Address";
    }
    static class CardAttrPath
    {
        public static readonly string name = "Path";
    }

    // Attributes of variables.
    static class AttrName
    {
        public static int id = 0;
        public static readonly string name = "Name";
    }
    static class AttrAddress
    {
        public static int id = 1;
        public static readonly string name = "Address";
    }
    static class AttrType
    {
        public static int id = 2;
        public static readonly string name = "Type";
    }
    static class AttrMin
    {
        public static int id = 3;
        public static readonly string name = "Min";
    }
    static class AttrMax
    {
        public static int id = 4;
        public static readonly string name = "Max";
    }
    static class AttrValue
    {
        public static int id = 5;
        public static readonly string name = "Value";
    }
    static class AttrRate
    {
        public static int id = 6;
        public static readonly string name = "Rate [s]";
    }
    static class AttrRefresh
    {
        public static int id = 7;
        public static readonly string name = "Refresh";
    }
    static class AttrGraph
    {
        public static int id = 8;
        public static readonly string name = "Graph";
    }

    static class AttrSave
    {
        public static int id = 9;
        public static readonly string name = "Save";
    }

    static class AttrLoad
    {
        public static int id = 10;
        public static readonly string name = "Load";
    }


    #endregion

    /// <summary>
    /// This class is used to define the constants.
    /// </summary>
    static class Constants
    {
        // Main Form.
        public static readonly Color alternatingRowsColor = Color.AliceBlue;
        public static readonly Color hexadecimalFormatColor = Color.Blue;
        public static readonly Color receivedDataColor = Color.Red;
        public static readonly Color sendDataColor = Color.Blue;
        public const int valueMinWidthColumn = 50;
        public const string valueContextMenuText = "Hexadecimal Display";
        public const string refreshColNameShift = "    ";
        public const string saveColNameShift = "    ";
        public const string loadColNameShift = "    ";
        public const string dockLayoutFileName = "dockLayout.xml";

        // Debugging.
        public const string enableListingButtonText = "Normal &Mode";
        public const string disableListingButtonText = "Limited &Mode";

        // Parsing header file.
        public const string commentBegin = "/*";
        public const string commentEnd = "*/";
        public const string attrSpecialChar = "@";
        public const string attrAllocator = ":";
        public const string attrSeparator = ",";

        // Keyboard shortcuts.
        public const char shortcut_Ctrl_A = (char)01;
        public const char shortcut_Ctrl_C = (char)03;
        public const char shortcut_Ctrl_V = (char)22;

        // Logging to file.
        public const string logFileDirectory = "log";
        public const string logFileExtension = ".log";
        public static string logDateTimeFormat = @"yyyy-MM-dd_HH-mm-ss_";

        public static readonly Dictionary<string, long> logFileSizes = new Dictionary<string, long>
        {
            { "10 kB"  , 10000 },
            { "100 kB" , 100000 },
            { "1 MB"   , 1000000 },
            { "10 MB"  , 10000000 }
        };

        public static readonly Dictionary<string, string> logFileSeparators = new Dictionary<string, string>
        {
            { "semicolon" , ";" },
            { "comma"     , "," },
            { "space"     , " " },
            { "tabulator" , "\t" }
        };

        // Serial line.
        public static readonly string[] comboBoxItemsBaudRate = { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
        public static readonly string[] comboBoxItemsParity = Enum.GetNames(typeof(Parity));
        public static readonly string[] comboBoxItemsDataBits = { "5", "6", "7", "8" };
        // The SerialPort class throws an ArgumentOutOfRangeException exception when you set the StopBits property to "None"
        // and it throws an exception "The parameter is incorrect" when you set the StopBits property to "OnePointFive" after call function Open().
        public static readonly string[] comboBoxItemsStopBits = { StopBits.One.ToString(), StopBits.Two.ToString() };

        // Transmission with string.
        public static readonly char stringCommandSeparator = ' ';

        // Rate precision (minimal value) in seconds (must be different from zero).
        public const double ratePrecision = 0.1D;
        public const int maxRatePrecisionDigits = 2;
        public static readonly double[] defaultRates = { ratePrecision, 5 * ratePrecision, 10 * ratePrecision, 50 * ratePrecision };
        public const double rateMaxLimit = 3600.0D;

        // Interval of timer in seconds (must be different from zero).
        public const double graphSampleRate = 0.1D;
        public const double readDataRate = 0.1D;

        // Graphs.
        public const string graphCreateMode = "Create Graph";
        public const string graphEditMode = "Graph Properties";
        public const string graphStripMenuItemID = "properties";
        public const int graphInitialVerticalOffset = 30;

        // Graph Creator.
        public const string graphCreatorEditModeLabel = "Select Curve:";
        public const string graphCreatorCreateModeLabel = "Append to Graph:";
        public const string graphCreatorEditModeButton = "&OK";
        public const string graphCreatorCreateModeButton = "C&reate";
        public const string graphCreatorAppendModeButton = "&Append";

        // Curve data.
        public const SymbolType curveSymbolType = SymbolType.None;
        public const float curveLineWidth = 1.0F;
        public const int curveListCapacity = (int)(Constants.graphMaxScaleValue / Constants.graphSampleRate);
        public static readonly Color curveLineColor = Color.Blue;

        // Graph data.
        public const string graphXAxisTitle = "Time [s]";

        public const double graphMinScaleValue = 0.0D;
        public const double graphMaxScaleValue = 60.0D;
        public const double graphScaleMinorStep = 2.0D;
        public const double graphScaleMajorStep = 10.0D;

        public const double graphRollingBeforeEndRatio = 0.1D;
        public const decimal graphRollingAfterStart = 1.36m;
    }
}