using PS3Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObrisBaseV3
{
    class Ps3Memory
    {
        public static byte[] BIND = new byte[4];
        public static uint ProcessID;
        public static uint[] processIDs;
        private static string usage;
        public static PS3TMAPI.ConnectStatus connectStatus;
        public static string Status;
        private static PS3API DEX = new PS3API();
        public static void Connect()
        {
            PS3TMAPI.InitTargetComms();
            PS3TMAPI.Connect(0, null);
        }
        public static void AttachProcess()
        {
            PS3TMAPI.GetProcessList(0, out processIDs);
            ulong uProcess = processIDs[0];
            ProcessID = Convert.ToUInt32(uProcess);
            PS3TMAPI.ProcessAttach(0, PS3TMAPI.UnitType.PPU, ProcessID);
            PS3TMAPI.ProcessContinue(0, ProcessID);
        }
        private static byte[] myBuffer = new byte[0x20];
        public static void WriteFloat(uint offset, float input)
        {
            BitConverter.GetBytes(input).CopyTo(myBuffer, 0);
            Array.Reverse(myBuffer, 0, 4);
            Ps3Memory.SetMemory(offset, myBuffer);
        }
        public static float ReadFloat(uint offset)
        {
            Ps3Memory.GetMemory(offset, myBuffer);
            Array.Reverse(myBuffer, 0, 4);
            return BitConverter.ToSingle(myBuffer, 0);
        }

        public static void GetStatus()
        {
            Status = Convert.ToString(PS3TMAPI.GetConnectStatus(0, out connectStatus, out usage));
        }
        public static void SetMemory(uint Address, byte[] Bytes)
        {
            PS3TMAPI.ProcessSetMemory(0, PS3TMAPI.UnitType.PPU, ProcessID, 0, Address, Bytes);
        }
        public static void GetMemory(uint Address, byte[] Bytes)
        {
            PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, ProcessID, 0, Address, ref Bytes);
        }
        public static void GetMemoryIntRead(uint Address, byte[] Bytes, UInt32 Thread)
        {
            PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, ProcessID, Thread, (ulong)Address, ref Bytes);
        }
        public void SetMemoryIntWrite(UInt32 Address, Byte[] Bytes, UInt32 thread)
        {
            PS3TMAPI.ProcessSetMemory(0, PS3TMAPI.UnitType.PPU, ProcessID, thread, Address, Bytes);
        }
        public static void GetMemory1(uint Address, byte[] Bytes)
        {
            PS3TMAPI.ProcessGetMemory(0, PS3TMAPI.UnitType.PPU, ProcessID, 0, Address, ref Bytes);
        }



        public static object CurrentAPI { get; set; }
        public class Extension
        {
            private static SelectAPI CurrentAPI;
            private static byte[] GetBytes(uint offset, int length, SelectAPI API)
            {
                byte[] bytes = new byte[length];
                if (API == SelectAPI.ControlConsole)
                {
                    return DEX.GetBytes(offset, length);
                }
                if (API == SelectAPI.TargetManager)
                {
                    bytes = DEX.GetBytes(offset, length);
                }
                return bytes;
            }
            public static void GetMemoryR(uint Address, ref byte[] Bytes)
            {
                DEX.GetMemory(Address, Bytes);
            }
            private static void GetMem(uint offset, byte[] buffer, SelectAPI API)
            {
                if (API == SelectAPI.ControlConsole)
                {
                    GetMemoryR(offset, ref buffer);
                }
                else if (API == SelectAPI.TargetManager)
                {
                    GetMemoryR(offset, ref buffer);
                }
            }

            public static bool ReadBool(uint offset)
            {
                byte[] buffer = new byte[1];
                GetMem(offset, buffer, CurrentAPI);
                return (buffer[0] != 0);
            }

            public static byte ReadByte(uint offset)
            {
                return GetBytes(offset, 1, CurrentAPI)[0];
            }

            public static byte[] ReadBytes(uint offset, int length)
            {
                return GetBytes(offset, length, CurrentAPI);
            }

            public static float ReadFloat(uint offset)
            {
                byte[] array = GetBytes(offset, 4, CurrentAPI);
                Array.Reverse(array, 0, 4);
                return BitConverter.ToSingle(array, 0);
            }

            public static short ReadInt16(uint offset)
            {
                byte[] array = GetBytes(offset, 2, CurrentAPI);
                Array.Reverse(array, 0, 2);
                return BitConverter.ToInt16(array, 0);
            }

            public static int ReadInt32(uint offset)
            {
                byte[] array = GetBytes(offset, 4, CurrentAPI);
                Array.Reverse(array, 0, 4);
                return BitConverter.ToInt32(array, 0);
            }

            public static long ReadInt64(uint offset)
            {
                byte[] array = GetBytes(offset, 8, CurrentAPI);
                Array.Reverse(array, 0, 8);
                return BitConverter.ToInt64(array, 0);
            }

            public static sbyte ReadSByte(uint offset)
            {
                byte[] buffer = new byte[1];
                GetMem(offset, buffer, CurrentAPI);
                return (sbyte)buffer[0];
            }

            public static string ReadString(uint offset)
            {
                int length = 40;
                int num2 = 0;
                string source = "";
                do
                {
                    byte[] bytes = ReadBytes(offset + ((uint)num2), length);
                    source = source + Encoding.UTF8.GetString(bytes);
                    num2 += length;
                }
                while (!source.Contains<char>('\0'));
                int index = source.IndexOf('\0');
                string str2 = source.Substring(0, index);
                source = string.Empty;
                return str2;
            }

            public static byte[] ReverseArray(float float_0)
            {
                byte[] bytes = BitConverter.GetBytes(float_0);
                Array.Reverse(bytes);
                return bytes;
            }

            public static byte[] uintBytes(uint input)
            {
                byte[] data = BitConverter.GetBytes(input);
                Array.Reverse(data);
                return data;
            }
            public static byte[] ReverseBytes(byte[] inArray)
            {
                Array.Reverse(inArray);
                return inArray;
            }
            public static byte[] ToHexFloat(float Axis)
            {
                byte[] bytes = BitConverter.GetBytes(Axis);
                Array.Reverse(bytes);
                return bytes;
            }

            public static ushort ReadUInt16(uint offset)
            {
                byte[] array = GetBytes(offset, 2, CurrentAPI);
                Array.Reverse(array, 0, 2);
                return BitConverter.ToUInt16(array, 0);
            }

            public static uint ReadUInt32(uint offset)
            {
                byte[] array = GetBytes(offset, 4, CurrentAPI);
                Array.Reverse(array, 0, 4);
                return BitConverter.ToUInt32(array, 0);
            }

            public static ulong ReadUInt64(uint offset)
            {
                byte[] array = GetBytes(offset, 8, CurrentAPI);
                Array.Reverse(array, 0, 8);
                return BitConverter.ToUInt64(array, 0);
            }

            private static void SetMem(uint Address, byte[] buffer, SelectAPI API)
            {
                DEX.SetMemory(Address, buffer);
            }

            public static void WriteBool(uint offset, bool input)
            {
                byte[] buffer = new byte[] { input ? ((byte)1) : ((byte)0) };
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteByte(uint offset, byte input)
            {
                byte[] buffer = new byte[] { input };
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteBytes(uint offset, byte[] input)
            {
                byte[] buffer = input;
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteFloat(uint offset, float input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteInt16(uint offset, short input)
            {
                byte[] array = new byte[2];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 2);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteInt32(uint offset, int input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteInt64(uint offset, long input)
            {
                byte[] array = new byte[8];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 8);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteSByte(uint offset, sbyte input)
            {
                byte[] buffer = new byte[] { (byte)input };
                SetMem(offset, buffer, CurrentAPI);
            }

            public static void WriteString(uint offset, string input)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                Array.Resize<byte>(ref bytes, bytes.Length + 1);
                SetMem(offset, bytes, CurrentAPI);
            }

            public static void WriteUInt16(uint offset, ushort input)
            {
                byte[] array = new byte[2];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 2);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteUInt32(uint offset, uint input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                SetMem(offset, array, CurrentAPI);
            }

            public static void WriteUInt64(uint offset, ulong input)
            {
                byte[] array = new byte[8];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 8);
                SetMem(offset, array, CurrentAPI);
            }
        }
    }
}
