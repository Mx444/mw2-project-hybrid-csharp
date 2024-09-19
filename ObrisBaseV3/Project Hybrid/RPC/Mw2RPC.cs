using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


              public static class MW2RPC
        {
            //Fixed MW2 RPC for 1.14 by Vezah, i know SC58 released an RPC but it didnt return anything so i fixed it :PP
            //This is to use the Functions that seb released !
            public static uint function_address;
 
            public static int Init()
            {
                function_address = Get_func_address();
                if (function_address == 0) return -1;
                Enable_RPC();
                return 0;
            }
 
            public static uint Get_func_address()
            {
                for (uint i = 0x10A24; i < 0x1000000; i += 4)
                {
                    byte[] bytes = ObrisBaseV3.Form1.PS3.Extension.ReadBytes(i, 8);
                    if (((bytes[0] == 0xec) && (bytes[1] == 0x21) && (bytes[2] == 0x00) && (bytes[3] == 0x32) && (bytes[4] == 0x4e) && (bytes[5] == 0x80) && (bytes[6] == 0x00) && (bytes[7] == 0x20)))
                    {
                        return i + 0xC;
                    }
 
                }
                return 0;
            }
 
            public static int CallFunc(uint uint_1, params object[] object_0)
            {
                int length = object_0.Length;
                uint num2 = 0;
                for (uint i = 0; i < length; i++)
                {
                    byte[] buffer;
                    if (object_0[i] is int)
                    {
                        buffer = BitConverter.GetBytes((int)object_0[i]);
                        Array.Reverse(buffer);
                        ObrisBaseV3.Form1.PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer);
                    }
                    else if (object_0[i] is uint)
                    {
                        buffer = BitConverter.GetBytes((uint)object_0[i]);
                        Array.Reverse(buffer);
                        ObrisBaseV3.Form1.PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer);
                    }
                    else if (object_0[i] is string)
                    {
                        byte[] buffer2 = Encoding.UTF8.GetBytes(Convert.ToString(object_0[i]) + "\0");
                        ObrisBaseV3.Form1.PS3.SetMemory(0x10050054 + (i * 0x400), buffer2);
                        uint num4 = 0x10050054 + (i * 0x400);
                        byte[] array = BitConverter.GetBytes(num4);
                        Array.Reverse(array);
                        ObrisBaseV3.Form1.PS3.SetMemory(0x10050000 + ((i + num2) * 4), array);
                    }
                    else if (object_0[i] is float)
                    {
                        num2++;
                        buffer = BitConverter.GetBytes((float)object_0[i]);
                        Array.Reverse(buffer);
                        ObrisBaseV3.Form1.PS3.SetMemory(0x10050024 + ((num2 - 1) * 4), buffer);
                    }
                }
                byte[] bytes = BitConverter.GetBytes(uint_1);
                Array.Reverse(bytes);
                ObrisBaseV3.Form1.PS3.SetMemory(0x1005004c, bytes);
                Thread.Sleep(20);
                byte[] buffer5 = new byte[4];
                ObrisBaseV3.Form1.PS3.GetMemory(0x10050050, buffer5);
                Array.Reverse(buffer5);
                return BitConverter.ToInt32(buffer5, 0);
            }
 
            public static void Enable_RPC()
            {
                ObrisBaseV3.Form1.PS3.SetMemory(function_address, new byte[] { 0x4e, 0x80, 0, 0x20 });
                Thread.Sleep(20);
                byte[] buffer = new byte[] {
                0x7c, 8, 2, 0xa6, 0xf8, 1, 0, 0x80, 60, 0x60, 0x10, 5, 0x81, 0x83, 0, 0x4c,
                0x2c, 12, 0, 0, 0x41, 130, 0, 100, 0x80, 0x83, 0, 4, 0x80, 0xa3, 0, 8,
                0x80, 0xc3, 0, 12, 0x80, 0xe3, 0, 0x10, 0x81, 3, 0, 20, 0x81, 0x23, 0, 0x18,
                0x81, 0x43, 0, 0x1c, 0x81, 0x63, 0, 0x20, 0xc0, 0x23, 0, 0x24, 0xc0, 0x43, 0, 40,
                0xc0, 0x63, 0, 0x2c, 0xc0, 0x83, 0, 0x30, 0xc0, 0xa3, 0, 0x34, 0xc0, 0xc3, 0, 0x38,
                0xc0, 0xe3, 0, 60, 0xc1, 3, 0, 0x40, 0xc1, 0x23, 0, 0x48, 0x80, 0x63, 0, 0,
                0x7d, 0x89, 3, 0xa6, 0x4e, 0x80, 4, 0x21, 60, 0x80, 0x10, 5, 0x38, 160, 0, 0,
                0x90, 0xa4, 0, 0x4c, 0x90, 100, 0, 80, 0xe8, 1, 0, 0x80, 0x7c, 8, 3, 0xa6,
                0x38, 0x21, 0, 0x70, 0x4e, 0x80, 0, 0x20
             };
                ObrisBaseV3.Form1.PS3.SetMemory(function_address + 4, buffer);
                ObrisBaseV3.Form1.PS3.SetMemory(0x10050000, new byte[0x2854]);
                ObrisBaseV3.Form1.PS3.SetMemory(function_address, new byte[] { 0xf8, 0x21, 0xff, 0x91 });
                ObrisBaseV3.Form1.PS3.SetMemory(0x2100000, new byte[0x20]);
                ObrisBaseV3.Form1.PS3.SetMemory(0x2105000, new byte[0x20]);
 
            }
        }
 

 
 