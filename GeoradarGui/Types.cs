using System;
using System.Collections.Generic;
using System.Linq;

namespace GeoradarGui
{
    /// <summary>
    /// This static class contains auxiliary dictionaries and functions for work with class System.Type.
    /// </summary>
    static class Types
    {
        /// <summary>
        /// Logging using log4net.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public struct Quaternion
        {
            public float q0;
            public float q1;
            public float q2;
            public float q3;
        }

        public struct Vector3d
        {
            public float a;
            public float b;
            public float c;
        }



        public static Quaternion InitQuaternion(Quaternion a)
        {
            a.q0 = 1;
            a.q1 = 0;
            a.q2 = 0;
            a.q3 = 0;
            return (a);
        }

        public static Vector3d InitQuaternion(Vector3d vector3d)
        {
            vector3d.a = 0;
            vector3d.b = 0;
            vector3d.c = 0;
            return (vector3d);
        }


        /// <summary>
        /// This static dictionary is used to find specific Type by text representation.
        /// </summary>
        public static readonly Dictionary<string, Type> dictionary = new Dictionary<string, Type>
        {
            { "INT8"   , typeof(sbyte)      },
            { "UINT8"  , typeof(byte)       },
            { "INT16"  , typeof(Int16)      },
            { "UINT16" , typeof(UInt16)     },
            { "INT32"  , typeof(Int32)      },
            { "UINT32" , typeof(UInt32)     },
            { "INT64"  , typeof(Int64)      },
            { "UINT64" , typeof(UInt64)     },
            { "REAL32" , typeof(float)      },
            { "REAL64" , typeof(double)     },
            { "CHAR"   , typeof(char)       },
            { "BOOL"   , typeof(bool)       },
            { "QUAT"   , typeof(Quaternion) },
            { "VECTOR3D"   , typeof(Vector3d) }

        };


        /// <summary>
        /// This static dictionary is used to find specific byte for read commands by Type.
        /// </summary>
        public static readonly Dictionary<Type, byte> readCommands = new Dictionary<Type, byte>
        {
            { typeof(sbyte) , Command.READ8},
            { typeof(byte)  , Command.READ8},
            { typeof(Int16) , Command.READ16},
            { typeof(UInt16), Command.READ16},
            { typeof(Int32) , Command.READ32},
            { typeof(UInt32), Command.READ32},
            { typeof(Int64) , Command.READ64},
            { typeof(UInt64), Command.READ64},
            { typeof(float) , Command.READ32},
            { typeof(double), Command.READ64},
            { typeof(char)  , Command.READ16},
            { typeof(bool)  , Command.READ8},
            { typeof(Quaternion)  , Command.READQUAT},
            { typeof(Vector3d)    , Command.READVECTOR3D}
        };

        /// <summary>
        /// This static dictionary is used to find specific byte for write commands by Type.
        /// </summary>
        public static readonly Dictionary<Type, byte> writeCommands = new Dictionary<Type, byte>
        {
            { typeof(sbyte) , Command.WRITE8},
            { typeof(byte)  , Command.WRITE8},
            { typeof(Int16) , Command.WRITE16},
            { typeof(UInt16), Command.WRITE16},
            { typeof(Int32) , Command.WRITE32},
            { typeof(UInt32), Command.WRITE32},
            { typeof(Int64) , Command.WRITE64},
            { typeof(UInt64), Command.WRITE64},
            { typeof(float) , Command.WRITE32},
            { typeof(double), Command.WRITE64},
            { typeof(char)  , Command.WRITE16},
            { typeof(bool)  , Command.WRITE8}
        };

        /// <summary>
        /// This static method converts four array of bytes to object.
        /// </summary>
        /// <param name="type">Used to specify type of the return value.</param>
        /// <param name="bytes">Input array of bytes.</param>
        /// <returns>A System.Object that represents the current array of bytes.</returns>
        public static object BytesToObject(Type type, byte[] bytes)
        {
            object obj = null;
            if (bytes != null)
            {
                try
                {
                    if (type == typeof(sbyte))
                        obj = (sbyte)bytes.First();
                    else if (type == typeof(byte))
                        obj = bytes.First();
                    else if (type == typeof(Int16))
                        obj = BitConverter.ToInt16(bytes, 0);
                    else if (type == typeof(UInt16))
                        obj = BitConverter.ToUInt16(bytes, 0);
                    else if (type == typeof(Int32))
                        obj = BitConverter.ToInt32(bytes, 0);
                    else if (type == typeof(UInt32))
                        obj = BitConverter.ToUInt32(bytes, 0);
                    else if (type == typeof(Int64))
                        obj = BitConverter.ToInt64(bytes, 0);
                    else if (type == typeof(UInt64))
                        obj = BitConverter.ToUInt64(bytes, 0);
                    else if (type == typeof(float))
                        obj = BitConverter.ToSingle(bytes, 0);
                    else if (type == typeof(double))
                        obj = BitConverter.ToDouble(bytes, 0);
                    else if (type == typeof(char))
                        obj = BitConverter.ToChar(bytes, 0);
                    else if (type == typeof(bool))
                        obj = BitConverter.ToBoolean(bytes, 0);
                }
                catch (Exception ex)
                {
                    // Type does not match with the number of bytes in the array - invalid response.
                    log.Error(ex);
                }
            }
            return obj;
        }

        /// <summary>
        /// This static method converts object to array of bytes.
        /// </summary>
        /// <param name="type">Used to specify type of the return value.</param>
        /// <param name="obj">Input data.</param>
        /// <returns>A array of bytes that represents the current System.Object.</returns>
        public static byte[] ObjectToBytes(Type type, object obj)
        {
            byte[] bytes = null;
            if (obj != null)
            {
                if (type == typeof(sbyte))
                    bytes = new byte[] { (byte)(sbyte)obj };
                else if (type == typeof(byte))
                    bytes = new byte[] { (byte)obj };
                else if (type == typeof(Int16))
                    bytes = BitConverter.GetBytes((Int16)obj);
                else if (type == typeof(UInt16))
                    bytes = BitConverter.GetBytes((UInt16)obj);
                else if (type == typeof(Int32))
                    bytes = BitConverter.GetBytes((Int32)obj);
                else if (type == typeof(UInt32))
                    bytes = BitConverter.GetBytes((UInt32)obj);
                else if (type == typeof(Int64))
                    bytes = BitConverter.GetBytes((Int64)obj);
                else if (type == typeof(UInt64))
                    bytes = BitConverter.GetBytes((UInt64)obj);
                else if (type == typeof(float))
                    bytes = BitConverter.GetBytes((float)obj);
                else if (type == typeof(double))
                    bytes = BitConverter.GetBytes((double)obj);
                else if (type == typeof(char))
                    bytes = BitConverter.GetBytes((char)obj);
                else if (type == typeof(bool))
                    bytes = BitConverter.GetBytes((bool)obj);
            }
            return bytes;
        }

        /// <summary>
        /// This static method converts string hexadecimal value to object.
        /// </summary>
        /// <param name="type">Used to specify type of the return value.</param>
        /// <param name="value">Input data.</param>
        /// <returns>A System.Object that represents the current string hexadecimal value.</returns>
        public static object HexadecimalStringToObject(Type type, string value)
        {
            object obj = null;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    if (type == typeof(sbyte))
                        obj = (sbyte)Convert.ToSByte(value, 16);
                    else if (type == typeof(byte))
                        obj = (byte)Convert.ToByte(value, 16);
                    else if (type == typeof(Int16))
                        obj = (Int16)Convert.ToInt16(value, 16);
                    else if (type == typeof(UInt16))
                        obj = (UInt16)Convert.ToUInt16(value, 16);
                    else if (type == typeof(Int32))
                        obj = (Int32)Convert.ToInt32(value, 16);
                    else if (type == typeof(UInt32))
                        obj = (UInt32)Convert.ToUInt32(value, 16);
                    else if (type == typeof(Int64))
                        obj = (Int64)Convert.ToInt64(value, 16);
                    else if (type == typeof(UInt64))
                        obj = (UInt64)Convert.ToUInt64(value, 16);
                }
                catch (Exception ex)
                {
                    // String data can not be converted to hexadecimal.
                    log.Error(ex);
                }
            }
            return obj;
        }

    }
}