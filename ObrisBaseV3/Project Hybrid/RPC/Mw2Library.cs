
    using PS3Lib;
    using System;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    internal class Mw2Library
    {
        public static uint Index = 0x3700;
        public static PS3API PS3 = new PS3API(SelectAPI.TargetManager);
        private Random rand = new Random();

        public static string GetMw2ClientsNames(int Client)
        {
            byte[] buffer = new byte[0x10];
            PS3.GetMemory(Offsets.Clients.NameInGame + ((uint) (Index * Client)), buffer);
            return Encoding.ASCII.GetString(buffer).Replace("\0", "");
        }

        public class Buttons
        {
            public static bool ButtonPressed(int client, string Button)
            {
                return ((Mw2Library.PS3.Extension.ReadString((uint) (0x34750e9f + (client * 0x97f80))) == Button) || (Mw2Library.PS3.Extension.ReadString((uint) (0x174aa3c + (client * 0x97f80))) == Button));
            }

            public static class Buttonz
            {
                public static string Circle = "+stance";
                public static string Cross = "+gostand";
                public static string DpadDown = "+actionslot 2";
                public static string DpadLeft = "+actionslot 3";
                public static string DpadRight = "+actionslot 4";
                public static string DpadUp = "+actionslot 1";
                public static string L1 = "+speed_throw";
                public static string L2 = "+smoke";
                public static string L3 = "+breath_sprint";
                public static string R1 = "+attack";
                public static string R2 = "+frag";
                public static string R3 = "+melee";
                public static string Select = "togglescores";
                public static string Square = "+usereload";
                public static string Start = "togglemenu";
                public static string Triangle = "weapnext";
            }
        }

        public static class DataGridView
        {
            public static string getAliveClients(int client)
            {
                byte[] bytes = new byte[1];
                GetMemoryR(Mw2Library.Offsets.Clients.Alive + ((uint) (client * 0x3700)), ref bytes);
                byte[] buffer2 = new byte[1];
                bool flag = new byte[1].SequenceEqual<byte>(bytes);
                bool flag2 = new byte[] { 1 }.SequenceEqual<byte>(bytes);
                if (flag)
                {
                    return "Alive";
                }
                if (flag2)
                {
                    return "Death";
                }
                return "Connecting";
            }

            public static string getClientsNames(int Client)
            {
                byte[] buffer = new byte[0x10];
                Mw2Library.PS3.GetMemory(Mw2Library.Offsets.Clients.NameInGame + ((uint) (0x3700 * Client)), buffer);
                return Encoding.ASCII.GetString(buffer).Replace("\0", "");
            }

            public static int getDeathsClients(int client)
            {
                byte[] bytes = new byte[4];
                GetMemoryR(Mw2Library.Offsets.Clients.Status.Deaths + ((uint) (client * 0x3700)), ref bytes);
                return BitConverter.ToInt32(ReverseBytes(bytes), 0);
            }

            public static int getKillsClients(int client)
            {
                byte[] bytes = new byte[4];
                GetMemoryR(Mw2Library.Offsets.Clients.Status.Kills + ((uint) (client * 0x3700)), ref bytes);
                return BitConverter.ToInt32(ReverseBytes(bytes), 0);
            }

            private static void GetMemoryR(uint Address, ref byte[] Bytes)
            {
                Mw2Library.PS3.GetMemory(Address, Bytes);
            }

            public static int getPrestigeClients(int client)
            {
                byte[] bytes = new byte[2];
                GetMemoryR(Mw2Library.Offsets.Clients.Status.Prestige + ((uint) (client * 0x3700)), ref bytes);
                return BitConverter.ToInt32(ReverseBytes(bytes), 0);
            }

            public static int getScoreClients(int client)
            {
                byte[] bytes = new byte[4];
                GetMemoryR(Mw2Library.Offsets.Clients.Status.Score + ((uint) (client * 0x3700)), ref bytes);
                return BitConverter.ToInt16(ReverseBytes(bytes), 0);
            }

            public static string getTeamClients(int client)
            {
                byte[] bytes = new byte[1];
                GetMemoryR(Mw2Library.Offsets.Clients.Team.ChangeTeam + ((uint) (client * 0x3700)), ref bytes);
                bool flag = new byte[1].SequenceEqual<byte>(bytes);
                bool flag2 = new byte[] { 1 }.SequenceEqual<byte>(bytes);
                bool flag3 = new byte[] { 2 }.SequenceEqual<byte>(bytes);
                bool flag4 = new byte[] { 3 }.SequenceEqual<byte>(bytes);
                bool flag5 = new byte[] { 8 }.SequenceEqual<byte>(bytes);
                bool flag6 = new byte[1].SequenceEqual<byte>(bytes);
                if (flag3)
                {
                    return "Friendly";
                }
                if (flag2)
                {
                    return "Enemy";
                }
                if (flag5)
                {
                    return "God Mode Team";
                }
                if (!flag4 && flag)
                {
                    return "Free for all";
                }
                return "Spectator";
            }

            public static int getXPClients(int client)
            {
                byte[] bytes = new byte[4];
                GetMemoryR(Mw2Library.Offsets.Clients.Status.XP + ((uint) (client * 0x3700)), ref bytes);
                return BitConverter.ToInt32(ReverseBytes(bytes), 0);
            }

            private static byte[] ReverseBytes(byte[] array)
            {
                Array.Reverse(array);
                return array;
            }
        }

        public class Debug
        {
            private static uint function_address = 0x10a30;
            public static PS3API PS3 = new PS3API(SelectAPI.TargetManager);

            public static int CallFunction(uint uint_1, params object[] object_0)
            {
                int length = object_0.Length;
                uint num2 = 0;
                for (uint i = 0; i < length; i++)
                {
                    byte[] buffer;
                    if (object_0[i] is int)
                    {
                        buffer = BitConverter.GetBytes((int) object_0[i]);
                        Array.Reverse(buffer);
                        PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer);
                    }
                    else if (object_0[i] is uint)
                    {
                        buffer = BitConverter.GetBytes((uint) object_0[i]);
                        Array.Reverse(buffer);
                        PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer);
                    }
                    else if (object_0[i] is string)
                    {
                        byte[] buffer2 = Encoding.UTF8.GetBytes(Convert.ToString(object_0[i]) + "\0");
                        PS3.SetMemory(0x10050054 + (i * 0x400), buffer2);
                        uint num4 = 0x10050054 + (i * 0x400);
                        byte[] array = BitConverter.GetBytes(num4);
                        Array.Reverse(array);
                        PS3.SetMemory(0x10050000 + ((i + num2) * 4), array);
                    }
                    else if (object_0[i] is float)
                    {
                        num2++;
                        buffer = BitConverter.GetBytes((float) object_0[i]);
                        Array.Reverse(buffer);
                        PS3.SetMemory(0x10050024 + ((num2 - 1) * 4), buffer);
                    }
                }
                byte[] bytes = BitConverter.GetBytes(uint_1);
                Array.Reverse(bytes);
                PS3.SetMemory(0x1005004c, bytes);
                Thread.Sleep(20);
                byte[] buffer5 = new byte[4];
                PS3.GetMemory(0x10050050, buffer5);
                Array.Reverse(buffer5);
                return BitConverter.ToInt32(buffer5, 0);
            }

            public static void Enable_RPC()
            {
                PS3.SetMemory(function_address, new byte[] { 0x4e, 0x80, 0, 0x20 });
                Thread.Sleep(20);
                byte[] buffer = new byte[] { 
                    0x7c, 8, 2, 0xa6, 0xf8, 1, 0, 0x80, 60, 0x60, 0x10, 5, 0x81, 0x83, 0, 0x4c, 
                    0x2c, 12, 0, 0, 0x41, 130, 0, 100, 0x80, 0x83, 0, 4, 0x80, 0xa3, 0, 8, 
                    0x80, 0xc3, 0, 12, 0x80, 0xe3, 0, 0x10, 0x81, 3, 0, 20, 0x81, 0x23, 0, 0x18, 
                    0x81, 0x43, 0, 0x1c, 0x81, 0x63, 0, 0x20, 0xc0, 0x23, 0, 0x24, 0xc0, 0x43, 0, 40, 
                    0xc0, 0x63, 0, 0x2c, 0xc0, 0x83, 0, 0x30, 0xc0, 0xa3, 0, 0x34, 0xc0, 0xc3, 0, 0x38, 
                    0xc0, 0xe3, 0, 60, 0xc1, 3, 0, 0x40, 0xc1, 0x23, 0, 0x48, 0x80, 0x63, 0, 0, 
                    0x7d, 0x89, 3, 0xa6, 0x4e, 0x80, 4, 0x21, 60, 0x80, 0x10, 5, 0x38, 160, 0, 0, 
                    0x90, 0xa4, 0, 0x4c, 0x80, 100, 0, 80, 0xe8, 1, 0, 0x80, 0x7c, 8, 3, 0xa6, 
                    0x38, 0x21, 0, 0x70, 0x4e, 0x80, 0, 0x20, 0x60, 0, 0, 0, 0, 0, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                    0, 0, 0, 0, 0, 0, 0, 0, 0x60, 0, 0, 0
                 };
                PS3.SetMemory(function_address + 4, buffer);
                PS3.SetMemory(0x10050000, new byte[0x2854]);
                PS3.SetMemory(function_address, new byte[] { 0xf8, 0x21, 0xff, 0x91 });
            }

            public static byte[] GetBytes(uint address, int length)
            {
                return PS3.Extension.ReadBytes(address, length);
            }

            public static void GetMemory(uint addr, byte[] buffer)
            {
                PS3.GetMemory(addr, buffer);
            }

            public static void Init()
            {
                PS3.ConnectTarget(0);
                PS3.AttachProcess();
                PS3.Extension.WriteInt32(0x10030000, 0x724c38);
                PS3.Extension.WriteInt32(0x10030004, 0x734be8);
                PS3.Extension.WriteBool(0x1d0ce6c, false);
            }

            public static byte ReadByte(uint address)
            {
                return PS3.GetBytes(address, 1)[0];
            }

            public static float ReadFloat(uint address)
            {
                return PS3.Extension.ReadFloat(address);
            }

            public static int ReadInt32(uint address)
            {
                return PS3.Extension.ReadInt32(address);
            }

            public static float ReadSingle(uint address)
            {
                byte[] bytes = PS3.GetBytes(address, 4);
                Array.Reverse(bytes, 0, 4);
                return BitConverter.ToSingle(bytes, 0);
            }

            public static float[] ReadSingle(uint address, int length)
            {
                byte[] bytes = PS3.GetBytes(address, length * 4);
                ReverseBytes(bytes);
                float[] numArray = new float[length];
                for (int i = 0; i < length; i++)
                {
                    numArray[i] = BitConverter.ToSingle(bytes, ((length - 1) - i) * 4);
                }
                return numArray;
            }

            public static uint ReadUInt32(uint address)
            {
                return PS3.Extension.ReadUInt32(address);
            }

            public static void Reconnect()
            {
                PS3TMAPI.InitTargetComms();
                PS3TMAPI.Connect(0, null);
            }

            public static byte[] ReverseBytes(byte[] array)
            {
                Array.Reverse(array);
                return array;
            }

            public static void SetMemory(uint addr, byte[] buffer)
            {
                PS3.SetMemory(addr, buffer);
            }

            public static void Stop()
            {
                PS3.DisconnectTarget();
            }

            public static void WriteByte(uint address, byte byVal)
            {
                PS3.Extension.WriteByte(address, byVal);
            }

            public static void WriteBytes(uint address, byte[] btes)
            {
                PS3.Extension.WriteBytes(address, btes);
            }

            public static void WriteFloat(uint address, float byVal)
            {
                PS3.Extension.WriteFloat(address, byVal);
            }

            public static void WriteInt32(uint address, int byVal)
            {
                PS3.Extension.WriteInt32(address, byVal);
            }

            public static void WriteSingle(uint address, float input)
            {
                byte[] array = new byte[4];
                BitConverter.GetBytes(input).CopyTo(array, 0);
                Array.Reverse(array, 0, 4);
                PS3.SetMemory(address, array);
            }

            public static void WriteSingle(uint address, float[] input)
            {
                int length = input.Length;
                byte[] array = new byte[length * 4];
                for (int i = 0; i < length; i++)
                {
                    ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (int) (i * 4));
                }
                PS3.SetMemory(address, array);
            }

            public static void WriteUInt32(uint address, uint byVal)
            {
                PS3.Extension.WriteUInt32(address, byVal);
            }
        }

        public static class HuDElements
        {
            public static string[] ActiveClients = new string[GetNumPlayers()];
            private static PS3API DEX = new PS3API(SelectAPI.TargetManager);
            public static string HostName;
            public static int HostNumber;

            public static byte[] ArrayReverse(byte[] byte_43)
            {
                Array.Reverse(byte_43);
                return byte_43;
            }

            public static void ChangeText(uint elem, string Txt)
            {
                uint byVal = G_LocalizedString(Txt);
                Mw2Library.Debug.WriteUInt32(elem + HudStruct.textOffset, byVal);
            }

            public static void DestroyAll()
            {
                for (uint i = 0; i < 100; i++)
                {
                    DestroyElem(i);
                }
            }

            public static void DestroyElem(uint elem)
            {
                Mw2Library.PS3.SetMemory(0x12e9858 + (elem * 180), new byte[180]);
            }

            public static void doType(uint client, uint elem, int r, int g, int b, int a, int r1, int g1, int b1, int a1)
            {
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
                Mw2Library.PS3.Extension.WriteInt32(elem, 0);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, client);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.GlowColor, RGB2INT(r1, g1, b1, a1));
                Mw2Library.Debug.SetMemory(elem + HElems.fxBirthTime, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(GetLevelTime()))));
                Mw2Library.Debug.SetMemory(elem + HElems.fxLetterTime, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(70))));
                Mw2Library.Debug.SetMemory(elem + HElems.fxDecayStartTime, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(0xfa0))));
                Mw2Library.Debug.SetMemory(elem + HElems.fxDecayDuration, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(0x3e8))));
            }

            public static void FadeOverTime(uint Elem, int Time, byte R = 0, byte G = 0, byte B = 0, byte A = 0)
            {
                byte[] buffer = new byte[4];
                Mw2Library.Debug.GetMemory(Elem + HudStruct.colorOffset, buffer);
                Mw2Library.Debug.SetMemory(Elem + HElems.fromColor, buffer);
                Mw2Library.Debug.SetMemory(Elem + HudStruct.colorOffset, new byte[] { R, G, B, A });
                Mw2Library.Debug.WriteInt32(Elem + HElems.fadeTime, (Time * 0x3e8) + ((int) 0.5));
                Mw2Library.Debug.WriteInt32(Elem + HElems.fadeStartTime, GetLevelTime());
            }

            public static void FadeTo(uint Elem, int Time, int R, int G, int B, int A)
            {
                byte[] buffer = new byte[4];
                Mw2Library.Debug.GetMemory(Elem + HudStruct.colorOffset, buffer);
                Mw2Library.Debug.SetMemory(Elem + HElems.fromColor, buffer);
                Mw2Library.Debug.SetMemory(Elem + HudStruct.colorOffset, RGBA(R, G, B, A));
                Mw2Library.Debug.WriteInt32(Elem + HElems.fadeTime, (Time * 500) + ((int) 0.5));
                Mw2Library.Debug.WriteInt32(Elem + HElems.fadeStartTime, GetLevelTime());
            }

            public static uint findPS(int client)
            {
                return (uint) (0x14e2230 + (client * 0x3700));
            }

            public static void fontScaleOverTime(uint elem, int Time, float toFont)
            {
                Mw2Library.Debug.WriteFloat(elem + HElems.fromFontScale, Mw2Library.Debug.ReadFloat(elem + HElems.fontScale));
                Mw2Library.Debug.WriteFloat(elem + HElems.fontScale, toFont);
                Mw2Library.Debug.WriteInt32(elem + HElems.fontScaleTime, (Time * 300) + ((int) 0.5));
                Mw2Library.Debug.WriteInt32(elem + HElems.fontScaleStartTime, GetLevelTime());
            }

            public static uint G_Client(int client)
            {
                return (uint) (0x14e2200 + (client * 0x3700));
            }

            public static uint G_LocalizedString(string input)
            {
                uint num = 0;
                bool flag = true;
                Mw2Library.RPC.WritePowerPc(true);
                Mw2Library.PS3.Extension.WriteString(0x10050010, input);
                Mw2Library.PS3.Extension.WriteBool(0x10050003, true);
                do
                {
                    num = Mw2Library.PS3.Extension.ReadUInt32(0x10050004);
                }
                while (num == 0);
                Mw2Library.PS3.Extension.WriteUInt32(0x10050004, 0);
                do
                {
                    flag = Mw2Library.PS3.Extension.ReadBool(0x10050003);
                }
                while (flag);
                Mw2Library.RPC.WritePowerPc(false);
                return num;
            }

            public static string getClientName(int clientIndex)
            {
                string str = Mw2Library.Debug.PS3.Extension.ReadString((uint) (0x14e5408 + (clientIndex * 0x3700)));
                if (str != string.Empty)
                {
                    return str;
                }
                return "Not Connected";
            }

            public static string getGameMode()
            {
                switch (ReturnInfos(2))
                {
                    case "war":
                        return "Team Deathmatch";

                    case "dm":
                        return "Free for All";

                    case "sd":
                        return "Search and Destroy";

                    case "dom":
                        return "Domination";

                    case "dem":
                        return "Demolition";
                }
                return "Unknown Gametype";
            }

            public static string getHardcore()
            {
                switch (ReturnInfos(4))
                {
                    case "0":
                        return "Hardcore - Off";

                    case "1":
                        return "Hardcore - On";
                }
                return "Unknown Gametype";
            }

            public static string getHostName()
            {
                return ReturnInfos(0x10);
            }

            public static int GetHostNumber(string name)
            {
                if (Mw2Library.PS3.Extension.ReadString(0x14e5408) != name)
                {
                    if (Mw2Library.PS3.Extension.ReadString(0x14e8b08) == name)
                    {
                        return 1;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x14ec208) == name)
                    {
                        return 2;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x14ef908) == name)
                    {
                        return 3;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x14f3008) == name)
                    {
                        return 4;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x14f6708) == name)
                    {
                        return 5;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x14f9e08) == name)
                    {
                        return 6;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x14fd508) == name)
                    {
                        return 7;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x1500c08) == name)
                    {
                        return 8;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x1504308) == name)
                    {
                        return 9;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x1507a08) == name)
                    {
                        return 10;
                    }
                    if (Mw2Library.PS3.Extension.ReadString(0x150b108) == name)
                    {
                        return 11;
                    }
                }
                return 0;
            }

            public static int GetLevelTime()
            {
                byte[] buffer = new byte[4];
                Mw2Library.Debug.GetMemory(0x12e0304, buffer);
                return BitConverter.ToInt32(ArrayReverse(buffer), 0);
            }

            public static string getMapName()
            {
                switch (ReturnInfos(6))
                {
                    case "mp_rust":
                        return "Rust";

                    case "mp_terminal":
                        return "Terminal";

                    case "mp_afghan":
                        return "Afghan";

                    case "mp_estate":
                        return "Estate";

                    case "mp_favela":
                        return "Favela";

                    case "mp_highrise":
                        return "Highrise";

                    case "mp_invasion":
                        return "Invasion";

                    case "mp_checkpoint":
                        return "Karachi";

                    case "mp_quarry":
                        return "Quarry";

                    case "mp_rundown":
                        return "Rundown";

                    case "mp_boneyard":
                        return "Scrapyard";

                    case "mp_nightshift":
                        return "Skidrow";

                    case "mp_subbase":
                        return "Sub Base";

                    case "mp_underpass":
                        return "Underpass";

                    case "mp_brecourt":
                        return "Wasteland";

                    case "mp_complex":
                        return "Bailout";

                    case "mp_crash":
                        return "Crash";
                }
                return "Unknown Map";
            }

            public static int GetNumPlayers()
            {
                int num = 0;
                for (int i = 0; i < 12; i++)
                {
                    if (Mw2Library.Debug.ReadUInt32(G_Client(i)) != 0)
                    {
                        num++;
                    }
                }
                return num;
            }

            public static uint HudElemAlloc(bool Reset = false)
            {
                if (Reset)
                {
                    HudAlloc.IndexSlot = 50;
                }
                uint num = HudAlloc.g_hudelem + (HudAlloc.IndexSlot * 180);
                HudAlloc.IndexSlot++;
                return num;
            }

            public static void iPrintln(int client, string message)
            {
                SV_SendServerCommand(client, "c \"" + message);
            }

            public static void iPrintlnBold(int client, string message)
            {
                SV_SendServerCommand(client, "f \"" + message);
            }

            public static void JetPack(int client)
            {
                float byVal = Mw2Library.Debug.ReadFloat(findPS(client)) + 90f;
                Mw2Library.Debug.WriteFloat(findPS(client), byVal);
            }

            public static void Localize(int client)
            {
                SetClientDvars(client, "loc_warnings 0");
                SetClientDvars(client, "loc_warningsAsErrors 0");
            }

            public static void MoveOverTime(uint elem, int time, float x, float y)
            {
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.fromAlignOrg, Mw2Library.Debug.PS3.Extension.ReadInt32(elem + HElems.alignOrg));
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.fromAlignScreen, Mw2Library.Debug.PS3.Extension.ReadInt32(elem + HElems.alignScreen));
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HElems.fromY, Mw2Library.Debug.PS3.Extension.ReadFloat(elem + HudStruct.yOffset));
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HElems.fromX, Mw2Library.Debug.PS3.Extension.ReadFloat(elem + HudStruct.xOffset));
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.moveStartTime, GetLevelTime());
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.moveTime, (time * 0x3e8) + ((int) 0.5));
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
            }

            public static void MoveScroller(uint elem, int time, float x, float y)
            {
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.fromAlignOrg, Mw2Library.Debug.PS3.Extension.ReadInt32(elem + HElems.alignOrg));
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.fromAlignScreen, Mw2Library.Debug.PS3.Extension.ReadInt32(elem + HElems.alignScreen));
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HElems.fromY, Mw2Library.Debug.PS3.Extension.ReadFloat(elem + HudStruct.yOffset));
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HElems.fromX, Mw2Library.Debug.PS3.Extension.ReadFloat(elem + HudStruct.xOffset));
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.moveStartTime, GetLevelTime());
                Mw2Library.Debug.PS3.Extension.WriteInt32(elem + HElems.moveTime, (time * 250) + ((int) 0.5));
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
                Mw2Library.Debug.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
            }

            public static void onPlayerSpawned(int c)
            {
                if (Mw2Library.Debug.PS3.Extension.ReadByte(G_Client(c) + 0x3193) == 1)
                {
                    Thread.Sleep(0x7d0);
                    iPrintln(c, "^1Welcome ^3" + getClientName(c) + " ^1to the lobby");
                }
            }

            private static string ReturnInfos(int Index)
            {
                return Encoding.ASCII.GetString(Mw2Library.Debug.GetBytes(0x17a54e0, 0x234)).Replace(@"\", "|").Split(new char[] { '|' })[Index];
            }

            public static int RGB2INT(int r, int g, int b, int a)
            {
                byte[] array = new byte[] { (byte) r, (byte) g, (byte) b, (byte) a };
                Array.Reverse(array);
                return BitConverter.ToInt32(array, 0);
            }

            public static byte[] RGBA(decimal R, decimal G, decimal B, decimal A)
            {
                byte[] buffer = new byte[4];
                byte[] bytes = BitConverter.GetBytes(Convert.ToInt32(R));
                byte[] buffer3 = BitConverter.GetBytes(Convert.ToInt32(G));
                byte[] buffer4 = BitConverter.GetBytes(Convert.ToInt32(B));
                byte[] buffer5 = BitConverter.GetBytes(Convert.ToInt32(A));
                buffer[0] = bytes[0];
                buffer[1] = buffer3[0];
                buffer[2] = buffer4[0];
                buffer[3] = buffer5[0];
                return buffer;
            }

            public static void ScaleOverTime(uint elem, int Time, int Width2, int Height2)
            {
                Mw2Library.Debug.WriteInt32(elem + HElems.fromWidth, Mw2Library.Debug.ReadInt32(elem + HudStruct.widthOffset));
                Mw2Library.Debug.WriteInt32(elem + HElems.fromHeight, Mw2Library.Debug.ReadInt32(elem + HudStruct.heightOffset));
                Mw2Library.Debug.WriteInt32(elem + HElems.scaleStartTime, GetLevelTime());
                Mw2Library.Debug.WriteInt32(elem + HElems.scaleTime, (Time * 500) + ((int) 0.5));
                Mw2Library.Debug.WriteInt32(elem + HElems.width, Width2);
                Mw2Library.Debug.WriteInt32(elem + HElems.height, Height2);
            }

            public static void SetClientDvars(int client, string dvars)
            {
                SV_SendServerCommand(client, "v " + dvars);
            }

            public static void SetElement(uint Element, uint HudTypes)
            {
                Mw2Library.PS3.Extension.WriteUInt32(Element, HudTypes);
            }

            public static void SetShader(uint clientIndex, uint elem, uint shader, int width, int height, float x, float y, int alignScreen, int alignOrg, uint align, float sort = 0f, int r = 0xff, int g = 0xff, int b = 0xff, int a = 0xff)
            {
                Mw2Library.PS3.Extension.WriteInt32(elem, 0);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
                Mw2Library.PS3.Extension.WriteInt32(elem + HElems.alignOrg, alignOrg);
                Mw2Library.PS3.Extension.WriteInt32(elem + HElems.alignScreen, alignScreen);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.shaderOffset, shader);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset, 5);
                Mw2Library.PS3.Extension.WriteUInt32((elem + HudStruct.relativeOffset) - 4, 6);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.heightOffset, height);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.widthOffset, width);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
                Mw2Library.PS3.Extension.WriteFloat((elem + HudStruct.textOffset) + 4, sort);
            }

            public static void SetText(uint clientIndex, uint elem, uint text, uint font, float fontScale, float x, float y, int alignScreen, int alignOrg, uint alignText, uint align, int r = 0xff, int g = 0xff, int b = 0xff, int a = 0xff, int GlowR = 0xff, int GlowG = 0, int GlowB = 0, int GlowA = 0)
            {
                Localize(Convert.ToInt32(clientIndex));
                Mw2Library.PS3.Extension.WriteInt32(elem, 0);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
                Mw2Library.PS3.Extension.WriteInt32(elem + HElems.alignOrg, alignOrg);
                Mw2Library.PS3.Extension.WriteInt32(elem + HElems.alignScreen, alignScreen);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.textOffset, text);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset, alignText);
                Mw2Library.PS3.Extension.WriteUInt32((elem + HudStruct.relativeOffset) - 4, 6);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.fontOffset, font);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
                Mw2Library.PS3.Extension.WriteInt16((elem + HudStruct.textOffset) + 4, 0x4000);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.fontSizeOffset, fontScale);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.GlowColor, RGB2INT(GlowR, GlowG, GlowB, GlowA));
            }

            public static void SV_SendServerCommand(int clientIndex, string Command)
            {
                bool flag;
                Mw2Library.RPC.WritePowerPc(true);
                Mw2Library.PS3.Extension.WriteString(0x10040010, Command);
                Mw2Library.PS3.Extension.WriteInt32(0x10040004, clientIndex);
                Mw2Library.PS3.Extension.WriteBool(0x10040003, true);
                do
                {
                    flag = Mw2Library.PS3.Extension.ReadBool(0x10040003);
                }
                while (flag);
                Mw2Library.RPC.WritePowerPc(false);
            }

            public static void TypeWriter(uint clientIndex, uint elem, uint text, uint font, float fontScale, float x, float y, uint alignText, uint align, int r = 0xff, int g = 0xff, int b = 0xff, int a = 0xff, int GlowR = 0xff, int GlowG = 0, int GlowB = 0, int GlowA = 0)
            {
                Localize(Convert.ToInt32(clientIndex));
                Mw2Library.PS3.Extension.WriteInt32(elem, 0);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.flags, 1);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.clientIndex, clientIndex);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.textOffset, text);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.relativeOffset, alignText);
                Mw2Library.PS3.Extension.WriteUInt32((elem + HudStruct.relativeOffset) - 4, 6);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.fontOffset, font);
                Mw2Library.PS3.Extension.WriteUInt32(elem + HudStruct.alignOffset, align);
                Mw2Library.PS3.Extension.WriteInt16((elem + HudStruct.textOffset) + 4, 0x4000);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.fontSizeOffset, fontScale);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.xOffset, x);
                Mw2Library.PS3.Extension.WriteFloat(elem + HudStruct.yOffset, y);
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.colorOffset, RGB2INT(r, g, b, a));
                Mw2Library.PS3.Extension.WriteInt32(elem + HudStruct.GlowColor, RGB2INT(GlowR, GlowG, GlowB, GlowA));
                Mw2Library.Debug.SetMemory(elem + HElems.fxBirthTime, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(GetLevelTime()))));
                Mw2Library.Debug.SetMemory(elem + HElems.fxLetterTime, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(70))));
                Mw2Library.Debug.SetMemory(elem + HElems.fxDecayStartTime, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(0xfa0))));
                Mw2Library.Debug.SetMemory(elem + HElems.fxDecayDuration, ArrayReverse(BitConverter.GetBytes(Convert.ToInt32(0x3e8))));
            }

            public static void WritePowerPc(bool Active)
            {
                byte[] buffer = new byte[] { 
                    0xf8, 0x21, 0xff, 0x61, 0x7c, 8, 2, 0xa6, 0xf8, 1, 0, 0xb0, 60, 0x60, 0x10, 3, 
                    0x80, 0x63, 0, 0, 0x60, 0x62, 0, 0, 60, 0x60, 0x10, 4, 0x80, 0x63, 0, 0, 
                    0x2c, 3, 0, 0, 0x41, 130, 0, 40, 60, 0x60, 0x10, 4, 0x80, 0x63, 0, 4, 
                    60, 160, 0x10, 4, 0x38, 0x80, 0, 0, 0x30, 0xa5, 0, 0x10, 0x4b, 0xe8, 0xb2, 0x7d, 
                    0x38, 0x60, 0, 0, 60, 0x80, 0x10, 4, 0x90, 100, 0, 0, 60, 0x60, 0x10, 5, 
                    0x80, 0x63, 0, 0, 0x2c, 3, 0, 0, 0x41, 130, 0, 0x24, 60, 0x60, 0x10, 5, 
                    0x30, 0x63, 0, 0x10, 0x4b, 0xe2, 0xf9, 0x7d, 60, 0x80, 0x10, 5, 0x90, 100, 0, 4, 
                    0x38, 0x60, 0, 0, 60, 0x80, 0x10, 5, 0x90, 100, 0, 0, 60, 0x60, 0x10, 3, 
                    0x80, 0x63, 0, 4, 0x60, 0x62, 0, 0, 0xe8, 1, 0, 0xb0, 0x7c, 8, 3, 0xa6, 
                    0x38, 0x21, 0, 160, 0x4e, 0x80, 0, 0x20
                 };
                byte[] buffer2 = new byte[] { 
                    0x81, 0x62, 0x92, 0x84, 0x7c, 8, 2, 0xa6, 0xf8, 0x21, 0xff, 1, 0xfb, 0xe1, 0, 0xb8, 
                    0xdb, 1, 0, 0xc0, 0x7c, 0x7f, 0x1b, 120, 0xdb, 0x21, 0, 200, 0xdb, 0x41, 0, 0xd0, 
                    0xdb, 0x61, 0, 0xd8, 0xdb, 0x81, 0, 0xe0, 0xdb, 0xa1, 0, 0xe8, 0xdb, 0xc1, 0, 240, 
                    0xdb, 0xe1, 0, 0xf8, 0xfb, 0x61, 0, 0x98, 0xfb, 0x81, 0, 160, 0xfb, 0xa1, 0, 0xa8, 
                    0xfb, 0xc1, 0, 0xb0, 0xf8, 1, 1, 0x10, 0x81, 0x2b, 0, 0, 0x88, 9, 0, 12, 
                    0x2f, 0x80, 0, 0, 0x40, 0x9e, 0, 100, 0x7c, 0x69, 0x1b, 120, 0xc0, 2, 0x92, 0x94, 
                    0xc1, 0xa2, 0x92, 0x88, 0xd4, 9, 2, 0x40, 0xd0, 9, 0, 12, 0xd1, 0xa9, 0, 4, 
                    0xd0, 9, 0, 8, 0xe8, 1, 1, 0x10, 0xeb, 0x61, 0, 0x98, 0xeb, 0x81, 0, 160, 
                    0x7c, 8, 3, 0xa6, 0xeb, 0xa1, 0, 0xa8, 0xeb, 0xc1, 0, 0xb0, 0xeb, 0xe1, 0, 0xb8, 
                    0xcb, 1, 0, 0xc0, 0xcb, 0x21, 0, 200
                 };
                if (Active)
                {
                    Mw2Library.PS3.SetMemory(0x38ede8, buffer);
                }
                else
                {
                    Mw2Library.PS3.SetMemory(0x38ede8, buffer2);
                }
            }

            public static class HElems
            {
                public static uint alignOrg = 0x2c;
                public static uint alignScreen = 0x30;
                public static uint clientIndex = 0xa8;
                public static uint color = 0x34;
                public static uint duration = 0x80;
                public static uint fadeStartTime = 60;
                public static uint fadeTime = 0x40;
                public static uint flags = 0xa4;
                public static uint font = 40;
                public static uint fontScale = 20;
                public static uint fontScaleStartTime = 0x1c;
                public static uint fontScaleTime = 0x20;
                public static uint fromAlignOrg = 0x68;
                public static uint fromAlignScreen = 0x6c;
                public static uint fromColor = 0x38;
                public static uint fromFontScale = 0x18;
                public static uint fromHeight = 80;
                public static uint fromWidth = 0x54;
                public static uint fromX = 100;
                public static uint fromY = 0x60;
                public static uint fxBirthTime = 0x90;
                public static uint fxDecayDuration = 0x9c;
                public static uint fxDecayStartTime = 0x98;
                public static uint fxLetterTime = 0x94;
                public static uint glowColor = 140;
                public static uint height = 0x44;
                public static uint label = 0x24;
                public static uint materialIndex = 0x4c;
                public static uint moveStartTime = 0x70;
                public static uint moveTime = 0x74;
                public static uint ort = 0x88;
                public static uint scaleStartTime = 0x58;
                public static uint scaleTime = 0x5c;
                public static uint soundID = 160;
                public static uint targetEntNum = 0x10;
                public static uint text = 0x84;
                public static uint time = 0x7c;
                public static uint type = 0;
                public static uint value = 120;
                public static uint width = 0x48;
                public static uint x = 8;
                public static uint y = 4;
                public static uint z = 12;
            }

            public static class HUDAlign
            {
                public static uint CENTER = 5;
                public static uint LEFT = 1;
                public static uint RIGHT = 2;
            }

            public static class HudAlloc
            {
                public static uint g_hudelem = 0x12e9858;
                public static uint IndexSlot = 50;
                public static bool Start = true;
            }

            public class HudStruct
            {
                public static uint alignOffset = 0x30;
                public static uint clientIndex = 0xa8;
                public static uint colorOffset = 0x34;
                public static uint flags = 0xa4;
                public static uint fontOffset = 40;
                public static uint fontSizeOffset = 20;
                public static uint GlowColor = 140;
                public static uint heightOffset = 0x44;
                public static uint relativeOffset = 0x2c;
                public static uint shaderOffset = 0x4c;
                public static uint textOffset = 0x84;
                public static uint widthOffset = 0x48;
                public static uint xOffset = 8;
                public static uint yOffset = 4;
            }

            public class HudTypes
            {
                public static uint Null = 0;
                public static uint Shader = 6;
                public static uint Text = 1;
            }

            public class Material
            {
                public static uint Black = 2;
                public static uint NoMap = 0x29;
                public static uint Prestige0 = 0x1a;
                public static uint Prestige1 = 0x1b;
                public static uint Prestige10 = 0x24;
                public static uint Prestige2 = 0x1c;
                public static uint Prestige3 = 0x1d;
                public static uint Prestige4 = 30;
                public static uint Prestige5 = 0x1f;
                public static uint Prestige6 = 0x20;
                public static uint Prestige7 = 0x21;
                public static uint Prestige8 = 0x22;
                public static uint Prestige9 = 0x23;
                public static uint White = 1;
                public static uint WhiteRectangle = 0x25;
            }
        }

        public static class Mods
        {
            public static class ChangeMap
            {
                public static class DLCMaps
                {
                    public static void Bailout(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_Bailout ");
                    }

                    public static void Carnival(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_abandon ");
                    }

                    public static void Crash(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_crash ");
                    }

                    public static void Fuel(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_fuel ");
                    }

                    public static void Overgrown(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_overgrown ");
                    }

                    public static void Salvage(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_compact ");
                    }

                    public static void Storm(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_storm ");
                    }

                    public static void Strike(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_strike ");
                    }

                    public static void TrailerPark(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_trailerpark ");
                    }

                    public static void Vacant(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_vacant ");
                    }
                }

                public static class RetailMaps
                {
                    public static void Afghan(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_afghan ");
                    }

                    public static void Derail(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_derail ");
                    }

                    public static void Estate(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_estate ");
                    }

                    public static void Favela(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_favela ");
                    }

                    public static void Highrise(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_highrise ");
                    }

                    public static void Invasion(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_invasion ");
                    }

                    public static void Karachi(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_checkpoint ");
                    }

                    public static void Quarry(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_quarry ");
                    }

                    public static void Rundown(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_rundown ");
                    }

                    public static void Rust(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_rust ");
                    }

                    public static void Scrapyard(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_boneyard ");
                    }

                    public static void Skidrow(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_nightshift ");
                    }

                    public static void SubBase(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_subbase ");
                    }

                    public static void Terminal(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_terminal ");
                    }

                    public static void Underpass(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_underpass ");
                    }

                    public static void Wasteland(string text)
                    {
                        Mw2Library.RPC.Cbuf_AddText("map mp_brecourt ");
                    }
                }
            }

            public static class Clients
            {
                public static bool SetUp1 = (((((((((Mw2Library.RPC.get_MapName() == "Afghan") | (Mw2Library.RPC.get_MapName() == "Highrise")) | (Mw2Library.RPC.get_MapName() == "Karachi")) | (Mw2Library.RPC.get_MapName() == "Quarry")) | (Mw2Library.RPC.get_MapName() == "Rundown")) | (Mw2Library.RPC.get_MapName() == "Skidrow")) | (Mw2Library.RPC.get_MapName() == "Terminal")) | (Mw2Library.RPC.get_MapName() == "Wasteland")) | (Mw2Library.RPC.get_MapName() == "Overgrown"));

                public static void Kick(int ClientInt)
                {
                    Mw2Library.RPC.Kick(ClientInt, "You have been kicked!");
                }

                public static void KickWithWarning(int ClientInt)
                {
                    Mw2Library.RPC.Kick(ClientInt, "^1Get The Fuck Out Of " + Mw2Library.PS3.Extension.ReadString(Mw2Library.Offsets.Name) + " ^2Lobby!");
                }

                public static void Kill(int ClientInt)
                {
                    byte[] buffer = new byte[] { 0xc5 };
                    Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Kill + ((uint) (ClientInt * 0x3700)), buffer);
                    Mw2Library.RPC.iPrintln(ClientInt, "^2You have been ^1Killed!");
                }

                public static void KillAndScare(int ClientInt)
                {
                    byte[] buffer = new byte[] { 0xff, 0xff };
                    Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.KillAndScare + ((uint) (ClientInt * 0x3700)), buffer);
                    Mw2Library.RPC.iPrintln(ClientInt, "^1SCREAMMMMMMMMMMMMMMMMM!");
                }

                public static class Aimbot
                {
                    public static class _180
                    {
                        public static void Off(int ClientInt)
                        {
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_target_sentient_radius");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_debug");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_debug");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_region_width");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_region_height");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_strength");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_deflection");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_region_height");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_region_width");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_yaw_scale_ads");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_yaw_scale");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_pitch_scale");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_pitch_scale_ads");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_region_height");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_region_width");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_aimAssistRangeScale");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoAimRangeScale");
                            Mw2Library.RPC.iPrintln(ClientInt, "180 Aimbot - Off!");
                        }

                        public static void On(int ClientInt)
                        {
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_target_sentient_radius 128");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_debug 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_debug 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_region_width 640");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_region_height 480");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_strength 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_deflection 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_region_height 480");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_region_width 640");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_yaw_scale_ads 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_yaw_scale 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_pitch_scale 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_pitch_scale_ads 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_region_height 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_region_width 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_aimAssistRangeScale 2");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoAimRangeScale 2");
                            Mw2Library.RPC.iPrintln(ClientInt, "180 Aimbot - On!");
                        }
                    }

                    public static class Normal
                    {
                        public static void Off(int ClientInt)
                        {
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_lerp");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_region_height");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_region_width");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_aimAssistRangeScale");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoAimRangeScale");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_debug");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_region_height");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_region_width");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_strength");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_deflection");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_yaw_scale_ads");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_pitch_scale_ads");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_slowdown_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_autoaim_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset aim_lockon_enabled");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "aim_autoaim_enabled 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "reset SingleAimBot");
                            Mw2Library.RPC.iPrintln(ClientInt, "Aimbot - Off!");
                        }

                        public static void On(int ClientInt)
                        {
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set SingleAimBot");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_lerp 100");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_region_height 480");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_region_width 640");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_aimAssistRangeScale 2");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoAimRangeScale 2");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_debug 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_region_height 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_region_width 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_strength 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_deflection 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_enabled 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_yaw_scale_ads 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_pitch_scale_ads 0");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_slowdown_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_autoaim_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set aim_lockon_enabled 1");
                            Mw2Library.RPC.SetClientDvars(ClientInt, "set SingleFire");
                            Mw2Library.RPC.iPrintln(ClientInt, "Aimbot - On!");
                        }
                    }
                }

                public static class AutoKill
                {
                    public static void Off(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.AutoKill + ((uint) (ClientInt * 640)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Auto Kill ^3Off!");
                    }

                    public static void On(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 1 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.AutoKill + ((uint) (ClientInt * 640)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Auto Kill ^3On!");
                    }
                }

                public static class Bullets
                {
                    public static void Cobra20mm(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa2 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa2 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x2a, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Cobra 20mm!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa1 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa1 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x29, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Cobra 20mm!");
                        }
                    }

                    public static void CobraMinigun(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa3 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa3 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x2b, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Cobra Minigun!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa2 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa2 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x2a, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Cobra Minigun!");
                        }
                    }

                    public static void CobraMissiles(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa1 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa1 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x29, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Cobra Missiles!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 160 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 160 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 40, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Cobra Missiles!");
                        }
                    }

                    public static void Harrier20mm(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9e });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9e });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x26, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Harrier 20mm!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9d });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9d });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x25, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Harrier 20mm!");
                        }
                    }

                    public static void HarrierMissiles(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9f });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9f });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x27, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Harrier Missiles!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9e });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9e });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x26, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Harrier Missiles!");
                        }
                    }

                    public static void LittleBird20mm(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 160 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 160 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 40, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Little Bird 20mm!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9f });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x9f });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x27, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Little Bird 20mm!");
                        }
                    }

                    public static void PavelowMinigun(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa5 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa5 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x2d, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Pavelow Minigun!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa4 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa4 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x2c, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Pavelow Minigun!");
                        }
                    }

                    public static void SentryMinigun(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa6 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa6 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x2e, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Sentry Gun!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa5 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Type2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0xa5 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Bullets.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x2d, 15, 0xff, 0xff, 0xff });
                            Mw2Library.RPC.iPrintln(ClientInt, "^1Bullet Type Set To: ^2Sentry Gun!");
                        }
                    }
                }

                public static class Challenges
                {
                    public static void GiveDerankAll(int ClientInt)
                    {
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v clanName \"{}{}\"");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v motd \"-> You Have Been Deranked! ^0By The Host! <-\"");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "c \"^0You have been Deranked!\"");
                        Thread.Sleep(300);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v loc_warnings \"0\"");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v loc_warningsAsErrors \"0\"");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 2056 000000 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3737 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3775 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3787 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3792 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3747 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000");
                        Thread.Sleep(100);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3826 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3848 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 000000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3877 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3812 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3883 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3909 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3918 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3934 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3949 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3969 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000 0000 00 0000 0000 0000 00");
                        Thread.Sleep(100);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3989 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4003 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4013 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4026 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4046 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 0000000000000 0000 00000000 0000 0000 0000 0000ZZ0000 0000 00000");
                        Thread.Sleep(100);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6641 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6644 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6507 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6651 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6509 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6656 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6661 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Thread.Sleep(0);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6679 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6633 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6690 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6701 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6532 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3850 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3900 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3950 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4050 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00 0000 00");
                        Thread.Sleep(100);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v clanName \"{}{}\"");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v motd \"-> You Have Been Deranked! ^0By The Host! <-\"");
                        Thread.Sleep(100);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "c \"^0You have been Deranked!\"");
                    }

                    public static void GiveUnlockAll(int ClientInt)
                    {
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v loc_warnings \"0\"");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v loc_warningsAsErrors \"0\"");
                        Mw2Library.RPC.iPrintln(ClientInt, "^1Unlocking All: 0%");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 2056 206426 6525 7F 3760 09 4623 E803 3761 09 4627 F430 3762 02 4631 14 3763 02 4635 3C 3764 02 4639 0F 3765 02 4643 14 3766 02 4647 28 3767 02 4651 0A 3752 09 4591 E803 3753 09 4595 0F40 3754 02 4599 14 3755 02 4603 3C 3756 02 4607 0F 3757 02 4611 14 3758 02 4615 28 3759 02 4619 0A 3736 09 4527 E803");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3737 09 4531 0F40 3738 02 4535 14 3739 02 4539 3C 3740 02 4543 0F 3741 02 4547 14 3742 02 4551 28 3743 02 4555 0A 3799 09 4779 E803 3800 09 4783 0F40 3801 02 4787 14 3802 02 4791 3C 3803 02 4795 0F 3804 02 4799 14 3805 02 4803 28 3806 02 4807 0A");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3775 09 4683 E803 3776 09 4687 0F40 3777 02 4691 14 3778 02 4695 3C 3779 02 4699 0F 3780 02 4703 14 3781 02 4707 28 3782 02 4711 0A 3728 09 4495 E803 3729 09 4499 0F40 3730 02 4503 14 3731 02 4507 3C 3732 02 4511 0F 3733 02 4515 14 3734 02 4519 28 3735 02 4523 0A 3783 09 4715 E803 3784 09 4719 0F40 3785 02 4723 14 3786 02 4727 3C");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3787 02 4731 0F 3788 02 4735 14 3789 02 4739 28 3790 02 4743 0A 3791 09 4747 E803 3864 02 5039 14 3865 02 5043 28 3866 02 5047 09 3888 09 5135 E803 3887 09 5131 0F40");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3792 09 4751 0F40 3793 02 4755 14 3794 02 4759 3C 3795 02 4763 0F 3796 02 4767 14 3797 02 4771 28 3798 02 4775 0A 3744 09 4559 E803 3745 09 4563 0F40 3746 02 4567 14 3889 02 5139 0F 3890 02 5143 3C 3891 02 5147 14 3892 02 5151 28 3893 02 5155 09 3807 09 4811 E803 3808 09 4815 0F40 3809 02 4819 0F 3810 02 4823 14 3811 02 4827 28");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3747 02 4571 3C 3748 02 4575 0F 3749 02 4579 14 3750 02 4583 28 3751 02 4587 0A 3853 09 4995 E803 3854 09 4999 0F40 3855 02 5003 1E 3856 02 5007 3C 3857 02 5011 14 3858 02 5015 28 3859 02 5019 09 3839 09 4939 E803 3840 09 4943 0F40 3841 02 4947 1E 3842 02 4951 3C 3843 02 4955 14 3844 02 4959 28 3845 02 4963 09 3825 09 4883 E803");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3826 09 4887 0F40 3827 02 4891 1E 3828 02 4895 3C 3829 02 4899 14 3830 02 4903 28 3831 02 4907 09 3832 09 4911 E803 3833 09 4915 0F40 3834 02 4919 1E 3835 02 4923 3C 3836 02 4927 14 3837 02 4931 28 3838 02 4935 09 3846 09 4967 E803 3847 09 4971 0F40");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3848 02 4975 1E 3849 02 4979 3C 3850 02 4983 14 3851 02 4987 28 3852 02 4991 09 3768 09 4655 E803 3769 09 4659 0F40 3771 02 4667 0F 3770 02 4663 3C 3772 02 4671 14 3773 02 4675 28 3774 02 4679 09 3874 09 5079 E803 3875 09 5083 0F40 3876 02 5087 0F");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3877 02 5091 3C 3878 02 5095 14 3879 02 5099 28 3880 02 5103 09 3867 09 5051 E803 3868 09 5055 0F40 3869 02 5059 0F 3870 02 5063 3C 3871 02 5067 14 3872 02 5071 28 3873 02 5075 09 3860 09 5023 E803 3861 09 5027 0F40 3862 02 5031 0F 3863 02 5035 3C");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3812 02 4831 06 3813 09 4835 E803 3814 09 4839 0F40 3815 02 4843 0F 3816 02 4847 14 3817 02 4851 28 3818 02 4855 06 3819 09 4859 E803 3820 09 4863 0F40 3821 02 4867 0F 3822 02 4871 14 3823 02 4875 28 3824 02 4879 06 3881 09 5107 E803 3882 09 5111 0F40");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3883 02 5115 0F 3884 02 5119 14 3885 02 5123 28 3886 02 5127 06 3898 09 5175 E803 3899 09 5179 0F40 3894 09 5159 E803 3895 09 5163 0F40 3900 09 5183 E803 3901 09 5187 0F40 3896 09 5167 E803 3897 09 5171 0F40 3902 09 5191 E803 3903 09 5195 0F40 3908 09 5215 E803");
                        Thread.Sleep(100);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Unlocking All: 25%");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3909 09 5219 0F40 3904 09 5199 E803 3905 09 5203 0F40 3906 09 5207 E803 3907 09 5211 0F40 3912 06 5231 C409 3913 09 5235 0F40 3910 06 5223 C409 3911 09 5227 0F40 3916 09 5247 E803 3917 09 5251 0F40 3914 09 5239 E803 3915 09 5243 0F40 3920 07 5263 C409 3921 09 5267 0F40");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3918 07 5255 C409 3919 09 5259 0F40 3922 09 5271 B004 3923 09 5275 B004 3924 09 5279 B004 3925 09 5283 B004 3926 09 5287 FA 3643 0A 4155 09 3927 07 5292 6108 3931 07 5307 EE02 3938 07 5335 0F40 3932 07 5311 8403 3935 07 5323 EE02 3933 07 5315 E803 3941 07 5347 402414");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3934 07 5319 FA 3936 07 5327 FA 3942 07 5351 0F40 3939 07 5339 64 3928 07 5295 0F40 3930 07 5303 FA 3929 07 5299 FA 3940 07 5343 EE02 3937 07 5331 64 3943 04 5355 32 3944 04 5359 32 3945 04 5363 32 3946 04 5367 32 3947 04 5371 32 3948 04 5375 32");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3949 04 5379 32 3950 04 5383 32 3951 04 5387 19 3952 04 5391 19 3953 04 5395 19 3954 04 5399 19 3955 04 5403 19 3956 04 5407 0A 3957 04 5411 0A 3958 04 5415 E803 3959 04 5419 E803 3960 04 5423 E803 3961 04 5427 E803 3962 04 5431 32 3963 04 5435 1E 3964 04 5439 32 3965 04 5443 1E 3966 04 5447 32 3967 04 5451 1E 3968 04 5455 1E");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3969 02 5459 FF 3972 02 5471 FF 3973 02 5475 FF 3983 02 5515 FF 3984 02 5519 FF 3985 02 5523 FF 3986 02 5527 FF 3987 02 5531 FF 3988 02 5535 FF 4100 02 5983 FF 3970 02 5463 19 3971 02 5467 19 4020 04 5663 1E 4021 04 5667 1E 4022 04 5671 1E 4023 04 5675 0F 4024 04 5679 0F 4025 04 5683 0F");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3989 02 5539 FF 3990 02 5543 FF 3991 02 5547 FF 3992 02 5551 FF 3994 02 5559 FF 3995 02 5563 FF 3996 02 5567 FF 3997 02 5571 FF 4001 02 5587 FF 4002 02 5591 FF 4028 04 5695 50C3 4029 04 5699 50C3 4030 04 5703 64 4035 04 5723 32 4036 04 5727 32 4037 04 5731 32 4038 04 5735 32 4039 04 5739 32 4040 04 5743 32");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4003 02 5595 FF 4004 02 5599 FF 4005 02 5603 FF 4006 02 5607 FF 4007 02 5611 FF 4008 02 5615 FF 4009 02 5619 FF 4010 02 5623 FF 4011 02 5627 FF 4012 02 5631 FF 4101 04 5987 C8 4103 04 5995 0A 4104 04 5999 1E 4105 04 6003 1E 3993 04 5555 14 3998 04 5575 C8 3999 03 5579 0A 4000 03 5583 0A 4107 04 6011 0F");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4013 02 5635 FF 4014 02 5639 FF 4015 02 5643 FF 4016 02 5647 FF 4017 02 5651 FF 4018 02 5655 FF 4114 02 6039 FF 4110 02 6023 FF 4106 02 6007 FF 4019 02 5659 FF 4041 04 5747 32 4050 03 5783 19 4051 03 5787 19 4055 03 5803 19 4056 03 5807 19 4065 04 5843 14 4068 04 5855 14 4069 04 5859 14 4058 03 5815 19");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4026 02 5687 FF 4027 02 5691 FF 4042 02 5751 FF 4031 02 5707 FF 4032 02 5711 FF 4033 02 5715 FF 4034 02 5719 FF 4043 02 5755 FF 4044 02 5759 FF 4045 02 5763 FF 4108 04 6015 32 4109 02 6019 0A 4111 03 6027 0A 4112 03 6031 0A 4113 03 6035 0A 4115 03 6043 0A 4116 05 6047 FA 4117 05 6051 64 4118 05 6055 E803");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4046 02 5767 FF 4047 02 5771 FF 4048 02 5775 FF 4049 02 5779 FF 4052 02 5791 FF 4053 02 5795 FF 4054 02 5799 FF 4102 02 5991 FF 4121 02 6067 FF 4057 02 5811 FF 4119 05 6059 2C00 4120 05 6063 2C00 6525 7F");
                        Thread.Sleep(100);
                        Mw2Library.RPC.iPrintln(ClientInt, "^3Unlocking All: 50%");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4059 02 5819 OO 4060 02 5823 OO 4061 02 5827 OO 4062 02 5831 OO 4063 02 5835 OO 4064 02 5839 OO 4066 02 5847 OO 4067 02 5851 OO 4070 02 5863 OO 4071 02 5867 OO 4072 02 5871 OO 4073 02 5875 OO 4074 02 5879 OO 4075 02 5883 OO 4076 02 5887 OO 4077 02 5891 OO 4078 02 5895 OO 4079 02 5899 OO 4080 02 5903 OO 4081 02 5907 OO");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4082 02 5911 OO 4083 02 5915 OO 4084 02 5919 OO 4085 02 5923 OO 4086 02 5927 OO 4087 02 5931 OO 4088 02 5935 OO 4089 02 5939 OO 4090 02 5943 OO 4091 02 5947 OO 4092 02 5951 OO 4093 02 5955 OO 4094 02 5959 OO 4095 02 5963 OO 4096 02 5967 OO 4097 02 5971 OO 4098 02 5975 OO 4099 02 5979 OO 4100 02 5983 OO 4099 02 5979 OO");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3038 05 6695 80 6696 10 6697 02 6697 42 6696 11 6696 31 6697 46 6697 C6 6696 33 6696 73 6697 CE 6698 09 6696 7B 6697 CF 6697 EF 6698 0D 6696 7F 6696 FF 6697 FF 6698 0F 6637 84 6637 8C 6503 03 6637 9C 6637 BC 6503 07 6637 FC 6638 FF 6503 0F 6638 03 6638 07");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6503 1F 6638 0F 6638 1F 6638 3F 6503 3F 6638 7F 6638 FF 6503 7F 6639 FF 6639 03 6639 07 6503 FF 6639 0F 6639 1F 6504 FF 6639 3F 6639 7F 6639 FF 6504 03 6640 09 6640 0B 6504 07 6640 0F 6640 1F 6504 0F 6640 3F 6640 7F 6504 1F 6640 FF 6641 23 6504 3F 6641 27");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3038 05 3550 05 3614 05 3486 05 3422 05 3358 05 3294 05 3230 05 3166 05 3102 05 3038 05 2072 2D302E302F30O 2092 30303130 2128 3130 2136 3B05ZZ3C05 2152 3D05O");
                        Thread.Sleep(100);
                        Mw2Library.RPC.iPrintln(ClientInt, "^5Unlocking All: 75%");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6641 2F 6504 7F 6641 3F 6641 7F 6504 FF 6641 FF 6642 85 6505 FF 6642 87 6642 8F 6505 03 6642 9F 6642 BF 6505 07 6642 FF 6643 11 6505 0F 6643 13 6643 17 6505 1F 6643 1F 6643 3F 6505 3F 6643 7F 6643 FF 6505 7F 6644 43 6644 47 6505 FF 6644 4F 6644 5F 6506 FF");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6644 7F 6644 FF 6506 03 6645 09 6645 0B 6506 07 6645 0F 6645 1F 6506 0F 6645 3F 6645 7F 6506 1F 6645 FF 6646 23 6506 3F 6646 27 6646 2F 6506 7F 6646 3F 6646 7F 6506 FF 6646 FF 6647 85 6507 FF 6647 87 6647 8F 6507 03 6647 9F 6647 BF 6507 07 6647 FF 6648 11");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6507 0F 6648 13 6648 17 6507 1F 6648 1F 6648 3F 6507 3F 6648 7F 6648 FF 6507 7F 6649 FF 6649 03 6649 07 6507 FF 6649 0F 6649 1F 6508 FF 6649 3F 6649 7F 6649 FF 6508 03 6650 FF 6650 03 6508 07 6650 07 6650 0F 6650 1F 6508 0F 6650 3F 6650 7F 6508 1F 6650 FF");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6651 FF 6651 03 6508 3F 6651 07 6651 0F 6508 7F 6651 1F 6651 3F 6508 FF 6651 7F 6651 FF 6509 FF 6652 FF 6652 03 6509 03 6652 07 6652 0F 6509 07 6652 1F 6652 3F 6509 0F 6652 7F 6652 FF 6509 1F 6653 FF 6653 03 6509 3F 6653 07 6653 0F 6509 7F 6653 1F 6653 3F");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6509 FF 6653 7F 6653 FF 6510 FF 6654 FF 6654 03 6510 03 6654 07 6654 0F 6510 07 6654 1F 6654 3F 6510 0F 6654 7F 6654 FF 6510 1F 6655 FF 6655 03 6510 3F 6655 07 6655 0F 6510 7F 6655 1F 6655 3F 6510 FF 6655 7F 6655 FF 6511 FF 6656 FF 6656 03 6511 03 6656 07");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6656 0F 6511 07 6656 1F 6656 3F 6511 0F 6656 7F 6656 FF 6511 1F 6657 FF 6657 03 6511 3F 6657 07 6657 0F 6511 7F 6657 1F 6657 3F 6511 FF 6657 7F 6657 FF 6512 FF 6658 FF 6658 03 6512 03 6658 07 6658 0F 6512 07 6658 1F 6658 9F 6658 BF 6658 FF 6680 FF 6661 5B");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6661 5F 6661 7F 6661 FF 6673 08 6673 18 6673 38 6673 78 6673 F8 6674 FF 6674 03 6674 07 6674 0F 6674 1F 6674 3F 6674 7F 6674 FF 6679 08 6673 F9 6673 FB 6673 FF 6675 FF 6677 FF 6677 03 6677 07 6677 0F 6677 1F 6677 3F 6677 7F 6677 FF 6679 09 6679 0B 6679 0F");
                        Thread.Sleep(100);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6679 1F 6679 3F 6679 7F 6679 FF 6680 03 6680 07 6680 0F 6680 1F 6680 3F 6680 BF 6681 FF 6681 03 6681 0B 6681 1B 6681 3B 6681 7B 6681 FB 6681 FF 6680 FF 6686 FF 6632 FF 6632 03 6632 07 6632 0F 6632 1F 6632 3F 6632 7F 6632 FF 6633 FF 6633 03 6633 07 6633 0F");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6633 1F 6633 3F 6633 7F 6633 FF 6634 FF 6634 03 6634 07 6634 0F 6634 1F 6634 3F 6634 7F 6634 FF 6635 FF 6635 03 6635 07 6635 0F 6635 1F 6635 3F 6635 7F 6635 FF 6636 FF 6636 03 6636 07 6636 0F 6636 1F 6636 3F 6636 7F 6636 FF 6637 FD 6637 FF 6690 FF 6690 03");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6690 07 6690 0F 6690 1F 6690 3F 6690 7F 6690 FF 6695 81 6695 83 6695 87 6695 8F 6695 9F 6695 BF 6698 1F 6698 3F 6698 7F 6698 FF 6699 C1 6699 C3 6699 C7 6699 CF 6699 DF 6699 FF 6700 1F 6700 3F 6700 7F 6700 FF 6701 03 6701 07 6701 0F 6701 1F 6701 3F 6701 7F");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6701 FF 6702 FF 6702 03 6702 07 6524 10 6524 30 6524 70 6524 F0 6529 FF 6529 03 6529 07 6530 08 6529 0F 6529 1F 6529 3F 6529 7F 6529 FF 6530 09 6530 0B 6530 0F 6530 1F 6530 7F 6530 FF 6531 FF 6531 03 6531 07 6531 0F 6531 1F 6531 3F 6531 7F 6531 FF 6532 FF");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 6532 03 6532 07 6532 0F 6512 C7 6526 02 6512 D7 6526 06 6512 F7 6526 86 6532 1F 6532 3F 6532 BF 6533 F9 6533 FB 6533 FF 6532 FF 6526 87 6526 A7 6512 FF 6540 7F 6526 E7 6526 EF 6526 FF 6517 FF 6527 FF 6528 FF 6522 FF 6524 F1 6524 F3 6524 F7 6524 FF");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3850 99 3851 99 3852 99 3853 99 3854 99 3855 99 3856 99 3857 99 3858 99 3859 99 3860 99 3861 99 3862 99 3863 99 3864 99 3865 99 3866 99 3867 99 3868 99 3869 99 3870 99 3871 99 3872 99 3873 99 3874 99 3875 99 3876 99 3877 99 3878 99 3879 99 3880 99 3881 99 3882 99 3883 99 3884 99 3885 99 3886 99 3887 99 3888 99 3889 99 3890 99 3891 99 3892 99 3893 99 3894 99 3895 99 3896 99 3897 99 3898 99 3899 99 3900 99");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3900 99 3901 99 3902 99 3903 99 3904 99 3905 99 3906 99 3907 99 3908 99 3909 99 3910 99 3911 99 3912 99 3913 99 3914 99 3915 99 3916 99 3917 99 3918 99 3919 99 3920 99 3921 99 3922 99 3923 99 3924 99 3925 99 3926 99 3927 99 3928 99 3929 99 3930 99 3931 99 3932 99 3933 99 3934 99 3935 99 3936 99 3937 99 3938 99 3939 99 3940 99 3941 99 3942 99 3943 99 3944 99 3945 99 3946 99 3947 99 3948 99 3949 99 3950 99");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 3950 99 3951 99 3952 99 3953 99 3954 99 3955 99 3956 99 3957 99 3958 99 3959 99 3960 99 3961 99 3962 99 3963 99 3964 99 3965 99 3966 99 3967 99 3968 99 3969 99 3970 99 3971 99 3972 99 3973 99 3974 99 3975 99 3976 99 3977 99 3978 99 3979 99 3980 99 3981 99 3982 99 3983 99 3984 99 3985 99 3986 99 3987 99 3988 99 3989 99 3990 99 3991 99 3992 99 3993 99 3994 99 3995 99 3996 99 3997 99 3998 99 3999 99 4000 99");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4000 99 4001 99 4002 99 4003 99 4004 99 4005 99 4006 99 4007 99 4008 99 4009 99 4010 99 4011 99 4012 99 4013 99 4014 99 4015 99 4016 99 4017 99 4018 99 4019 99 4020 99 4021 99 4022 99 4023 99 4024 99 4025 99 4026 99 4027 99 4028 99 4029 99 4030 99 4031 99 4032 99 4033 99 4034 99 4035 99 4036 99 4037 99 4038 99 4039 99 4040 99 4041 99 4042 99 4043 99 4044 99 4045 99 4046 99 4047 99 4048 99 4049 99 4050 99");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 4050 99 4051 99 4052 99 4053 99 4054 99 4055 99 4056 99 4057 99 4058 99 4059 99 4060 99 4061 99 4062 99 4063 99 4064 99 4065 99 4066 99 4067 99 4068 99 4069 99 4070 99 4071 99 4072 99 4073 99 4074 99 4075 99 4076 99 4077 99 4078 99 4079 99 4080 99 4081 99 4082 99 4083 99 4084 99 4085 99 4086 99 4087 99 4088 99 4089 99 4090 99 4091 99 4092 99 4093 99 4094 99 4095 99 4096 99 4097 99 4098 99 4099 99 4100 99");
                        Thread.Sleep(100);
                        Mw2Library.RPC.iPrintln(ClientInt, "^6Unlocking All: 100%");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v clanName \"{MK}\"");
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "v motd \"-> Made by ^5Mx444 ^7- All Clients Instant Unlock All & Stats 1.14 - ^2Have ^3Fun :)<-\"");
                        Thread.Sleep(100);
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "c \"^7Instant Unlock All DONE ! - Hosted by ^6 " + Mw2Library.PS3.Extension.ReadString(Mw2Library.Offsets.Name) + "\"");

                    }

                    public static void Level70(int ClientInt)
                    {
                        Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 2056 206426 6525 7F 3760 09 4623 E803 3761 09 4627 F430 3762 02 4631 14 3763 02 4635 3C 3764 02 4639 0F 3765 02 4643 14 3766 02 4647 28 3767 02 4651 0A 3752 09 4591 E803 3753 09 4595 0F40 3754 02 4599 14 3755 02 4603 3C 3756 02 4607 0F 3757 02 4611 14 3758 02 4615 28 3759 02 4619 0A 3736 09 4527 E803");
                    }

                    public static class Prestige
                    {
                        public static void _0(int ClientInt)
                        {
                            Mw2Library.RPC.SetClientDvars(0, "loc_warnings 0");
                            Mw2Library.RPC.SetClientDvars(0, "loc_warningsAsErrors 0");
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 2064 00000");
                        }

                        public static void _1(int ClientInt)
                        {
                            Mw2Library.RPC.SetClientDvars(0, "loc_warnings 0");
                            Mw2Library.RPC.SetClientDvars(0, "loc_warningsAsErrors 0");
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N 2064 01000");
                        }

                        public static void _10(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 0A000");
                        }

                        public static void _11(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 0B000");
                        }

                        public static void _2(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 02000");
                        }

                        public static void _3(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 03000");
                        }

                        public static void _4(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 04000");
                        }

                        public static void _5(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 05000");
                        }

                        public static void _6(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 06000");
                        }

                        public static void _7(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 07000");
                        }

                        public static void _8(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 08000");
                        }

                        public static void _9(int ClientInt)
                        {
                            Mw2Library.RPC.SV_SendServerCommand(ClientInt, "N  2064 09000");
                        }
                    }
                }

                public static class Give
                {
                    public static void AllPerks(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 
                            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
                            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff
                         };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.AllPerks + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2All Perks ^3On");
                    }

                    public static void CounterUAV(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 1 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.UAV.CounterUAV + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Counter UAV ^3On");
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.UAV.GiveUAV + ((uint) (ClientInt * 0x3700)), new byte[1]);
                    }

                    public static void ExplosiveBullets(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 0xff, 0xef };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.ExplosiveBullets + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Explosive Bullets ^3On");
                    }

                    public static void FieldOfView(int ClientInt)
                    {
                        Mw2Library.RPC.SetClientDvars(ClientInt, "cg_fov 90");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Field Of View Set To: ^390!");
                    }

                    public static void GodMode(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Health1 + ((uint) (ClientInt * 0x3700)), new byte[] { 0xff, 0xff });
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Health2 + ((uint) (ClientInt * 0x3700)), new byte[] { 0xff, 0xff, 0xff, 0xff });
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Health3 + ((uint) (ClientInt * 0x3700)), new byte[] { 0xff, 0xff, 0xff, 0xff });
                        Mw2Library.RPC.iPrintln(ClientInt, "^2God Mode ^3On");
                    }

                    public static void Lag(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 15 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Lag + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Lag ^3On");
                    }

                    public static void LittleCrosshair(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 2 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.LittleCrosshair + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Little Crosshair: ^3On");
                    }

                    public static void RedBoxes(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 80 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.RedBoxes + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Red Boxes ^3On");
                    }

                    public static void SkateMode(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 1 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.SkateMode + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Skate Mod ^3On");
                    }

                    public static void SpectatorGodMod(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 0x18 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.SpectatorGodMod + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Spectator God Mode ^3On");
                    }

                    public static void ThirdPerson(int ClientInt)
                    {
                        Mw2Library.RPC.SetClientDvars(ClientInt, "camera_thirdPerson 1");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Third Person ^3On");
                    }

                    public static void UAV(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 1 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.UAV.GiveUAV + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2UAV ^3On");
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.UAV.CounterUAV + ((uint) (ClientInt * 0x3700)), new byte[1]);
                    }

                    public static void UltimateAmmo(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 15, 0xff, 0xff, 0xff };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.PrimaryBullets + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.PrimaryClip + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.SecondaryBullets + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.SecondaryClip + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.Lethal + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.Tactical + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.AkimboPrimaryBullets + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.AkimboSecondaryWeapon + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.GranadeLuncherBullets + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.GranadeLuncherClip + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Ultimate Ammo ^3On");
                    }

                    public static void Wallhack(int ClientInt)
                    {
                        Mw2Library.RPC.SetClientDvars(ClientInt, "r_znear 57");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Wallhack ^3On");
                    }

                    public static class AkimboWeapons
                    {
                        public static void Primary(int ClientInt)
                        {
                            byte[] buffer = new byte[] { 1 };
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Akimbo.Primary + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Akimbo Primary Weapon's: ^3On");
                        }

                        public static void Secondary(int ClientInt)
                        {
                            byte[] buffer = new byte[] { 1 };
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Akimbo.Secondary + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Akimbo Secondary Weapon's: ^3On");
                        }
                    }

                    public static class GunCamo
                    {
                        public static class Primary
                        {
                            public static void Arctic(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Arctic);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^7Arctic");
                            }

                            public static void BlueTiger(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.BlueTiger);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^4Blue Tiger!");
                            }

                            public static void Desert(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Desert);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^3Desert");
                            }

                            public static void Digital(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Digital);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^0Digital!");
                            }

                            public static void Fall(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Fall);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^3Fall!");
                            }

                            public static void Nothing(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Nothing);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Removed!");
                            }

                            public static void RedTiger(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.RedTiger);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^1Red Tiger!");
                            }

                            public static void Urban(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Urban);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^6Urban!");
                            }

                            public static void Woodland(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Primary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Woodland);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Primary Weapon Camo Changed To: ^3Desert");
                            }
                        }

                        public static class Secondary
                        {
                            public static void Arctic(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Arctic);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^7Arctic");
                            }

                            public static void BlueTiger(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.BlueTiger);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^4Blue Tiger!");
                            }

                            public static void Desert(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Desert);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^3Desert");
                            }

                            public static void Digital(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Digital);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^0Digital!");
                            }

                            public static void Fall(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Fall);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^3Fall!");
                            }

                            public static void Nothing(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Nothing);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Removed!");
                            }

                            public static void RedTiger(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.RedTiger);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^1Red Tiger!");
                            }

                            public static void Urban(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Urban);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^6Urban!");
                            }

                            public static void Woodland(int ClientInt)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.GunCamo.Secondary + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.GunCamo.Camos.Woodland);
                                Mw2Library.RPC.iPrintln(ClientInt, "^2Secondary Weapon Camo Changed To: ^3Desert");
                            }
                        }
                    }

                    public static class mFlag
                    {
                        public static void Default(int ClientInt)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.mFlag.Offset + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.mFlag.Flags.Default);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2mFlag ^3Reset!");
                        }

                        public static void Freeze(int ClientInt)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.mFlag.Offset + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.mFlag.Flags.Freeze);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Freeze ^3On");
                        }

                        public static void NoClip(int ClientInt)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.mFlag.Offset + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.mFlag.Flags.NoClip);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2No Clip ^3On");
                        }

                        public static void UFOMOD(int ClientInt)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.mFlag.Offset + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.mFlag.Flags.UFOMOD);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2UFO MOD ^3On");
                        }
                    }

                    public static class NightVision_Dpad
                    {
                        public static void Down(int ClientInt)
                        {
                            byte[] buffer = new byte[] { 15 };
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Down + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Press ^1Down ^3To Turn Night Vision On!");
                        }

                        public static void Left(int ClientInt)
                        {
                            byte[] buffer = new byte[] { 15 };
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Left + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Press ^1Left ^3To Turn Night Vision On!");
                        }

                        public static void Right(int ClientInt)
                        {
                            byte[] buffer = new byte[] { 15 };
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Right + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Press ^1Right ^3To Turn Night Vision On!");
                        }

                        public static void Up(int ClientInt)
                        {
                            byte[] buffer = new byte[] { 15 };
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Up + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Press ^1Up ^3To Turn Night Vision On!");
                        }
                    }
                }

                public static class MovementFlags
                {
                    public static void AutoProne(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.MovementFlags.Movement + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.MovementFlags.Flags.AutoProne);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Auto Prone ^3On");
                    }

                    public static void Default(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.MovementFlags.Movement + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.MovementFlags.Flags.Default);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Movement Back To ^3Default");
                    }

                    public static void DisableJump(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.MovementFlags.Movement + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.MovementFlags.Flags.DisableJump);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Jump ^3Disabled");
                    }

                    public static void DisableSprint(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.MovementFlags.Movement + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.MovementFlags.Flags.DisableSprint);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Sprint ^3Disabled");
                    }
                }

                public static class Reset
                {
                    public static void AkimboWeapons(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Akimbo.Primary + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Akimbo.Secondary + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Akimbo Weapon's: ^3Off!");
                    }

                    public static void AllPerks(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 
                            0x60, 0, 0, 0, 0, 8, 0, 0, 0, 3, 0, 0, 0, 6, 0, 0, 
                            0, 11, 0, 0, 0, 0, 0, 0, 0, 0x1b, 0, 0, 0, 0x18, 0, 0, 
                            0, 0x21, 0, 0, 0, 40, 0, 0, 0
                         };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.AllPerks + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2All Perks ^3Off!");
                    }

                    public static void CounterUAV(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.UAV.CounterUAV + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Counter UAV ^3Off");
                    }

                    public static void ExplosiveBullets(int ClientInt)
                    {
                        byte[] buffer2 = new byte[2];
                        buffer2[1] = 0x40;
                        byte[] buffer = buffer2;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.ExplosiveBullets + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Explosive Bullets ^3Off");
                    }

                    public static void FieldOfView(int ClientInt)
                    {
                        Mw2Library.RPC.SetClientDvars(ClientInt, "cg_fov 65");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Field Of View Is Back To ^3Default!");
                    }

                    public static void GodMode(int ClientInt)
                    {
                        byte[] buffer = new byte[2];
                        buffer[1] = 100;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Health1 + ((uint) (ClientInt * 0x3700)), buffer);
                        buffer = new byte[4];
                        buffer[3] = 100;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Health2 + ((uint) (ClientInt * 0x3700)), buffer);
                        buffer = new byte[4];
                        buffer[3] = 100;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Health3 + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2God Mode ^3Reset!");
                    }

                    public static void Lag(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 2 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Lag + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Lag ^3Off");
                    }

                    public static void LittleCrosshair(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.LittleCrosshair + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Little Crosshair: ^3Off!");
                    }

                    public static void mFlag(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.mFlag.Offset + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.mFlag.Flags.Default);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2mFlag ^3Reset!");
                    }

                    public static void NightVision_Dpad(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Right + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Down + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Left + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.NightVision.Up + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Night Vision D_Pad's - ^3Reset!");
                    }

                    public static void RedBoxes(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.RedBoxes + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Red Boxes ^3Off!");
                    }

                    public static void SkateMode(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.SkateMode + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Skate Mod ^3Off");
                    }

                    public static void SpectatorGodMod(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 0x10 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.SpectatorGodMod + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Spectator God Mode ^3Off");
                    }

                    public static void ThirdPerson(int ClientInt)
                    {
                        Mw2Library.RPC.SetClientDvars(ClientInt, "camera_thirdPerson 0");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Third Person ^3Off!");
                    }

                    public static void UAV(int ClientInt)
                    {
                        byte[] buffer = new byte[1];
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.UAV.GiveUAV + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.UAV.CounterUAV + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2UAV ^3Reset!");
                    }

                    public static void UltimateAmmo(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 15, 0xff, 0xff, 0xff };
                        byte[] buffer2 = new byte[4];
                        buffer2[3] = 30;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.PrimaryBullets + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 30;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.PrimaryClip + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 30;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.SecondaryBullets + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 30;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.SecondaryClip + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 4;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.Lethal + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 4;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.Tactical + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 30;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.AkimboPrimaryBullets + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 30;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.AkimboSecondaryWeapon + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 2;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.GranadeLuncherBullets + ((uint) (ClientInt * 0x3700)), buffer2);
                        buffer2 = new byte[4];
                        buffer2[3] = 5;
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Ammo.GranadeLuncherClip + ((uint) (ClientInt * 0x3700)), buffer2);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Ultimate Ammo ^3Reset!");
                    }

                    public static void Wallhack(int ClientInt)
                    {
                        Mw2Library.RPC.SetClientDvars(ClientInt, "r_znear 1");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Wallhack ^3Off!");
                    }
                }

                public static class Team
                {
                    public static void FFA(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Team.ChangeTeam + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.Team.Choose.FFA);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Team Changed To: ^3Free For All");
                    }

                    public static void OpFor(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Team.ChangeTeam + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.Team.Choose.OpFor);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Team Changed To: ^3Enemies");
                    }

                    public static void Rangers(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Team.ChangeTeam + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.Team.Choose.Rangers);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Team Changed To: ^2Friendly");
                    }

                    public static void Spectator(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Team.ChangeTeam + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.Team.Choose.Spectator);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Team Changed To: ^3Spectator");
                    }
                }

                public static class Teleport
                {
                    public static void ClientToHost(int ClientInt)
                    {
                        byte[] input = Mw2Library.PS3.Extension.ReadBytes(Mw2Library.Offsets.Clients.Teleport.Location, 0x16);
                        Mw2Library.PS3.Extension.WriteBytes(Mw2Library.Offsets.Clients.Teleport.Location + ((uint) (ClientInt * 0x3700)), input);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Teleported To Host");
                    }

                    public static void ClientToPosition(int ClientInt)
                    {
                        byte[] input = File.ReadAllBytes("Mw2-Position.txt");
                        Mw2Library.PS3.Extension.WriteBytes(Mw2Library.Offsets.Clients.Teleport.Location + ((uint) (ClientInt * 0x3700)), input);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Teleported To Saved Position!");
                    }

                    public static void ClientToSky(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 70 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Teleport.Height + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Teleport To Sky!");
                    }

                    public static void ClientToSpace(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 0x79 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Teleport.Height + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Teleported To Space!");
                    }

                    public static void ClientUnderMap(int ClientInt)
                    {
                        byte[] buffer = new byte[] { 0x42 };
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.Teleport.Height + ((uint) (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Teleported Under Map!");
                    }

                    public static void EveryoneToClient(int ClientInt)
                    {
                        byte[] input = Mw2Library.PS3.Extension.ReadBytes(Mw2Library.Offsets.Clients.Teleport.Location + ((uint) (ClientInt * 0x3700)), 0x16);
                        for (uint i = 0; i < 0x12; i++)
                        {
                            uint offset = Mw2Library.Offsets.Clients.Teleport.Location + (0x3700 * i);
                            Mw2Library.PS3.Extension.WriteBytes(offset, input);
                            Mw2Library.RPC.iPrintln((int) i, "^1All Player's Teleport: ^2On");
                        }
                    }

                    public static void EveryoneToHost(int ClientInt)
                    {
                        byte[] input = Mw2Library.PS3.Extension.ReadBytes(Mw2Library.Offsets.Clients.Teleport.Location, 0x16);
                        for (uint i = 0; i < 12; i++)
                        {
                            uint offset = Mw2Library.Offsets.Clients.Teleport.Location + (0x3700 * i);
                            Mw2Library.PS3.Extension.WriteBytes(offset, input);
                            Mw2Library.RPC.iPrintln((int) i, "^1All Player's Teleported To Host!");
                        }
                    }

                    public static void EveryoneToPosition(int ClientInt)
                    {
                        byte[] input = File.ReadAllBytes("Mw2-Position.txt");
                        for (uint i = 0; i < 0x12; i++)
                        {
                            uint offset = Mw2Library.Offsets.Clients.Teleport.Location + (0x3700 * i);
                            Mw2Library.PS3.Extension.WriteBytes(offset, input);
                            Mw2Library.RPC.iPrintln((int) i, "^1All Player's Teleport To Saved Location!");
                        }
                    }

                    public static void EveryoneToPositionAuto(int ClientInt)
                    {
                        byte[] input = File.ReadAllBytes("Mw2-Position.txt");
                        for (uint i = 0; i < 0x12; i++)
                        {
                            uint offset = Mw2Library.Offsets.Clients.Teleport.Location + (0x3700 * i);
                            Mw2Library.PS3.Extension.WriteBytes(offset, input);
                        }
                    }

                    public static void EveryoneToSky(int ClientInt)
                    {
                        byte[] input = new byte[] { 70 };
                        for (uint i = 0; i < 0x12; i++)
                        {
                            uint offset = Mw2Library.Offsets.Clients.Teleport.Height + (0x3700 * i);
                            Mw2Library.PS3.Extension.WriteBytes(offset, input);
                            Mw2Library.RPC.iPrintln((int) i, "^1All Player's Teleport To: ^2Sky!");
                        }
                    }

                    public static void EveryoneToSpace(int ClientInt)
                    {
                        byte[] input = new byte[] { 0x79 };
                        for (uint i = 0; i < 0x12; i++)
                        {
                            uint offset = Mw2Library.Offsets.Clients.Teleport.Height + (0x3700 * i);
                            Mw2Library.PS3.Extension.WriteBytes(offset, input);
                            Mw2Library.RPC.iPrintln((int) i, "^1All Player's Teleport To: ^2Space!");
                        }
                    }

                    public static void HostToClient(int ClientInt)
                    {
                        byte[] input = Mw2Library.PS3.Extension.ReadBytes(Mw2Library.Offsets.Clients.Teleport.Location + ((uint) (ClientInt * 0x3700)), 0x16);
                        Mw2Library.PS3.Extension.WriteBytes(Mw2Library.Offsets.Clients.Teleport.Location, input);
                        Mw2Library.RPC.iPrintln(0, "^2Teleported To Client");
                    }

                    public static void SavePosition(int ClientInt)
                    {
                        Mw2Library.RPC.iPrintln(ClientInt, "^5Current Position Saved!!!");
                        byte[] bytes = Mw2Library.PS3.Extension.ReadBytes(Mw2Library.Offsets.Clients.Teleport.Location + ((uint) (ClientInt * 0x3700)), 0x16);
                        File.WriteAllBytes("Mw2-Position.txt", bytes);
                    }
                }

                public static class Visions
                {
                    public static void AC130(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "ac130");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3AC-130 Vision!");
                    }

                    public static void Chapelin(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "cheat_chaplinnight");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Chapelin!");
                    }

                    public static void CoDFourVision(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "cargoship_indoor");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3CoD 4 Vision!");
                    }

                    public static void Default(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "default");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Default!");
                    }

                    public static void Explosion(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "cheat_contrast");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Explosion!");
                    }

                    public static void Flash(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "dcemp_emp");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Flash!");
                    }

                    public static void MissileCam(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "missilecam");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Missile Cam!");
                    }

                    public static void OldMovie(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "mpintro");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Old Movie!");
                    }

                    public static void Sepia(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "sepia");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Sepia!");
                    }

                    public static void Sunrise(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "dc_whitehouse_lawn");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Sunrise!");
                    }

                    public static void Thermal(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "thermal_mp");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Thermal!");
                    }

                    public static void Water(int ClientInt)
                    {
                        Mw2Library.RPC.Vision(ClientInt, "oilrig_underwater");
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Water!");
                    }

                    public static class BlackAndWhite
                    {
                        public static void Black(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "black_bw");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Black!");
                        }

                        public static void StrongBlackAndWhite(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cheat_bw_invert_contrast");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^5Strong ^3Black And White!");
                        }

                        public static void White(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cargoship_blast");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3White!");
                        }

                        public static void WhiteAndBlack(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cheat_bw_contrast");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Black And White!");
                        }
                    }

                    public static class CobraSunset
                    {
                        public static void One(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cobra_sunset1");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Cobra Sunset - 1!");
                        }

                        public static void Three(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cobra_sunset3");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Cobra Sunset - 3!");
                        }

                        public static void Two(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cobra_sunset2");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Cobra Sunset - 2!");
                        }
                    }

                    public static class Colors
                    {
                        public static void Blue(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cheat_invert_contrast");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Blue!");
                        }

                        public static void Green(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "airport_green");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Green!");
                        }

                        public static void Red(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "end_game");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Red!");
                        }

                        public static void Yellow(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "overwatch_nv");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Yellow!");
                        }
                    }

                    public static class Inverted
                    {
                        public static void Normal(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "ac130_inverted");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Inverted!");
                        }

                        public static void Strong(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "cheat_bw_invert");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Strong Inverted!");
                        }
                    }

                    public static class Night
                    {
                        public static void Fake(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "default_night_mp");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Fake Night Vision!");
                        }

                        public static void Real(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "default_night");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Real Night Vision!");
                        }
                    }

                    public static class Nuke
                    {
                        public static void Aftermath(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "aftermath");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Aftermath!");
                        }

                        public static void Nuke_mp(int ClientInt)
                        {
                            Mw2Library.RPC.Vision(ClientInt, "mpnuke");
                            Mw2Library.RPC.iPrintln(ClientInt, "^2Vision Set To: ^3Nuke!");
                        }
                    }
                }

                public static class Weapon
                {
                    public static void DefaultWeapon(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory((uint) (0x14e259a + (ClientInt * 0x3700)), new byte[] { 0, 1, 15, 0xff, 0xff, 0xff });
                        byte[] buffer = new byte[2];
                        buffer[1] = 1;
                        Mw2Library.PS3.SetMemory((uint) (0x14e2422 + (ClientInt * 0x3700)), buffer);
                        buffer = new byte[2];
                        buffer[1] = 1;
                        Mw2Library.PS3.SetMemory((uint) (0x14e24b6 + (ClientInt * 0x3700)), buffer);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Default Weapon ^1Given!");
                    }

                    public static void GoldDesertEagle(int ClientInt)
                    {
                        byte[] buffer;
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            buffer = new byte[2];
                            buffer[1] = 0x2e;
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Weapon1 + ((uint) (ClientInt * 0x3700)), buffer);
                            buffer = new byte[2];
                            buffer[1] = 0x2e;
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Weapon2 + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Ammo1 + ((uint) (ClientInt * 0x3700)), new byte[] { 0, 0x24, 15, 0xff, 0xff, 0xff });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Ammo2 + ((uint) (ClientInt * 0x3700)), new byte[] { 0, 0x24, 15, 0xff, 0xff, 0xff });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.GiveWeapon.NoWeapons);
                            Mw2Library.RPC.iPrintln(ClientInt, "^3Gold Desert Eagle ^1Given!");
                        }
                        else
                        {
                            buffer = new byte[2];
                            buffer[1] = 0x2d;
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Weapon1 + ((uint) (ClientInt * 0x3700)), buffer);
                            buffer = new byte[2];
                            buffer[1] = 0x2d;
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Weapon2 + ((uint) (ClientInt * 0x3700)), buffer);
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Ammo1 + ((uint) (ClientInt * 0x3700)), new byte[] { 0, 0x23, 15, 0xff, 0xff, 0xff });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.GoldEagle.Ammo2 + ((uint) (ClientInt * 0x3700)), new byte[] { 0, 0x23, 15, 0xff, 0xff, 0xff });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.GiveWeapon.NoWeapons);
                            Mw2Library.RPC.iPrintln(ClientInt, "^3Gold Desert Eagle ^1Given!");
                        }
                    }

                    public static void M16A4Noobtube(int ClientInt)
                    {
                        if (Mw2Library.Mods.Clients.SetUp1)
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 1, 0x67 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 1, 0x67 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 0, 0xaf, 15, 0xff, 0xff, 0xff });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.TakeSecondary + ((uint) (ClientInt * 0x3700)), new byte[2]);
                            Mw2Library.RPC.iPrintln(ClientInt, "^3M16A4 ^2Noobtube ^1Given!");
                        }
                        else
                        {
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 1, 0x66 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 1, 0x66 });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 0, 0xae, 15, 0xff, 0xff, 0xff });
                            Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.M16A4Noobtube.TakeSecondary + ((uint) (ClientInt * 0x3700)), new byte[2]);
                            Mw2Library.RPC.iPrintln(ClientInt, "^3M16A4 ^2Noobtube ^1Given!");
                        }
                    }

                    public static void TakeWeapons(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.GiveWeapon.NoWeapons);
                    }

                    public static class AC130
                    {
                        public static void _105mm(int ClientInt)
                        {
                            if (Mw2Library.Mods.Clients.SetUp1)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x99 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x99 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x22, 15, 0xff, 0xff, 0xff });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.GiveWeapon.NoWeapons);
                                Mw2Library.RPC.iPrintln(ClientInt, "^1AC-130: ^2105mm ^1- ^3Given!");
                            }
                            else
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x98 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x98 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x21, 15, 0xff, 0xff, 0xff });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.GiveWeapon.NoWeapons);
                                Mw2Library.RPC.iPrintln(ClientInt, "^1AC-130: ^2105mm ^1- ^3Given!");
                            }
                        }

                        public static void _25mm(int ClientInt)
                        {
                            if (Mw2Library.Mods.Clients.SetUp1)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x97 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x97 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x20, 15, 0xff, 0xff, 0xff });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), new byte[11]);
                                Mw2Library.RPC.iPrintln(ClientInt, "^1AC-130: ^225mm ^1- ^3Given!");
                            }
                            else
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 150 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 150 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x1f, 15, 0xff, 0xff, 0xff });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), new byte[11]);
                                Mw2Library.RPC.iPrintln(ClientInt, "^1AC-130: ^225mm ^1- ^3Given!");
                            }
                        }

                        public static void _40mm(int ClientInt)
                        {
                            if (Mw2Library.Mods.Clients.SetUp1)
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x98 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x98 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x21, 15, 0xff, 0xff, 0xff });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.GiveWeapon.NoWeapons);
                                Mw2Library.RPC.iPrintln(ClientInt, "^1AC-130: ^240mm ^1- ^3Given!");
                            }
                            else
                            {
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon1 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x97 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Weapon2 + ((uint) (ClientInt * 0x3700)), new byte[] { 4, 0x97 });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.Ammo + ((uint) (ClientInt * 0x3700)), new byte[] { 2, 0x20, 15, 0xff, 0xff, 0xff });
                                Mw2Library.PS3.SetMemory(Mw2Library.Offsets.GiveWeapon.TakeWeapons + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.GiveWeapon.NoWeapons);
                                Mw2Library.RPC.iPrintln(ClientInt, "^1AC-130: ^240mm ^1- ^3Given!");
                            }
                        }
                    }
                }

                public static class WeaponFlags
                {
                    public static void Default(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.WeaponFlags.Weapon + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.WeaponFlags.Flags.Default);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Weapon Flag Set To: ^3Default!");
                    }

                    public static void DisableADS(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.WeaponFlags.Weapon + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.WeaponFlags.Flags.DisableADS);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2ADS: ^3Disabled!");
                    }

                    public static void DisableWeapons(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.WeaponFlags.Weapon + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.WeaponFlags.Flags.DisableWeapons);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Weapons: ^3Disabled!");
                    }

                    public static void DisableWeaponsSwitch(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.WeaponFlags.Weapon + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.WeaponFlags.Flags.DisableWeaponsSwitch);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2Weapons Switch: ^3Disabled!");
                    }

                    public static void NoRecoil(int ClientInt)
                    {
                        Mw2Library.PS3.SetMemory(Mw2Library.Offsets.Clients.WeaponFlags.Weapon + ((uint) (ClientInt * 0x3700)), Mw2Library.Offsets.Clients.WeaponFlags.Flags.NoRecoil);
                        Mw2Library.RPC.iPrintln(ClientInt, "^2No Recoil: ^3On!");
                    }
                }
            }

            public static class Lobby
            {
                public static void fallDamageMax(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("bg_fallDamageMaxHeight " + text);
                }

                public static void fallDamageMin(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("bg_fallDamageMinHeight " + text);
                }

                public static void GravityScale(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("g_gravity " + text);
                }

                public static void JumpHeight(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("jump_height " + text);
                }

                public static void KnockBack(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("g_knockback " + text);
                }

                public static void MeeleRange(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("player_meleeRange " + text);
                    Mw2Library.RPC.Cbuf_AddText("player_meleeHeight " + text);
                    Mw2Library.RPC.Cbuf_AddText("player_meleeWidth " + text);
                }

                public static void SpeedScale(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("g_speed " + text);
                }

                public static void TimeScale(decimal text)
                {
                    Mw2Library.RPC.Cbuf_AddText("fixedtime " + text);
                }
            }
        }

        public static class Offsets
        {
            public static uint Name = 0x1f9f11c;

            public static class Addresses
            {
                public static uint G_Client = 0x14e2200;
                public static uint G_Entity = 0x1319780;
                public static uint G_SetModel = 0x1be3f0;
            }

            public static class Bullets
            {
                public static uint Ammo = 0x14e259a;
                public static uint Type1 = 0x14e2422;
                public static uint Type2 = 0x14e24b6;
            }

            public static class Clients
            {
                public static uint Alive = 0x14e5393;
                public static uint AllPerks = 0x14e262a;
                public static uint AutoKill = 0x1319901;
                public static uint ClanTagInGame = 0x14e54cc;
                public static uint ExplosiveBullets = 0x14e2628;
                public static uint Health1 = 0x14e5429;
                public static uint Health2 = 0x14e235a;
                public static uint Health3 = 0x14e2398;
                public static uint Index = 0x3700;
                public static uint Kill = 0x14e2220;
                public static uint KillAndScare = 0x14e2224;
                public static uint Lag = 0x14e53af;
                public static uint LittleCrosshair = 0x14e24d3;
                public static uint NameInGame = 0x14e5490;
                public static uint PlayerSpeed = 0x14e543c;
                public static uint RedBoxes = 0x14e2213;
                public static uint ShowMeTheMap = 0x14e23b7;
                public static uint SkateMode = 0x14e220e;
                public static uint SpectatorGodMod = 0x14e2212;

                public static class Akimbo
                {
                    public static uint Primary = 0x14e2467;
                    public static uint Secondary = 0x14e245d;
                }

                public static class Ammo
                {
                    public static uint AkimboPrimaryBullets = 0x14e2570;
                    public static uint AkimboSecondaryWeapon = 0x14e2558;
                    public static uint GranadeLuncherBullets = 0x14e2578;
                    public static uint GranadeLuncherClip = 0x14e24f4;
                    public static uint Lethal = 0x14e2560;
                    public static uint PrimaryBullets = 0x14e256c;
                    public static uint PrimaryClip = 0x14e24ec;
                    public static uint SecondaryBullets = 0x14e2554;
                    public static uint SecondaryClip = 0x14e24dc;
                    public static uint Tactical = 0x14e2578;
                }

                public static class ButtonsIcones
                {
                    public static uint Down = 0x14e54ff;
                    public static uint Left = 0x14e5503;
                    public static uint Lethal = 0x14e54f3;
                    public static uint Right = 0x14e5507;
                    public static uint Tactical = 0x14e54f7;
                    public static uint Up = 0x14e54fb;
                }

                public static class GunCamo
                {
                    public static uint Primary = 0x14e2468;
                    public static uint Secondary = 0x14e245e;

                    public static class Camos
                    {
                        public static byte[] Arctic = new byte[] { 3 };
                        public static byte[] BlueTiger = new byte[] { 7 };
                        public static byte[] Desert = new byte[] { 2 };
                        public static byte[] Digital = new byte[] { 4 };
                        public static byte[] Fall = new byte[] { 8 };
                        public static byte[] Nothing = new byte[1];
                        public static byte[] RedTiger = new byte[] { 6 };
                        public static byte[] Urban = new byte[] { 5 };
                        public static byte[] Woodland = new byte[] { 1 };
                    }
                }

                public static class mFlag
                {
                    public static uint Offset = 0x14e5623;

                    public static class Flags
                    {
                        public static byte[] Default = new byte[1];
                        public static byte[] Freeze = new byte[] { 6 };
                        public static byte[] NoClip = new byte[] { 1 };
                        public static byte[] UFOMOD = new byte[] { 2 };
                    }
                }

                public static class MovementFlags
                {
                    public static uint Movement = 0x14e220d;

                    public static class Flags
                    {
                        public static byte[] AutoProne = new byte[] { 0x55 };
                        public static byte[] Default = new byte[1];
                        public static byte[] DisableJump = new byte[] { 4 };
                        public static byte[] DisableSprint = new byte[] { 2 };
                    }
                }

                public static class NightVision
                {
                    public static uint Down = 0x14e2657;
                    public static uint Left = 0x14e265b;
                    public static uint Right = 0x14e265f;
                    public static uint Up = 0x14e2653;
                }

                public static class Status
                {
                    public static uint Assists = 0x14e53a4;
                    public static uint Deaths = 0x14e539c;
                    public static uint Kills = 0x14e53a0;
                    public static uint Prestige = 0x14e54bb;
                    public static uint Score = 0x14e5398;
                    public static uint XP = 0x14e54b7;
                }

                public static class Team
                {
                    public static uint ChangeTeam = 0x14e5453;

                    public static class Choose
                    {
                        public static byte[] FFA = new byte[1];
                        public static byte[] OpFor = new byte[] { 1 };
                        public static byte[] Rangers = new byte[] { 2 };
                        public static byte[] Spectator = new byte[] { 3 };
                    }
                }

                public static class Teleport
                {
                    public static uint Height = 0x14e2224;
                    public static uint Location = 0x14e221b;
                }

                public static class UAV
                {
                    public static uint CounterUAV = 0x14e54eb;
                    public static uint GiveUAV = 0x14e54e6;
                }

                public static class WeaponFlags
                {
                    public static uint Weapon = 0x14e24be;

                    public static class Flags
                    {
                        public static byte[] Default;
                        public static byte[] DisableADS;
                        public static byte[] DisableWeapons;
                        public static byte[] DisableWeaponsSwitch = new byte[] { 8 };
                        public static byte[] NoRecoil;

                        static Flags()
                        {
                            byte[] buffer = new byte[2];
                            buffer[1] = 0x80;
                            DisableWeapons = buffer;
                            buffer = new byte[2];
                            buffer[1] = 0x20;
                            DisableADS = buffer;
                            NoRecoil = new byte[] { 4 };
                            buffer = new byte[2];
                            Default = buffer;
                        }
                    }
                }
            }

            public static class GiveWeapon
            {
                public static uint Ammo = 0x14e259a;
                public static byte[] NoWeapons = new byte[11];
                public static uint TakeWeapons = 0x14e2426;
                public static uint Weapon1 = 0x14e2422;
                public static uint Weapon2 = 0x14e24b6;

                public static class GoldEagle
                {
                    public static uint Ammo1 = 0x14e24da;
                    public static uint Ammo2 = 0x14e2552;
                    public static uint Weapon1 = 0x14e24b6;
                    public static uint Weapon2 = 0x14e2422;
                }

                public static class M16A4Noobtube
                {
                    public static uint Ammo = 0x14e256a;
                    public static uint TakeSecondary = 0x14e2422;
                    public static uint Weapon1 = 0x14e242a;
                    public static uint Weapon2 = 0x14e24b6;
                }
            }

            public static class Models
            {
                public static byte[] AC130;
                public static byte[] AC130_Coop;
                public static byte[] AttachHelicopter_2;
                public static byte[] AttachHelicopter_3;
                public static byte[] AttachHelicopter_4;
                public static byte[] AttackHelicopter;
                public static byte[] BlackBall;
                public static byte[] Bomb_Close;
                public static byte[] Bomb_Open;
                public static byte[] Box;
                public static byte[] C4;
                public static byte[] CarePackage_Enemy;
                public static byte[] CarePackage_Friendly;
                public static byte[] Claymore;
                public static byte[] Default_TF141;
                public static byte[] Default_Vehicle;
                public static byte[] FakeCarePackage;
                public static byte[] Harriar_Black;
                public static byte[] Harriar_White;
                public static byte[] Intel;
                public static byte[] Invisible;
                public static byte[] Laptop;
                public static byte[] LittleBird;
                public static byte[] Mig29;
                public static byte[] Minigun;
                public static byte[] Missile;
                public static uint Model = 0x1319968;
                public static byte[] Netrual_Flag;
                public static byte[] One_Man_Army;
                public static byte[] Pavelow_Black;
                public static byte[] Pavelow_White;
                public static byte[] S_And_D_Bomb;
                public static byte[] S_And_D_Bomb_Ruind;
                public static byte[] Scavenger;
                public static byte[] Sealth_Bomber;
                public static byte[] SentryGun;
                public static byte[] SentryGun_Broken;
                public static byte[] SentryGun_Gold;
                public static byte[] SentryGun_Red;
                public static byte[] Shield;
                public static byte[] ShieldGuy;
                public static byte[] UAV;

                static Models()
                {
                    byte[] buffer = new byte[2];
                    buffer[1] = 0x3d;
                    Default_TF141 = buffer;
                    buffer = new byte[2];
                    buffer[1] = 2;
                    Default_Vehicle = buffer;
                    buffer = new byte[2];
                    buffer[1] = 3;
                    FakeCarePackage = buffer;
                    buffer = new byte[2];
                    buffer[1] = 4;
                    S_And_D_Bomb = buffer;
                    buffer = new byte[2];
                    buffer[1] = 5;
                    S_And_D_Bomb_Ruind = buffer;
                    buffer = new byte[2];
                    buffer[1] = 7;
                    Netrual_Flag = buffer;
                    buffer = new byte[2];
                    buffer[1] = 8;
                    Box = buffer;
                    buffer = new byte[2];
                    buffer[1] = 9;
                    Laptop = buffer;
                    buffer = new byte[2];
                    buffer[1] = 11;
                    Mig29 = buffer;
                    buffer = new byte[2];
                    buffer[1] = 12;
                    Missile = buffer;
                    buffer = new byte[2];
                    buffer[1] = 0x1b;
                    ShieldGuy = buffer;
                    C4 = new byte[] { 1, 0x4a };
                    Claymore = new byte[] { 1, 0x4d };
                    Scavenger = new byte[] { 1, 0x4f };
                    One_Man_Army = new byte[] { 1, 0x58 };
                    Bomb_Close = new byte[] { 1, 0x66 };
                    Bomb_Open = new byte[] { 1, 0x6d };
                    Intel = new byte[] { 1, 0x67 };
                    AC130_Coop = new byte[] { 1, 0x68 };
                    BlackBall = new byte[] { 1, 0x6b };
                    UAV = new byte[] { 1, 0x6c };
                    Harriar_White = new byte[] { 1, 110 };
                    Harriar_Black = new byte[] { 1, 0x6f };
                    Minigun = new byte[] { 1, 0x70 };
                    Sealth_Bomber = new byte[] { 1, 0x71 };
                    CarePackage_Friendly = new byte[] { 1, 0x73 };
                    CarePackage_Enemy = new byte[] { 1, 0x74 };
                    LittleBird = new byte[] { 1, 0x75 };
                    AC130 = new byte[] { 1, 0x76 };
                    AttackHelicopter = new byte[] { 1, 0x7a };
                    AttachHelicopter_2 = new byte[] { 1, 0x7b };
                    AttachHelicopter_3 = new byte[] { 1, 0x7c };
                    AttachHelicopter_4 = new byte[] { 1, 0x7d };
                    Pavelow_White = new byte[] { 1, 0x7e };
                    Pavelow_Black = new byte[] { 1, 0x7f };
                    SentryGun = new byte[] { 1, 0x80 };
                    SentryGun_Gold = new byte[] { 1, 0x81 };
                    SentryGun_Red = new byte[] { 1, 130 };
                    SentryGun_Broken = new byte[] { 1, 0x83 };
                    Shield = new byte[] { 1, 0x84 };
                    buffer = new byte[2];
                    Invisible = buffer;
                }

                public static void SetModel(uint client, byte[] Bytes)
                {
                    Mw2Library.PS3.SetMemory(Model + (client * 0x3700), Bytes);
                }

                public static void TestSpawn(uint client)
                {
                    SetModel(client, CarePackage_Friendly);
                }
            }

            public static class StatusInGame
            {
                public static uint Assists = 0x14e53a4;
                public static uint Deaths = 0x14e539c;
                public static uint Kills = 0x14e53a0;
                public static uint Level = 0x14e54b7;
                public static uint Prestige = 0x14e54bb;
                public static uint Score = 0x14e5398;
            }
        }

        public static class RPC
        {
            public static PS3API PS3 = new PS3API(SelectAPI.TargetManager);

            public static void Bots(int numberOfTestClients)
            {
                CallFunction.Call(0x2189d8, new object[] { numberOfTestClients });
            }

            public static void Cbuf_AddText(string text)
            {
                byte[] buffer = new byte[0x100];
                byte[] buffer2 = new byte[] { 0x38, 0x60, 0, 0, 60, 0x80, 2, 0, 0x30, 0x84, 80, 0, 0x4b, 0xf8, 0x63, 0xfd };
                byte[] buffer3 = new byte[] { 0x81, 0x22, 0x45, 0x10, 0x81, 0x69, 0, 0, 0x88, 11, 0, 12, 0x2f, 0x80, 0, 0 };
                byte[] bytes = new byte[0];
                bytes = Encoding.UTF8.GetBytes(text);
                PS3.SetMemory(0x2005000, bytes);
                PS3.SetMemory(0x253ab8, buffer2);
                Thread.Sleep(15);
                PS3.SetMemory(0x253ab8, buffer3);
                PS3.SetMemory(0x2005000, buffer);
            }

            public static void Chat(int client, string message)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "h \"" + message);
            }

            public static void FreezePS3(int client, string message)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "a \"" + message);
            }

            public static uint G_Client(int clientIndex)
            {
                return (uint) (0x14e2200 + (0x3700 * clientIndex));
            }

            public static uint G_Entity(int clientIndex)
            {
                return (uint) (0x1319800 + (640 * clientIndex));
            }

            public static string get_MapName()
            {
                string str = PS3.Extension.ReadString(0xd495f4);
                string str2 = "/";
                if (InGame())
                {
                    if (str.Contains("afghan"))
                    {
                        str2 = "Afghan";
                    }
                    if (str.Contains("highrise"))
                    {
                        str2 = "Highrise";
                    }
                    if (str.Contains("rundown"))
                    {
                        str2 = "Rundown";
                    }
                    if (str.Contains("quarry"))
                    {
                        str2 = "Quarry";
                    }
                    if (str.Contains("nightshift"))
                    {
                        str2 = "Skidrow";
                    }
                    if (str.Contains("terminal"))
                    {
                        str2 = "Terminal";
                    }
                    if (str.Contains("brecourt"))
                    {
                        str2 = "Wasteland";
                    }
                    if (str.Contains("derail"))
                    {
                        str2 = "Derail";
                    }
                    if (str.Contains("estate"))
                    {
                        str2 = "Estate";
                    }
                    if (str.Contains("favela"))
                    {
                        str2 = "Favela";
                    }
                    if (str.Contains("invasion"))
                    {
                        str2 = "Invasion";
                    }
                    if (str.Contains("rust"))
                    {
                        str2 = "Rust";
                    }
                    if (str.Contains("scrapyard") || str.Contains("boneyard"))
                    {
                        str2 = "Scrapyard";
                    }
                    if (str.Contains("sub"))
                    {
                        str2 = "Subbase";
                    }
                    if (str.Contains("underpass"))
                    {
                        str2 = "Underpass";
                    }
                    if (str.Contains("checkpoint"))
                    {
                        str2 = "Karachi";
                    }
                    if (str.Contains("bailout"))
                    {
                        str2 = "Bailout";
                    }
                    if (str.Contains("compact"))
                    {
                        str2 = "Salvage";
                    }
                    if (str.Contains("storm") || str.Contains("storm2"))
                    {
                        str2 = "Storm";
                    }
                    if (str.Contains("crash"))
                    {
                        str2 = "Crash";
                    }
                    if (str.Contains("overgrown"))
                    {
                        str2 = "Overgrown";
                    }
                    if (str.Contains("strike"))
                    {
                        str2 = "Strike";
                    }
                    if (str.Contains("vacant"))
                    {
                        str2 = "Vacant";
                    }
                    if (str.Contains("trailerpark"))
                    {
                        str2 = "Trailer Park";
                    }
                    if (str.Contains("fuel"))
                    {
                        str2 = "Fuel";
                    }
                    if (str.Contains("abandon"))
                    {
                        str2 = "Carnival";
                    }
                }
                return str2;
            }

            public static bool InGame()
            {
                return PS3.Extension.ReadBool(0x1d17a8c);
            }

            public static void iPrintln(int client, string message)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "c \"" + message);
            }

            public static void iPrintlnBold(int client, string message)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "f \"" + message);
            }

            public static string KeyBoard(int Client, string Title, string PresetText, int MaxLength)
            {
                CallFunction.Call(0x238070, new object[] { Client, Title, PresetText, MaxLength, 0x70b4d8 });
                while (PS3.Extension.ReadInt32(0x203b4c8) != 0)
                {
                }
                return PS3.Extension.ReadString(0x2510e22);
            }

            public static void Kick(int client, string message)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "w \"" + message);
            }

            public static void RemovelocWarnings(int ClientInt)
            {
                SV_SendServerCommand(ClientInt, "v loc_warnings \"0\"");
                SV_SendServerCommand(ClientInt, "v loc_warningsAsErrors \"0\"");
            }

            public static void Say(string text)
            {
                Cbuf_AddText(";say " + text);
            }

            public static void SetClientDvars(int client, string dvars)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "v " + dvars);
            }

            public static void SV_SendServerCommand(int clientIndex, string Command)
            {
                bool flag;
                WritePowerPc(true);
                PS3.Extension.WriteString(0x10040010, Command);
                PS3.Extension.WriteInt32(0x10040004, clientIndex);
                PS3.Extension.WriteBool(0x10040003, true);
                do
                {
                    flag = PS3.Extension.ReadBool(0x10040003);
                }
                while (flag);
                WritePowerPc(false);
            }

            public static void TeamChat(int client, string message)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "i \"" + message);
            }

            public static void Vision(int client, string message)
            {
                RemovelocWarnings(client);
                SV_SendServerCommand(client, "Q \"" + message);
            }

            public static void WritePowerPc(bool Active)
            {
                byte[] buffer = new byte[] { 
                    0xf8, 0x21, 0xff, 0x61, 0x7c, 8, 2, 0xa6, 0xf8, 1, 0, 0xb0, 60, 0x60, 0x10, 3, 
                    0x80, 0x63, 0, 0, 0x60, 0x62, 0, 0, 60, 0x60, 0x10, 4, 0x80, 0x63, 0, 0, 
                    0x2c, 3, 0, 0, 0x41, 130, 0, 40, 60, 0x60, 0x10, 4, 0x80, 0x63, 0, 4, 
                    60, 160, 0x10, 4, 0x38, 0x80, 0, 0, 0x30, 0xa5, 0, 0x10, 0x4b, 0xe8, 0xb2, 0x7d, 
                    0x38, 0x60, 0, 0, 60, 0x80, 0x10, 4, 0x90, 100, 0, 0, 60, 0x60, 0x10, 5, 
                    0x80, 0x63, 0, 0, 0x2c, 3, 0, 0, 0x41, 130, 0, 0x24, 60, 0x60, 0x10, 5, 
                    0x30, 0x63, 0, 0x10, 0x4b, 0xe2, 0xf9, 0x7d, 60, 0x80, 0x10, 5, 0x90, 100, 0, 4, 
                    0x38, 0x60, 0, 0, 60, 0x80, 0x10, 5, 0x90, 100, 0, 0, 60, 0x60, 0x10, 3, 
                    0x80, 0x63, 0, 4, 0x60, 0x62, 0, 0, 0xe8, 1, 0, 0xb0, 0x7c, 8, 3, 0xa6, 
                    0x38, 0x21, 0, 160, 0x4e, 0x80, 0, 0x20
                 };
                byte[] buffer2 = new byte[] { 
                    0x81, 0x62, 0x92, 0x84, 0x7c, 8, 2, 0xa6, 0xf8, 0x21, 0xff, 1, 0xfb, 0xe1, 0, 0xb8, 
                    0xdb, 1, 0, 0xc0, 0x7c, 0x7f, 0x1b, 120, 0xdb, 0x21, 0, 200, 0xdb, 0x41, 0, 0xd0, 
                    0xdb, 0x61, 0, 0xd8, 0xdb, 0x81, 0, 0xe0, 0xdb, 0xa1, 0, 0xe8, 0xdb, 0xc1, 0, 240, 
                    0xdb, 0xe1, 0, 0xf8, 0xfb, 0x61, 0, 0x98, 0xfb, 0x81, 0, 160, 0xfb, 0xa1, 0, 0xa8, 
                    0xfb, 0xc1, 0, 0xb0, 0xf8, 1, 1, 0x10, 0x81, 0x2b, 0, 0, 0x88, 9, 0, 12, 
                    0x2f, 0x80, 0, 0, 0x40, 0x9e, 0, 100, 0x7c, 0x69, 0x1b, 120, 0xc0, 2, 0x92, 0x94, 
                    0xc1, 0xa2, 0x92, 0x88, 0xd4, 9, 2, 0x40, 0xd0, 9, 0, 12, 0xd1, 0xa9, 0, 4, 
                    0xd0, 9, 0, 8, 0xe8, 1, 1, 0x10, 0xeb, 0x61, 0, 0x98, 0xeb, 0x81, 0, 160, 
                    0x7c, 8, 3, 0xa6, 0xeb, 0xa1, 0, 0xa8, 0xeb, 0xc1, 0, 0xb0, 0xeb, 0xe1, 0, 0xb8, 
                    0xcb, 1, 0, 0xc0, 0xcb, 0x21, 0, 200
                 };
                if (Active)
                {
                    PS3.SetMemory(0x38ede8, buffer);
                }
                else
                {
                    PS3.SetMemory(0x38ede8, buffer2);
                }
            }

            public static class Aimbot
            {
                public static PS3API PS3 = new PS3API(SelectAPI.TargetManager);

                public static bool ButtonPressed(int client, string Button)
                {
                    return (PS3.Extension.ReadString((uint) (0x34750e9f + (client * 0x97f80))) == Button);
                }

                public static void DoAimbot(int Client)
                {
                    if (ButtonPressed(Client, Buttons.L1) || ButtonPressed(Client, Buttons.L1 + Buttons.R1))
                    {
                        SetClientViewAngles(Client, ReturnOrigin(ReturnNearestPlayer(Client)));
                    }
                }

                public static uint G_Client(int Client, uint Mod = 0)
                {
                    return ((Offsets.G_Client + ((uint) (Offsets.G_ClientSize * Client))) + Mod);
                }

                public static uint G_Entity(int Client, uint Mod = 0)
                {
                    return ((Offsets.G_Entity + ((uint) (Offsets.G_EntitySize * Client))) + Mod);
                }

                public static int ReturnNearestPlayer(int Client)
                {
                    int num = -1;
                    float num2 = 4.294967E+09f;
                    float[] numArray = new float[3];
                    float num3 = 0f;
                    for (int i = 0; i < 0x12; i++)
                    {
                        numArray[0] = ReturnOrigin(i)[0] - ReturnOrigin(Client)[0];
                        numArray[1] = ReturnOrigin(i)[1] - ReturnOrigin(Client)[1];
                        numArray[2] = ReturnOrigin(i)[2] - ReturnOrigin(Client)[2];
                        num3 = (float) Math.Sqrt((double) (((numArray[0] * numArray[0]) + (numArray[1] * numArray[1])) + (numArray[2] * numArray[2])));
                        if (((i != Client) && (ReturnPlayerActivity(i) && ReturnPlayerLifeStatus(i))) && (num3 < num2))
                        {
                            num = i;
                            num2 = num3;
                        }
                    }
                    return num;
                }

                public static float[] ReturnOrigin(int Client)
                {
                    return new float[] { PS3.Extension.ReadFloat(G_Client(Client, 0x1c)), PS3.Extension.ReadFloat(G_Client(Client, 0x20)), PS3.Extension.ReadFloat(G_Client(Client, 0x24)) };
                }

                public static bool ReturnPlayerActivity(int Client)
                {
                    return (PS3.Extension.ReadString(G_Client(Client, 0x3290)) != "");
                }

                public static bool ReturnPlayerLifeStatus(int Client)
                {
                    return (PS3.Extension.ReadByte(G_Client(Client, 0x345c)) != 1);
                }

                public static void SetClientViewAngles(int Client, float[] Angles)
                {
                    PS3.Extension.WriteFloat(0x10004000, Angles[0]);
                    PS3.Extension.WriteFloat(0x10004004, Angles[1]);
                    PS3.Extension.WriteFloat(0x10004008, Angles[2]);
                    Mw2Library.RPC.CallFunction.Call(Offsets.VectoAngles, new object[] { 0x10004000, 0x1000400c });
                    Mw2Library.RPC.CallFunction.Call(Offsets.SetClientViewAngles, new object[] { G_Entity(Client, 0), 0x1000400c });
                }

                public class Buttons
                {
                    public static string Circle = "+stance";
                    public static string Cross = "+gostand";
                    public static string DpadDown = "+actionslot 2";
                    public static string DpadLeft = "+actionslot 3";
                    public static string DpadRight = "+actionslot 4";
                    public static string DpadUp = "+actionslot 1";
                    public static string L1 = "+speed_throw";
                    public static string L2 = "+smoke";
                    public static string L3 = "+breath_sprint";
                    public static string R1 = "+attack";
                    public static string R2 = "+frag";
                    public static string R3 = "+melee";
                    public static string Select = "togglescores";
                    public static string Square = "+usereload";
                    public static string Start = "togglemenu";
                    public static string Triangle = "weapnext";
                }

                public class Offsets
                {
                    public static uint G_Client = 0x14e2200;
                    public static uint G_ClientSize = 0x3700;
                    public static uint G_Entity = 0x1319800;
                    public static uint G_EntitySize = 640;
                    public static uint SetClientViewAngles = 0x16cbe0;
                    public static uint VectoAngles = 0x2590a8;
                }
            }

            public static class CallFunction
            {
                private static uint function_address;

                public static int Call(uint func_address, params object[] parameters)
                {
                    int length = parameters.Length;
                    int index = 0;
                    uint num3 = 0;
                    uint num4 = 0;
                    uint num5 = 0;
                    uint num6 = 0;
                    while (index < length)
                    {
                        if (parameters[index] is int)
                        {
                            Mw2Library.RPC.PS3.Extension.WriteInt32(0x10020000 + (num3 * 4), (int) parameters[index]);
                            num3++;
                        }
                        else if (parameters[index] is uint)
                        {
                            Mw2Library.RPC.PS3.Extension.WriteUInt32(0x10020000 + (num3 * 4), (uint) parameters[index]);
                            num3++;
                        }
                        else
                        {
                            uint num7;
                            if (parameters[index] is string)
                            {
                                num7 = 0x10022000 + (num4 * 0x400);
                                Mw2Library.RPC.PS3.Extension.WriteString(num7, Convert.ToString(parameters[index]));
                                Mw2Library.RPC.PS3.Extension.WriteUInt32(0x10020000 + (num3 * 4), num7);
                                num3++;
                                num4++;
                            }
                            else if (parameters[index] is float)
                            {
                                Mw2Library.RPC.PS3.Extension.WriteFloat(0x10020024 + (num5 * 4), (float) parameters[index]);
                                num5++;
                            }
                            else if (parameters[index] is float[])
                            {
                                float[] input = (float[]) parameters[index];
                                num7 = 0x10021000 + (num6 * 4);
                                WriteSingle(num7, input);
                                Mw2Library.RPC.PS3.Extension.WriteUInt32(0x10020000 + (num3 * 4), num7);
                                num3++;
                                num6 += (uint) input.Length;
                            }
                        }
                        index++;
                    }
                    Mw2Library.RPC.PS3.Extension.WriteUInt32(0x1002004c, func_address);
                    Thread.Sleep(20);
                    return Mw2Library.RPC.PS3.Extension.ReadInt32(0x10020050);
                }
                public static void WriteSingle(uint address, float input)
                {
                    byte[] array = new byte[4];
                    BitConverter.GetBytes(input).CopyTo(array, 0);
                    Array.Reverse(array, 0, 4);
                    PS3.SetMemory(address, array);
                }

                public static void WriteSingle(uint address, float[] input)
                {
                    int length = input.Length;
                    byte[] array = new byte[length * 4];
                    for (int i = 0; i < length; i++)
                    {
                        ReverseBytes(BitConverter.GetBytes(input[i])).CopyTo(array, (int)(i * 4));
                    }
                    PS3.SetMemory(address, array);
                }
                public static byte[] ReverseBytes(byte[] input)
                {
                    Array.Reverse(input);
                    return input;
                }
                public static void Enable()
                {
                    Mw2Library.RPC.PS3.SetMemory(function_address, new byte[] { 0x4e, 0x80, 0, 0x20 });
                    Thread.Sleep(20);
                    byte[] buffer = new byte[] { 
                        0x7c, 8, 2, 0xa6, 0xf8, 1, 0, 0x80, 60, 0x60, 0x10, 2, 0x81, 0x83, 0, 0x4c, 
                        0x2c, 12, 0, 0, 0x41, 130, 0, 100, 0x80, 0x83, 0, 4, 0x80, 0xa3, 0, 8, 
                        0x80, 0xc3, 0, 12, 0x80, 0xe3, 0, 0x10, 0x81, 3, 0, 20, 0x81, 0x23, 0, 0x18, 
                        0x81, 0x43, 0, 0x1c, 0x81, 0x63, 0, 0x20, 0xc0, 0x23, 0, 0x24, 0xc0, 0x43, 0, 40, 
                        0xc0, 0x63, 0, 0x2c, 0xc0, 0x83, 0, 0x30, 0xc0, 0xa3, 0, 0x34, 0xc0, 0xc3, 0, 0x38, 
                        0xc0, 0xe3, 0, 60, 0xc1, 3, 0, 0x40, 0xc1, 0x23, 0, 0x48, 0x80, 0x63, 0, 0, 
                        0x7d, 0x89, 3, 0xa6, 0x4e, 0x80, 4, 0x21, 60, 0x80, 0x10, 2, 0x38, 160, 0, 0, 
                        0x90, 0xa4, 0, 0x4c, 0x90, 100, 0, 80, 0xe8, 1, 0, 0x80, 0x7c, 8, 3, 0xa6, 
                        0x38, 0x21, 0, 0x70, 0x4e, 0x80, 0, 0x20
                     };
                    Mw2Library.RPC.PS3.SetMemory(function_address + 4, buffer);
                    Mw2Library.RPC.PS3.SetMemory(0x10020000, new byte[0x2854]);
                    Mw2Library.RPC.PS3.SetMemory(function_address, new byte[] { 0xf8, 0x21, 0xff, 0x91 });
                }

                public static int Init()
                {
                    function_address = 0x10a30;
                    Enable();
                    return 0;
                }

                public static class VezahRPC
                {
                    public static uint function_address;
                    public static PS3API PS3 = new PS3API(SelectAPI.TargetManager);

                    public static int CallFunc(uint uint_1, params object[] object_0)
                    {
                        int length = object_0.Length;
                        uint num2 = 0;
                        for (uint i = 0; i < length; i++)
                        {
                            byte[] buffer;
                            if (object_0[i] is int)
                            {
                                buffer = BitConverter.GetBytes((int) object_0[i]);
                                Array.Reverse(buffer);
                                PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer);
                            }
                            else if (object_0[i] is uint)
                            {
                                buffer = BitConverter.GetBytes((uint) object_0[i]);
                                Array.Reverse(buffer);
                                PS3.SetMemory(0x10050000 + ((i + num2) * 4), buffer);
                            }
                            else if (object_0[i] is string)
                            {
                                byte[] buffer2 = Encoding.UTF8.GetBytes(Convert.ToString(object_0[i]) + "\0");
                                PS3.SetMemory(0x10050054 + (i * 0x400), buffer2);
                                uint num4 = 0x10050054 + (i * 0x400);
                                byte[] array = BitConverter.GetBytes(num4);
                                Array.Reverse(array);
                                PS3.SetMemory(0x10050000 + ((i + num2) * 4), array);
                            }
                            else if (object_0[i] is float)
                            {
                                num2++;
                                buffer = BitConverter.GetBytes((float) object_0[i]);
                                Array.Reverse(buffer);
                                PS3.SetMemory(0x10050024 + ((num2 - 1) * 4), buffer);
                            }
                        }
                        byte[] bytes = BitConverter.GetBytes(uint_1);
                        Array.Reverse(bytes);
                        PS3.SetMemory(0x1005004c, bytes);
                        Thread.Sleep(20);
                        byte[] buffer5 = new byte[4];
                        PS3.GetMemory(0x10050050, buffer5);
                        Array.Reverse(buffer5);
                        return BitConverter.ToInt32(buffer5, 0);
                    }

                    public static void Enable_RPC()
                    {
                        PS3.SetMemory(function_address, new byte[] { 0x4e, 0x80, 0, 0x20 });
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
                        PS3.SetMemory(function_address + 4, buffer);
                        PS3.SetMemory(0x10050000, new byte[0x2854]);
                        PS3.SetMemory(function_address, new byte[] { 0xf8, 0x21, 0xff, 0x91 });
                        PS3.SetMemory(0x2100000, new byte[0x20]);
                        PS3.SetMemory(0x2105000, new byte[0x20]);
                    }

                    public static uint Get_func_address()
                    {
                        for (uint i = 0x10a24; i < 0x1000000; i += 4)
                        {
                            byte[] buffer = PS3.Extension.ReadBytes(i, 8);
                            if (((((buffer[0] == 0xec) && (buffer[1] == 0x21)) && ((buffer[2] == 0) && (buffer[3] == 50))) && (((buffer[4] == 0x4e) && (buffer[5] == 0x80)) && (buffer[6] == 0))) && (buffer[7] == 0x20))
                            {
                                return (i + 12);
                            }
                        }
                        return 0;
                    }

                    public static int Init()
                    {
                        function_address = Get_func_address();
                        if (function_address == 0)
                        {
                            return -1;
                        }
                        Enable_RPC();
                        return 0;
                    }
                }
            }

            public static class GiveWeapon
            {
                public static void G_GivePlayerWeapon(int clientIndex, WeaponIndexes wpn, Weapon_Camos Camo = 0, bool Akimbo = false)
                {
                    int num = Mw2Library.RPC.CallFunction.Call(0x32f90, new object[] { Enum.GetName(typeof(WeaponIndexes), wpn) });
                    Mw2Library.RPC.CallFunction.Call(0x1c0890, new object[] { Mw2Library.RPC.G_Client(clientIndex), num, (int) Camo, Convert.ToInt32(Akimbo) });
                    Mw2Library.RPC.CallFunction.Call(0x174bf8, new object[] { Mw2Library.RPC.G_Entity(clientIndex), num, 0, 0x270f, 1 });
                    Mw2Library.RPC.SV_SendServerCommand(clientIndex, "a " + num);
                    Mw2Library.RPC.iPrintln(clientIndex, "Given Weapon: ^1" + Convert.ToString(wpn));
                }

                public enum Weapon_Camos
                {
                    None,
                    Woodland,
                    Digital,
                    Desert,
                    Arctic,
                    Urban,
                    Red_Tiger,
                    Blue_Tiger,
                    Fall
                }

                public enum WeaponIndexes
                {
                    aa12_eotech_fmj_mp = 0x377,
                    aa12_eotech_grip_mp = 0x378,
                    aa12_eotech_mp = 0x371,
                    aa12_eotech_silencer_mp = 0x379,
                    aa12_eotech_xmags_mp = 890,
                    aa12_fmj_grip_mp = 0x37b,
                    aa12_fmj_mp = 0x372,
                    aa12_fmj_reflex_mp = 0x37c,
                    aa12_fmj_silencer_mp = 0x37d,
                    aa12_fmj_xmags_mp = 0x37e,
                    aa12_grip_mp = 0x373,
                    aa12_grip_reflex_mp = 0x37f,
                    aa12_grip_silencer_mp = 0x380,
                    aa12_grip_xmags_mp = 0x381,
                    aa12_mp = 880,
                    aa12_reflex_mp = 0x374,
                    aa12_reflex_silencer_mp = 0x382,
                    aa12_reflex_xmags_mp = 0x383,
                    aa12_silencer_mp = 0x375,
                    aa12_silencer_xmags_mp = 900,
                    aa12_xmags_mp = 0x376,
                    ac130_105mm_mp = 0x498,
                    ac130_25mm_mp = 0x496,
                    ac130_40mm_mp = 0x497,
                    airdrop_marker_mp = 0x479,
                    airdrop_mega_marker_mp = 0x493,
                    airdrop_sentry_marker_mp = 0x490,
                    ak47_acog_fmj_mp = 0x13b,
                    ak47_acog_gl_mp = 0x13c,
                    ak47_acog_heartbeat_mp = 0x13d,
                    ak47_acog_mp = 0x12f,
                    ak47_acog_shotgun_mp = 0x13e,
                    ak47_acog_silencer_mp = 0x13f,
                    ak47_acog_xmags_mp = 320,
                    ak47_eotech_fmj_mp = 0x141,
                    ak47_eotech_gl_mp = 0x142,
                    ak47_eotech_heartbeat_mp = 0x143,
                    ak47_eotech_mp = 0x130,
                    ak47_eotech_shotgun_mp = 0x144,
                    ak47_eotech_silencer_mp = 0x145,
                    ak47_eotech_xmags_mp = 0x146,
                    ak47_fmj_gl_mp = 0x147,
                    ak47_fmj_heartbeat_mp = 0x148,
                    ak47_fmj_mp = 0x131,
                    ak47_fmj_reflex_mp = 0x149,
                    ak47_fmj_shotgun_mp = 330,
                    ak47_fmj_silencer_mp = 0x14b,
                    ak47_fmj_thermal_mp = 0x14c,
                    ak47_fmj_xmags_mp = 0x14d,
                    ak47_gl_heartbeat_mp = 0x14e,
                    ak47_gl_mp = 0x132,
                    ak47_gl_reflex_mp = 0x14f,
                    ak47_gl_silencer_mp = 0x150,
                    ak47_gl_thermal_mp = 0x151,
                    ak47_gl_xmags_mp = 0x152,
                    ak47_heartbeat_mp = 0x134,
                    ak47_heartbeat_reflex_mp = 0x153,
                    ak47_heartbeat_shotgun_mp = 340,
                    ak47_heartbeat_silencer_mp = 0x155,
                    ak47_heartbeat_thermal_mp = 0x156,
                    ak47_heartbeat_xmags_mp = 0x157,
                    ak47_mp = 0x12e,
                    ak47_reflex_mp = 0x135,
                    ak47_reflex_shotgun_mp = 0x158,
                    ak47_reflex_silencer_mp = 0x159,
                    ak47_reflex_xmags_mp = 0x15a,
                    ak47_shotgun_attach_mp = 0x137,
                    ak47_shotgun_mp = 310,
                    ak47_shotgun_silencer_mp = 0x15b,
                    ak47_shotgun_thermal_mp = 0x15c,
                    ak47_shotgun_xmags_mp = 0x15d,
                    ak47_silencer_mp = 0x138,
                    ak47_silencer_thermal_mp = 350,
                    ak47_silencer_xmags_mp = 0x15f,
                    ak47_thermal_mp = 0x139,
                    ak47_thermal_xmags_mp = 0x160,
                    ak47_xmags_mp = 0x13a,
                    artillery_mp = 0x49b,
                    at4_mp = 0x2fc,
                    aug_acog_fmj_mp = 0x459,
                    aug_acog_grip_mp = 0x45a,
                    aug_acog_heartbeat_mp = 0x45b,
                    aug_acog_mp = 0x450,
                    aug_acog_silencer_mp = 0x45c,
                    aug_acog_xmags_mp = 0x45d,
                    aug_eotech_fmj_mp = 0x45e,
                    aug_eotech_grip_mp = 0x45f,
                    aug_eotech_heartbeat_mp = 0x460,
                    aug_eotech_mp = 0x451,
                    aug_eotech_silencer_mp = 0x461,
                    aug_eotech_xmags_mp = 0x462,
                    aug_fmj_grip_mp = 0x463,
                    aug_fmj_heartbeat_mp = 0x464,
                    aug_fmj_mp = 0x452,
                    aug_fmj_reflex_mp = 0x465,
                    aug_fmj_silencer_mp = 0x466,
                    aug_fmj_thermal_mp = 0x467,
                    aug_fmj_xmags_mp = 0x468,
                    aug_grip_heartbeat_mp = 0x469,
                    aug_grip_mp = 0x453,
                    aug_grip_reflex_mp = 0x46a,
                    aug_grip_silencer_mp = 0x46b,
                    aug_grip_thermal_mp = 0x46c,
                    aug_grip_xmags_mp = 0x46d,
                    aug_heartbeat_mp = 0x454,
                    aug_heartbeat_reflex_mp = 0x46e,
                    aug_heartbeat_silencer_mp = 0x46f,
                    aug_heartbeat_thermal_mp = 0x470,
                    aug_heartbeat_xmags_mp = 0x471,
                    aug_mp = 0x44f,
                    aug_reflex_mp = 0x455,
                    aug_reflex_silencer_mp = 0x472,
                    aug_reflex_xmags_mp = 0x473,
                    aug_silencer_mp = 0x456,
                    aug_silencer_thermal_mp = 0x474,
                    aug_silencer_xmags_mp = 0x475,
                    aug_thermal_mp = 0x457,
                    aug_thermal_xmags_mp = 0x476,
                    aug_xmags_mp = 0x458,
                    barrel_mp = 0x4a7,
                    barrett_acog_fmj_mp = 0x306,
                    barrett_acog_heartbeat_mp = 0x307,
                    barrett_acog_mp = 0x300,
                    barrett_acog_silencer_mp = 0x308,
                    barrett_acog_xmags_mp = 0x309,
                    barrett_fmj_heartbeat_mp = 0x30a,
                    barrett_fmj_mp = 0x301,
                    barrett_fmj_silencer_mp = 0x30b,
                    barrett_fmj_thermal_mp = 780,
                    barrett_fmj_xmags_mp = 0x30d,
                    barrett_heartbeat_mp = 770,
                    barrett_heartbeat_silencer_mp = 0x30e,
                    barrett_heartbeat_thermal_mp = 0x30f,
                    barrett_heartbeat_xmags_mp = 0x310,
                    barrett_mp = 0x2ff,
                    barrett_silencer_mp = 0x303,
                    barrett_silencer_thermal_mp = 0x311,
                    barrett_silencer_xmags_mp = 0x312,
                    barrett_thermal_mp = 0x304,
                    barrett_thermal_xmags_mp = 0x313,
                    barrett_xmags_mp = 0x305,
                    beretta_akimbo_fmj_mp = 9,
                    beretta_akimbo_mp = 4,
                    beretta_akimbo_silencer_mp = 10,
                    beretta_akimbo_xmags_mp = 11,
                    beretta_fmj_mp = 5,
                    beretta_fmj_silencer_mp = 12,
                    beretta_fmj_tactical_mp = 13,
                    beretta_fmj_xmags_mp = 14,
                    beretta_mp = 3,
                    beretta_silencer_mp = 6,
                    beretta_silencer_tactical_mp = 15,
                    beretta_silencer_xmags_mp = 0x10,
                    beretta_tactical_mp = 7,
                    beretta_tactical_xmags_mp = 0x11,
                    beretta_xmags_mp = 8,
                    beretta393_akimbo_fmj_mp = 0x48,
                    beretta393_akimbo_mp = 0x42,
                    beretta393_akimbo_silencer_mp = 0x49,
                    beretta393_akimbo_xmags_mp = 0x4a,
                    beretta393_eotech_fmj_mp = 0x4b,
                    beretta393_eotech_mp = 0x43,
                    beretta393_eotech_silencer_mp = 0x4c,
                    beretta393_eotech_xmags_mp = 0x4d,
                    beretta393_fmj_mp = 0x44,
                    beretta393_fmj_reflex_mp = 0x4e,
                    beretta393_fmj_silencer_mp = 0x4f,
                    beretta393_fmj_xmags_mp = 80,
                    beretta393_mp = 0x41,
                    beretta393_reflex_mp = 0x45,
                    beretta393_reflex_silencer_mp = 0x51,
                    beretta393_reflex_xmags_mp = 0x52,
                    beretta393_silencer_mp = 70,
                    beretta393_silencer_xmags_mp = 0x53,
                    beretta393_xmags_mp = 0x47,
                    briefcase_bomb_defuse_mp = 0x486,
                    briefcase_bomb_mp = 0x485,
                    c4_mp = 0x477,
                    cheytac_acog_fmj_mp = 0x345,
                    cheytac_acog_heartbeat_mp = 0x346,
                    cheytac_acog_mp = 0x33f,
                    cheytac_acog_silencer_mp = 0x347,
                    cheytac_acog_xmags_mp = 840,
                    cheytac_fmj_heartbeat_mp = 0x349,
                    cheytac_fmj_mp = 0x340,
                    cheytac_fmj_silencer_mp = 0x34a,
                    cheytac_fmj_thermal_mp = 0x34b,
                    cheytac_fmj_xmags_mp = 0x34c,
                    cheytac_heartbeat_mp = 0x341,
                    cheytac_heartbeat_silencer_mp = 0x34d,
                    cheytac_heartbeat_thermal_mp = 0x34e,
                    cheytac_heartbeat_xmags_mp = 0x34f,
                    cheytac_mp = 830,
                    cheytac_silencer_mp = 0x342,
                    cheytac_silencer_thermal_mp = 0x350,
                    cheytac_silencer_xmags_mp = 0x351,
                    cheytac_thermal_mp = 0x343,
                    cheytac_thermal_xmags_mp = 850,
                    cheytac_xmags_mp = 0x344,
                    claymore_mp = 0x478,
                    coltanaconda_akimbo_fmj_mp = 0x2b,
                    coltanaconda_akimbo_mp = 40,
                    coltanaconda_fmj_mp = 0x29,
                    coltanaconda_fmj_tactical_mp = 0x2c,
                    coltanaconda_mp = 0x27,
                    coltanaconda_tactical_mp = 0x2a,
                    concussion_grenade_mp = 0x47e,
                    defaultweapon_mp = 1,
                    deserteagle_akimbo_fmj_mp = 0x25,
                    deserteagle_akimbo_mp = 0x22,
                    deserteagle_fmj_mp = 0x23,
                    deserteagle_fmj_tactical_mp = 0x26,
                    deserteagle_mp = 0x21,
                    deserteagle_tactical_mp = 0x24,
                    deserteaglegold_mp = 0x2d,
                    fal_acog_fmj_mp = 0x26d,
                    fal_acog_gl_mp = 0x26e,
                    fal_acog_heartbeat_mp = 0x26f,
                    fal_acog_mp = 0x261,
                    fal_acog_shotgun_mp = 0x270,
                    fal_acog_silencer_mp = 0x271,
                    fal_acog_xmags_mp = 0x272,
                    fal_eotech_fmj_mp = 0x273,
                    fal_eotech_gl_mp = 0x274,
                    fal_eotech_heartbeat_mp = 0x275,
                    fal_eotech_mp = 610,
                    fal_eotech_shotgun_mp = 630,
                    fal_eotech_silencer_mp = 0x277,
                    fal_eotech_xmags_mp = 0x278,
                    fal_fmj_gl_mp = 0x279,
                    fal_fmj_heartbeat_mp = 0x27a,
                    fal_fmj_mp = 0x263,
                    fal_fmj_reflex_mp = 0x27b,
                    fal_fmj_shotgun_mp = 0x27c,
                    fal_fmj_silencer_mp = 0x27d,
                    fal_fmj_thermal_mp = 0x27e,
                    fal_fmj_xmags_mp = 0x27f,
                    fal_gl_heartbeat_mp = 640,
                    fal_gl_mp = 0x264,
                    fal_gl_reflex_mp = 0x281,
                    fal_gl_silencer_mp = 0x282,
                    fal_gl_thermal_mp = 0x283,
                    fal_gl_xmags_mp = 0x284,
                    fal_heartbeat_mp = 0x266,
                    fal_heartbeat_reflex_mp = 0x285,
                    fal_heartbeat_shotgun_mp = 0x286,
                    fal_heartbeat_silencer_mp = 0x287,
                    fal_heartbeat_thermal_mp = 0x288,
                    fal_heartbeat_xmags_mp = 0x289,
                    fal_mp = 0x260,
                    fal_reflex_mp = 0x267,
                    fal_reflex_shotgun_mp = 650,
                    fal_reflex_silencer_mp = 0x28b,
                    fal_reflex_xmags_mp = 0x28c,
                    fal_shotgun_attach_mp = 0x269,
                    fal_shotgun_mp = 0x268,
                    fal_shotgun_silencer_mp = 0x28d,
                    fal_shotgun_thermal_mp = 0x28e,
                    fal_shotgun_xmags_mp = 0x28f,
                    fal_silencer_mp = 0x26a,
                    fal_silencer_thermal_mp = 0x290,
                    fal_silencer_xmags_mp = 0x291,
                    fal_thermal_mp = 0x26b,
                    fal_thermal_xmags_mp = 0x292,
                    fal_xmags_mp = 620,
                    famas_acog_fmj_mp = 570,
                    famas_acog_gl_mp = 0x23b,
                    famas_acog_heartbeat_mp = 0x23c,
                    famas_acog_mp = 0x22e,
                    famas_acog_shotgun_mp = 0x23d,
                    famas_acog_silencer_mp = 0x23e,
                    famas_acog_xmags_mp = 0x23f,
                    famas_eotech_fmj_mp = 0x240,
                    famas_eotech_gl_mp = 0x241,
                    famas_eotech_heartbeat_mp = 0x242,
                    famas_eotech_mp = 0x22f,
                    famas_eotech_shotgun_mp = 0x243,
                    famas_eotech_silencer_mp = 580,
                    famas_eotech_xmags_mp = 0x245,
                    famas_fmj_gl_mp = 0x246,
                    famas_fmj_heartbeat_mp = 0x247,
                    famas_fmj_mp = 560,
                    famas_fmj_reflex_mp = 0x248,
                    famas_fmj_shotgun_mp = 0x249,
                    famas_fmj_silencer_mp = 0x24a,
                    famas_fmj_thermal_mp = 0x24b,
                    famas_fmj_xmags_mp = 0x24c,
                    famas_gl_heartbeat_mp = 0x24d,
                    famas_gl_mp = 0x231,
                    famas_gl_reflex_mp = 590,
                    famas_gl_silencer_mp = 0x24f,
                    famas_gl_thermal_mp = 0x250,
                    famas_gl_xmags_mp = 0x251,
                    famas_heartbeat_mp = 0x233,
                    famas_heartbeat_reflex_mp = 0x252,
                    famas_heartbeat_shotgun_mp = 0x253,
                    famas_heartbeat_silencer_mp = 0x254,
                    famas_heartbeat_thermal_mp = 0x255,
                    famas_heartbeat_xmags_mp = 0x256,
                    famas_mp = 0x22d,
                    famas_reflex_mp = 0x234,
                    famas_reflex_shotgun_mp = 0x257,
                    famas_reflex_silencer_mp = 600,
                    famas_reflex_xmags_mp = 0x259,
                    famas_shotgun_attach_mp = 0x236,
                    famas_shotgun_mp = 0x235,
                    famas_shotgun_silencer_mp = 0x25a,
                    famas_shotgun_thermal_mp = 0x25b,
                    famas_shotgun_xmags_mp = 0x25c,
                    famas_silencer_mp = 0x237,
                    famas_silencer_thermal_mp = 0x25d,
                    famas_silencer_xmags_mp = 0x25e,
                    famas_thermal_mp = 0x238,
                    famas_thermal_xmags_mp = 0x25f,
                    famas_xmags_mp = 0x239,
                    flare_mp = 0x481,
                    flash_grenade_mp = 0x47c,
                    fn2000_acog_fmj_mp = 0x1d4,
                    fn2000_acog_gl_mp = 0x1d5,
                    fn2000_acog_heartbeat_mp = 470,
                    fn2000_acog_mp = 0x1c8,
                    fn2000_acog_shotgun_mp = 0x1d7,
                    fn2000_acog_silencer_mp = 0x1d8,
                    fn2000_acog_xmags_mp = 0x1d9,
                    fn2000_eotech_fmj_mp = 0x1da,
                    fn2000_eotech_gl_mp = 0x1db,
                    fn2000_eotech_heartbeat_mp = 0x1dc,
                    fn2000_eotech_mp = 0x1c9,
                    fn2000_eotech_shotgun_mp = 0x1dd,
                    fn2000_eotech_silencer_mp = 0x1de,
                    fn2000_eotech_xmags_mp = 0x1df,
                    fn2000_fmj_gl_mp = 480,
                    fn2000_fmj_heartbeat_mp = 0x1e1,
                    fn2000_fmj_mp = 0x1ca,
                    fn2000_fmj_reflex_mp = 0x1e2,
                    fn2000_fmj_shotgun_mp = 0x1e3,
                    fn2000_fmj_silencer_mp = 0x1e4,
                    fn2000_fmj_thermal_mp = 0x1e5,
                    fn2000_fmj_xmags_mp = 0x1e6,
                    fn2000_gl_heartbeat_mp = 0x1e7,
                    fn2000_gl_mp = 0x1cb,
                    fn2000_gl_reflex_mp = 0x1e8,
                    fn2000_gl_silencer_mp = 0x1e9,
                    fn2000_gl_thermal_mp = 490,
                    fn2000_gl_xmags_mp = 0x1eb,
                    fn2000_heartbeat_mp = 0x1cd,
                    fn2000_heartbeat_reflex_mp = 0x1ec,
                    fn2000_heartbeat_shotgun_mp = 0x1ed,
                    fn2000_heartbeat_silencer_mp = 0x1ee,
                    fn2000_heartbeat_thermal_mp = 0x1ef,
                    fn2000_heartbeat_xmags_mp = 0x1f0,
                    fn2000_mp = 0x1c7,
                    fn2000_reflex_mp = 0x1ce,
                    fn2000_reflex_shotgun_mp = 0x1f1,
                    fn2000_reflex_silencer_mp = 0x1f2,
                    fn2000_reflex_xmags_mp = 0x1f3,
                    fn2000_shotgun_attach_mp = 0x1d0,
                    fn2000_shotgun_mp = 0x1cf,
                    fn2000_shotgun_silencer_mp = 500,
                    fn2000_shotgun_thermal_mp = 0x1f5,
                    fn2000_shotgun_xmags_mp = 0x1f6,
                    fn2000_silencer_mp = 0x1d1,
                    fn2000_silencer_thermal_mp = 0x1f7,
                    fn2000_silencer_xmags_mp = 0x1f8,
                    fn2000_thermal_mp = 0x1d2,
                    fn2000_thermal_xmags_mp = 0x1f9,
                    fn2000_xmags_mp = 0x1d3,
                    frag_grenade_mp = 0x47b,
                    frag_grenade_short_mp = 0x483,
                    gl_ak47_mp = 0x133,
                    gl_fal_mp = 0x265,
                    gl_famas_mp = 0x232,
                    gl_fn2000_mp = 460,
                    gl_m16_mp = 0x166,
                    gl_m4_mp = 0x199,
                    gl_masada_mp = 0x1ff,
                    gl_mp = 0x2f9,
                    gl_scar_mp = 0x298,
                    gl_tavor_mp = 0x2cb,
                    glock_akimbo_fmj_mp = 0x35,
                    glock_akimbo_mp = 0x2f,
                    glock_akimbo_silencer_mp = 0x36,
                    glock_akimbo_xmags_mp = 0x37,
                    glock_eotech_fmj_mp = 0x38,
                    glock_eotech_mp = 0x30,
                    glock_eotech_silencer_mp = 0x39,
                    glock_eotech_xmags_mp = 0x3a,
                    glock_fmj_mp = 0x31,
                    glock_fmj_reflex_mp = 0x3b,
                    glock_fmj_silencer_mp = 60,
                    glock_fmj_xmags_mp = 0x3d,
                    glock_mp = 0x2e,
                    glock_reflex_mp = 50,
                    glock_reflex_silencer_mp = 0x3e,
                    glock_reflex_xmags_mp = 0x3f,
                    glock_silencer_mp = 0x33,
                    glock_silencer_xmags_mp = 0x40,
                    glock_xmags_mp = 0x34,
                    harrier_20mm_mp = 0x49d,
                    harrier_ffar_mp = 0x49e,
                    harrier_missile_mp = 0x49c,
                    heli_remote_mp = 0x4a3,
                    javelin_mp = 0x2fe,
                    killstreak_ac130_mp = 0x489,
                    killstreak_counter_uav_mp = 0x48e,
                    killstreak_emp_mp = 0x492,
                    killstreak_harrier_airstrike_mp = 0x495,
                    killstreak_helicopter_flares_mp = 0x491,
                    killstreak_helicopter_minigun_mp = 0x48b,
                    killstreak_helicopter_mp = 0x488,
                    killstreak_nuke_mp = 0x48c,
                    killstreak_precision_airstrike_mp = 0x48d,
                    killstreak_predator_missile_mp = 0x48a,
                    killstreak_sentry_mp = 0x48f,
                    killstreak_stealth_airstrike_mp = 0x494,
                    killstreak_uav_mp = 0x487,
                    kriss_acog_fmj_mp = 240,
                    kriss_acog_mp = 0xe7,
                    kriss_acog_rof_mp = 0xf1,
                    kriss_acog_silencer_mp = 0xf2,
                    kriss_acog_xmags_mp = 0xf3,
                    kriss_akimbo_fmj_mp = 0xf4,
                    kriss_akimbo_mp = 0xe8,
                    kriss_akimbo_rof_mp = 0xf5,
                    kriss_akimbo_silencer_mp = 0xf6,
                    kriss_akimbo_xmags_mp = 0xf7,
                    kriss_eotech_fmj_mp = 0xf8,
                    kriss_eotech_mp = 0xe9,
                    kriss_eotech_rof_mp = 0xf9,
                    kriss_eotech_silencer_mp = 250,
                    kriss_eotech_xmags_mp = 0xfb,
                    kriss_fmj_mp = 0xea,
                    kriss_fmj_reflex_mp = 0xfc,
                    kriss_fmj_rof_mp = 0xfd,
                    kriss_fmj_silencer_mp = 0xfe,
                    kriss_fmj_thermal_mp = 0xff,
                    kriss_fmj_xmags_mp = 0x100,
                    kriss_mp = 230,
                    kriss_reflex_mp = 0xeb,
                    kriss_reflex_rof_mp = 0x101,
                    kriss_reflex_silencer_mp = 0x102,
                    kriss_reflex_xmags_mp = 0x103,
                    kriss_rof_mp = 0xec,
                    kriss_rof_silencer_mp = 260,
                    kriss_rof_thermal_mp = 0x105,
                    kriss_rof_xmags_mp = 0x106,
                    kriss_silencer_mp = 0xed,
                    kriss_silencer_thermal_mp = 0x107,
                    kriss_silencer_xmags_mp = 0x108,
                    kriss_thermal_mp = 0xee,
                    kriss_thermal_xmags_mp = 0x109,
                    kriss_xmags_mp = 0xef,
                    lightstick_mp = 0x4a8,
                    m1014_eotech_fmj_mp = 0x38c,
                    m1014_eotech_grip_mp = 0x38d,
                    m1014_eotech_mp = 0x386,
                    m1014_eotech_silencer_mp = 910,
                    m1014_eotech_xmags_mp = 0x38f,
                    m1014_fmj_grip_mp = 0x390,
                    m1014_fmj_mp = 0x387,
                    m1014_fmj_reflex_mp = 0x391,
                    m1014_fmj_silencer_mp = 0x392,
                    m1014_fmj_xmags_mp = 0x393,
                    m1014_grip_mp = 0x388,
                    m1014_grip_reflex_mp = 0x394,
                    m1014_grip_silencer_mp = 0x395,
                    m1014_grip_xmags_mp = 0x396,
                    m1014_mp = 0x385,
                    m1014_reflex_mp = 0x389,
                    m1014_reflex_silencer_mp = 0x397,
                    m1014_reflex_xmags_mp = 920,
                    m1014_silencer_mp = 0x38a,
                    m1014_silencer_xmags_mp = 0x399,
                    m1014_xmags_mp = 0x38b,
                    m16_acog_fmj_mp = 0x16e,
                    m16_acog_gl_mp = 0x16f,
                    m16_acog_heartbeat_mp = 0x170,
                    m16_acog_mp = 0x162,
                    m16_acog_shotgun_mp = 0x171,
                    m16_acog_silencer_mp = 370,
                    m16_acog_xmags_mp = 0x173,
                    m16_eotech_fmj_mp = 0x174,
                    m16_eotech_gl_mp = 0x175,
                    m16_eotech_heartbeat_mp = 0x176,
                    m16_eotech_mp = 0x163,
                    m16_eotech_shotgun_mp = 0x177,
                    m16_eotech_silencer_mp = 0x178,
                    m16_eotech_xmags_mp = 0x179,
                    m16_fmj_gl_mp = 0x17a,
                    m16_fmj_heartbeat_mp = 0x17b,
                    m16_fmj_mp = 0x164,
                    m16_fmj_reflex_mp = 380,
                    m16_fmj_shotgun_mp = 0x17d,
                    m16_fmj_silencer_mp = 0x17e,
                    m16_gl_mp = 0x165,
                    m16_heartbeat_mp = 0x167,
                    m16_mp = 0x161,
                    m16_reflex_mp = 360,
                    m16_shotgun_attach_mp = 0x16a,
                    m16_shotgun_mp = 0x169,
                    m16_silencer_mp = 0x16b,
                    m16_thermal_mp = 0x16c,
                    m16_xmags_mp = 0x16d,
                    m21_acog_fmj_mp = 0x330,
                    m21_acog_heartbeat_mp = 0x331,
                    m21_acog_mp = 810,
                    m21_acog_silencer_mp = 0x332,
                    m21_acog_xmags_mp = 0x333,
                    m21_fmj_heartbeat_mp = 820,
                    m21_fmj_mp = 0x32b,
                    m21_fmj_silencer_mp = 0x335,
                    m21_fmj_thermal_mp = 0x336,
                    m21_fmj_xmags_mp = 0x337,
                    m21_heartbeat_mp = 0x32c,
                    m21_heartbeat_silencer_mp = 0x338,
                    m21_heartbeat_thermal_mp = 0x339,
                    m21_heartbeat_xmags_mp = 0x33a,
                    m21_mp = 0x329,
                    m21_silencer_mp = 0x32d,
                    m21_silencer_thermal_mp = 0x33b,
                    m21_silencer_xmags_mp = 0x33c,
                    m21_thermal_mp = 0x32e,
                    m21_thermal_xmags_mp = 0x33d,
                    m21_xmags_mp = 0x32f,
                    m240_acog_fmj_mp = 0x431,
                    m240_acog_grip_mp = 0x432,
                    m240_acog_heartbeat_mp = 0x433,
                    m240_acog_mp = 0x428,
                    m240_acog_silencer_mp = 0x434,
                    m240_acog_xmags_mp = 0x435,
                    m240_eotech_fmj_mp = 0x436,
                    m240_eotech_grip_mp = 0x437,
                    m240_eotech_heartbeat_mp = 0x438,
                    m240_eotech_mp = 0x429,
                    m240_eotech_silencer_mp = 0x439,
                    m240_eotech_xmags_mp = 0x43a,
                    m240_fmj_grip_mp = 0x43b,
                    m240_fmj_heartbeat_mp = 0x43c,
                    m240_fmj_mp = 0x42a,
                    m240_fmj_reflex_mp = 0x43d,
                    m240_fmj_silencer_mp = 0x43e,
                    m240_fmj_thermal_mp = 0x43f,
                    m240_fmj_xmags_mp = 0x440,
                    m240_grip_heartbeat_mp = 0x441,
                    m240_grip_mp = 0x42b,
                    m240_grip_reflex_mp = 0x442,
                    m240_grip_silencer_mp = 0x443,
                    m240_grip_thermal_mp = 0x444,
                    m240_grip_xmags_mp = 0x445,
                    m240_heartbeat_mp = 0x42c,
                    m240_heartbeat_reflex_mp = 0x446,
                    m240_heartbeat_silencer_mp = 0x447,
                    m240_heartbeat_thermal_mp = 0x448,
                    m240_heartbeat_xmags_mp = 0x449,
                    m240_mp = 0x427,
                    m240_reflex_mp = 0x42d,
                    m240_reflex_silencer_mp = 0x44a,
                    m240_reflex_xmags_mp = 0x44b,
                    m240_silencer_mp = 0x42e,
                    m240_silencer_thermal_mp = 0x44c,
                    m240_silencer_xmags_mp = 0x44d,
                    m240_thermal_mp = 0x42f,
                    m240_thermal_xmags_mp = 0x44e,
                    m240_xmags_mp = 0x430,
                    m4_acog_fmj_mp = 0x1a1,
                    m4_acog_gl_mp = 0x1a2,
                    m4_acog_heartbeat_mp = 0x1a3,
                    m4_acog_mp = 0x195,
                    m4_acog_shotgun_mp = 420,
                    m4_acog_silencer_mp = 0x1a5,
                    m4_acog_xmags_mp = 0x1a6,
                    m4_eotech_fmj_mp = 0x1a7,
                    m4_eotech_gl_mp = 0x1a8,
                    m4_eotech_heartbeat_mp = 0x1a9,
                    m4_eotech_mp = 0x196,
                    m4_eotech_shotgun_mp = 0x1aa,
                    m4_eotech_silencer_mp = 0x1ab,
                    m4_eotech_xmags_mp = 0x1ac,
                    m4_fmj_gl_mp = 0x1ad,
                    m4_fmj_heartbeat_mp = 430,
                    m4_fmj_mp = 0x197,
                    m4_fmj_reflex_mp = 0x1af,
                    m4_fmj_shotgun_mp = 0x1b0,
                    m4_fmj_silencer_mp = 0x1b1,
                    m4_fmj_thermal_mp = 0x1b2,
                    m4_fmj_xmags_mp = 0x1b3,
                    m4_gl_heartbeat_mp = 0x1b4,
                    m4_gl_mp = 0x198,
                    m4_gl_reflex_mp = 0x1b5,
                    m4_gl_silencer_mp = 0x1b6,
                    m4_gl_thermal_mp = 0x1b7,
                    m4_gl_xmags_mp = 440,
                    m4_heartbeat_mp = 410,
                    m4_heartbeat_reflex_mp = 0x1b9,
                    m4_heartbeat_shotgun_mp = 0x1ba,
                    m4_heartbeat_silencer_mp = 0x1bb,
                    m4_heartbeat_thermal_mp = 0x1bc,
                    m4_heartbeat_xmags_mp = 0x1bd,
                    m4_mp = 0x194,
                    m4_reflex_mp = 0x19b,
                    m4_reflex_shotgun_mp = 0x1be,
                    m4_reflex_silencer_mp = 0x1bf,
                    m4_reflex_xmags_mp = 0x1c0,
                    m4_shotgun_attach_mp = 0x19d,
                    m4_shotgun_mp = 0x19c,
                    m4_shotgun_silencer_mp = 0x1c1,
                    m4_shotgun_thermal_mp = 450,
                    m4_shotgun_xmags_mp = 0x1c3,
                    m4_silencer_mp = 0x19e,
                    m4_silencer_thermal_mp = 0x1c4,
                    m4_silencer_xmags_mp = 0x1c5,
                    m4_thermal_mp = 0x19f,
                    m4_thermal_xmags_mp = 0x1c6,
                    m4_xmags_mp = 0x1a0,
                    m79_mp = 0x2fa,
                    masada_acog_fmj_mp = 0x207,
                    masada_acog_gl_mp = 520,
                    masada_acog_heartbeat_mp = 0x209,
                    masada_acog_mp = 0x1fb,
                    masada_acog_shotgun_mp = 0x20a,
                    masada_acog_silencer_mp = 0x20b,
                    masada_acog_xmags_mp = 0x20c,
                    masada_eotech_fmj_mp = 0x20d,
                    masada_eotech_gl_mp = 0x20e,
                    masada_eotech_heartbeat_mp = 0x20f,
                    masada_eotech_mp = 0x1fc,
                    masada_eotech_shotgun_mp = 0x210,
                    masada_eotech_silencer_mp = 0x211,
                    masada_eotech_xmags_mp = 530,
                    masada_fmj_gl_mp = 0x213,
                    masada_fmj_heartbeat_mp = 0x214,
                    masada_fmj_mp = 0x1fd,
                    masada_fmj_reflex_mp = 0x215,
                    masada_fmj_shotgun_mp = 0x216,
                    masada_fmj_silencer_mp = 0x217,
                    masada_fmj_thermal_mp = 0x218,
                    masada_fmj_xmags_mp = 0x219,
                    masada_gl_heartbeat_mp = 0x21a,
                    masada_gl_mp = 510,
                    masada_gl_reflex_mp = 0x21b,
                    masada_gl_silencer_mp = 540,
                    masada_gl_thermal_mp = 0x21d,
                    masada_gl_xmags_mp = 0x21e,
                    masada_heartbeat_mp = 0x200,
                    masada_heartbeat_reflex_mp = 0x21f,
                    masada_heartbeat_shotgun_mp = 0x220,
                    masada_heartbeat_silencer_mp = 0x221,
                    masada_heartbeat_thermal_mp = 0x222,
                    masada_heartbeat_xmags_mp = 0x223,
                    masada_mp = 0x1fa,
                    masada_reflex_mp = 0x201,
                    masada_reflex_shotgun_mp = 0x224,
                    masada_reflex_silencer_mp = 0x225,
                    masada_reflex_xmags_mp = 550,
                    masada_shotgun_attach_mp = 0x203,
                    masada_shotgun_mp = 0x202,
                    masada_shotgun_silencer_mp = 0x227,
                    masada_shotgun_thermal_mp = 0x228,
                    masada_shotgun_xmags_mp = 0x229,
                    masada_silencer_mp = 0x204,
                    masada_silencer_thermal_mp = 0x22a,
                    masada_silencer_xmags_mp = 0x22b,
                    masada_thermal_mp = 0x205,
                    masada_thermal_xmags_mp = 0x22c,
                    masada_xmags_mp = 0x206,
                    mg4_acog_fmj_mp = 0x409,
                    mg4_acog_grip_mp = 0x40a,
                    mg4_acog_heartbeat_mp = 0x40b,
                    mg4_acog_mp = 0x400,
                    mg4_acog_silencer_mp = 0x40c,
                    mg4_acog_xmags_mp = 0x40d,
                    mg4_eotech_fmj_mp = 0x40e,
                    mg4_eotech_grip_mp = 0x40f,
                    mg4_eotech_heartbeat_mp = 0x410,
                    mg4_eotech_mp = 0x401,
                    mg4_eotech_silencer_mp = 0x411,
                    mg4_eotech_xmags_mp = 0x412,
                    mg4_fmj_grip_mp = 0x413,
                    mg4_fmj_heartbeat_mp = 0x414,
                    mg4_fmj_mp = 0x402,
                    mg4_fmj_reflex_mp = 0x415,
                    mg4_fmj_silencer_mp = 0x416,
                    mg4_fmj_thermal_mp = 0x417,
                    mg4_fmj_xmags_mp = 0x418,
                    mg4_grip_heartbeat_mp = 0x419,
                    mg4_grip_mp = 0x403,
                    mg4_grip_reflex_mp = 0x41a,
                    mg4_grip_silencer_mp = 0x41b,
                    mg4_grip_thermal_mp = 0x41c,
                    mg4_grip_xmags_mp = 0x41d,
                    mg4_heartbeat_mp = 0x404,
                    mg4_heartbeat_reflex_mp = 0x41e,
                    mg4_heartbeat_silencer_mp = 0x41f,
                    mg4_heartbeat_thermal_mp = 0x420,
                    mg4_heartbeat_xmags_mp = 0x421,
                    mg4_mp = 0x3ff,
                    mg4_reflex_mp = 0x405,
                    mg4_reflex_silencer_mp = 0x422,
                    mg4_reflex_xmags_mp = 0x423,
                    mg4_silencer_mp = 0x406,
                    mg4_silencer_thermal_mp = 0x424,
                    mg4_silencer_xmags_mp = 0x425,
                    mg4_thermal_mp = 0x407,
                    mg4_thermal_xmags_mp = 0x426,
                    mg4_xmags_mp = 0x408,
                    model1887_akimbo_fmj_mp = 0x35a,
                    model1887_akimbo_mp = 0x358,
                    model1887_fmj_mp = 0x359,
                    model1887_mp = 0x357,
                    mp5k_acog_fmj_mp = 0x84,
                    mp5k_acog_mp = 0x7b,
                    mp5k_acog_rof_mp = 0x85,
                    mp5k_acog_silencer_mp = 0x86,
                    mp5k_acog_xmags_mp = 0x87,
                    mp5k_akimbo_fmj_mp = 0x88,
                    mp5k_akimbo_mp = 0x7c,
                    mp5k_akimbo_rof_mp = 0x89,
                    mp5k_akimbo_silencer_mp = 0x8a,
                    mp5k_akimbo_xmags_mp = 0x8b,
                    mp5k_eotech_fmj_mp = 140,
                    mp5k_eotech_mp = 0x7d,
                    mp5k_eotech_rof_mp = 0x8d,
                    mp5k_eotech_silencer_mp = 0x8e,
                    mp5k_eotech_xmags_mp = 0x8f,
                    mp5k_fmj_mp = 0x7e,
                    mp5k_fmj_reflex_mp = 0x90,
                    mp5k_fmj_rof_mp = 0x91,
                    mp5k_fmj_silencer_mp = 0x92,
                    mp5k_fmj_thermal_mp = 0x93,
                    mp5k_fmj_xmags_mp = 0x94,
                    mp5k_mp = 0x7a,
                    mp5k_reflex_mp = 0x7f,
                    mp5k_reflex_rof_mp = 0x95,
                    mp5k_reflex_silencer_mp = 150,
                    mp5k_reflex_xmags_mp = 0x97,
                    mp5k_rof_mp = 0x80,
                    mp5k_rof_silencer_mp = 0x98,
                    mp5k_rof_thermal_mp = 0x99,
                    mp5k_rof_xmags_mp = 0x9a,
                    mp5k_silencer_mp = 0x81,
                    mp5k_silencer_thermal_mp = 0x9b,
                    mp5k_silencer_xmags_mp = 0x9c,
                    mp5k_thermal_mp = 130,
                    mp5k_thermal_xmags_mp = 0x9d,
                    mp5k_xmags_mp = 0x83,
                    nuke_mp = 0x4a6,
                    onemanarmy_mp = 0x480,
                    p90_acog_fmj_mp = 0xcc,
                    p90_acog_mp = 0xc3,
                    p90_acog_rof_mp = 0xcd,
                    p90_acog_silencer_mp = 0xce,
                    p90_acog_xmags_mp = 0xcf,
                    p90_akimbo_fmj_mp = 0xd0,
                    p90_akimbo_mp = 0xc4,
                    p90_akimbo_rof_mp = 0xd1,
                    p90_akimbo_silencer_mp = 210,
                    p90_akimbo_xmags_mp = 0xd3,
                    p90_eotech_fmj_mp = 0xd4,
                    p90_eotech_mp = 0xc5,
                    p90_eotech_rof_mp = 0xd5,
                    p90_eotech_silencer_mp = 0xd6,
                    p90_eotech_xmags_mp = 0xd7,
                    p90_fmj_mp = 0xc6,
                    p90_fmj_reflex_mp = 0xd8,
                    p90_fmj_rof_mp = 0xd9,
                    p90_fmj_silencer_mp = 0xda,
                    p90_fmj_thermal_mp = 0xdb,
                    p90_fmj_xmags_mp = 220,
                    p90_mp = 0xc2,
                    p90_reflex_mp = 0xc7,
                    p90_reflex_rof_mp = 0xdd,
                    p90_reflex_silencer_mp = 0xde,
                    p90_reflex_xmags_mp = 0xdf,
                    p90_rof_mp = 200,
                    p90_rof_silencer_mp = 0xe0,
                    p90_rof_thermal_mp = 0xe1,
                    p90_rof_xmags_mp = 0xe2,
                    p90_silencer_mp = 0xc9,
                    p90_silencer_thermal_mp = 0xe3,
                    p90_silencer_xmags_mp = 0xe4,
                    p90_thermal_mp = 0xca,
                    p90_thermal_xmags_mp = 0xe5,
                    p90_xmags_mp = 0xcb,
                    pavelow_minigun_mp = 0x4a4,
                    pp2000_akimbo_fmj_mp = 0x5b,
                    pp2000_akimbo_mp = 0x55,
                    pp2000_akimbo_silencer_mp = 0x5c,
                    pp2000_akimbo_xmags_mp = 0x5d,
                    pp2000_eotech_fmj_mp = 0x5e,
                    pp2000_eotech_mp = 0x56,
                    pp2000_eotech_silencer_mp = 0x5f,
                    pp2000_eotech_xmags_mp = 0x60,
                    pp2000_fmj_mp = 0x57,
                    pp2000_fmj_reflex_mp = 0x61,
                    pp2000_fmj_silencer_mp = 0x62,
                    pp2000_fmj_xmags_mp = 0x63,
                    pp2000_mp = 0x54,
                    pp2000_reflex_mp = 0x58,
                    pp2000_reflex_silencer_mp = 100,
                    pp2000_reflex_xmags_mp = 0x65,
                    pp2000_silencer_mp = 0x59,
                    pp2000_silencer_xmags_mp = 0x66,
                    pp2000_xmags_mp = 90,
                    ranger_akimbo_fmj_mp = 0x356,
                    ranger_akimbo_mp = 0x354,
                    ranger_fmj_mp = 0x355,
                    ranger_mp = 0x353,
                    remotemissile_projectile_mp = 0x499,
                    riotshield_mp = 2,
                    rpd_acog_fmj_mp = 0x3b9,
                    rpd_acog_grip_mp = 0x3ba,
                    rpd_acog_heartbeat_mp = 0x3bb,
                    rpd_acog_mp = 0x3b0,
                    rpd_acog_silencer_mp = 0x3bc,
                    rpd_acog_xmags_mp = 0x3bd,
                    rpd_eotech_fmj_mp = 0x3be,
                    rpd_eotech_grip_mp = 0x3bf,
                    rpd_eotech_heartbeat_mp = 960,
                    rpd_eotech_mp = 0x3b1,
                    rpd_eotech_silencer_mp = 0x3c1,
                    rpd_eotech_xmags_mp = 0x3c2,
                    rpd_fmj_grip_mp = 0x3c3,
                    rpd_fmj_heartbeat_mp = 0x3c4,
                    rpd_fmj_mp = 0x3b2,
                    rpd_fmj_reflex_mp = 0x3c5,
                    rpd_fmj_silencer_mp = 0x3c6,
                    rpd_fmj_thermal_mp = 0x3c7,
                    rpd_fmj_xmags_mp = 0x3c8,
                    rpd_grip_heartbeat_mp = 0x3c9,
                    rpd_grip_mp = 0x3b3,
                    rpd_grip_reflex_mp = 970,
                    rpd_grip_silencer_mp = 0x3cb,
                    rpd_grip_thermal_mp = 0x3cc,
                    rpd_grip_xmags_mp = 0x3cd,
                    rpd_heartbeat_mp = 0x3b4,
                    rpd_heartbeat_reflex_mp = 0x3ce,
                    rpd_heartbeat_silencer_mp = 0x3cf,
                    rpd_heartbeat_thermal_mp = 0x3d0,
                    rpd_heartbeat_xmags_mp = 0x3d1,
                    rpd_mp = 0x3af,
                    rpd_reflex_mp = 0x3b5,
                    rpd_reflex_silencer_mp = 0x3d2,
                    rpd_reflex_xmags_mp = 0x3d3,
                    rpd_silencer_mp = 950,
                    rpd_silencer_thermal_mp = 980,
                    rpd_silencer_xmags_mp = 0x3d5,
                    rpd_thermal_mp = 0x3b7,
                    rpd_thermal_xmags_mp = 0x3d6,
                    rpd_xmags_mp = 0x3b8,
                    rpg_mp = 0x2fb,
                    sa80_acog_fmj_mp = 0x3e1,
                    sa80_acog_grip_mp = 0x3e2,
                    sa80_acog_heartbeat_mp = 0x3e3,
                    sa80_acog_mp = 0x3d8,
                    sa80_acog_silencer_mp = 0x3e4,
                    sa80_acog_xmags_mp = 0x3e5,
                    sa80_eotech_fmj_mp = 0x3e6,
                    sa80_eotech_grip_mp = 0x3e7,
                    sa80_eotech_heartbeat_mp = 0x3e8,
                    sa80_eotech_mp = 0x3d9,
                    sa80_eotech_silencer_mp = 0x3e9,
                    sa80_eotech_xmags_mp = 0x3ea,
                    sa80_fmj_grip_mp = 0x3eb,
                    sa80_fmj_heartbeat_mp = 0x3ec,
                    sa80_fmj_mp = 0x3da,
                    sa80_fmj_reflex_mp = 0x3ed,
                    sa80_fmj_silencer_mp = 0x3ee,
                    sa80_fmj_thermal_mp = 0x3ef,
                    sa80_fmj_xmags_mp = 0x3f0,
                    sa80_grip_heartbeat_mp = 0x3f1,
                    sa80_grip_mp = 0x3db,
                    sa80_grip_reflex_mp = 0x3f2,
                    sa80_grip_silencer_mp = 0x3f3,
                    sa80_grip_thermal_mp = 0x3f4,
                    sa80_grip_xmags_mp = 0x3f5,
                    sa80_heartbeat_mp = 0x3dc,
                    sa80_heartbeat_reflex_mp = 0x3f6,
                    sa80_heartbeat_silencer_mp = 0x3f7,
                    sa80_heartbeat_thermal_mp = 0x3f8,
                    sa80_heartbeat_xmags_mp = 0x3f9,
                    sa80_mp = 0x3d7,
                    sa80_reflex_mp = 0x3dd,
                    sa80_reflex_silencer_mp = 0x3fa,
                    sa80_reflex_xmags_mp = 0x3fb,
                    sa80_silencer_mp = 990,
                    sa80_silencer_thermal_mp = 0x3fc,
                    sa80_silencer_xmags_mp = 0x3fd,
                    sa80_thermal_mp = 0x3df,
                    sa80_thermal_xmags_mp = 0x3fe,
                    sa80_xmags_mp = 0x3e0,
                    scar_acog_fmj_mp = 0x2a0,
                    scar_acog_gl_mp = 0x2a1,
                    scar_acog_heartbeat_mp = 0x2a2,
                    scar_acog_mp = 660,
                    scar_acog_shotgun_mp = 0x2a3,
                    scar_acog_silencer_mp = 0x2a4,
                    scar_acog_xmags_mp = 0x2a5,
                    scar_eotech_fmj_mp = 0x2a6,
                    scar_eotech_gl_mp = 0x2a7,
                    scar_eotech_heartbeat_mp = 680,
                    scar_eotech_mp = 0x295,
                    scar_eotech_shotgun_mp = 0x2a9,
                    scar_eotech_silencer_mp = 0x2aa,
                    scar_eotech_xmags_mp = 0x2ab,
                    scar_fmj_gl_mp = 0x2ac,
                    scar_fmj_heartbeat_mp = 0x2ad,
                    scar_fmj_mp = 0x296,
                    scar_fmj_reflex_mp = 0x2ae,
                    scar_fmj_shotgun_mp = 0x2af,
                    scar_fmj_silencer_mp = 0x2b0,
                    scar_fmj_thermal_mp = 0x2b1,
                    scar_fmj_xmags_mp = 690,
                    scar_gl_heartbeat_mp = 0x2b3,
                    scar_gl_mp = 0x297,
                    scar_gl_reflex_mp = 0x2b4,
                    scar_gl_silencer_mp = 0x2b5,
                    scar_gl_thermal_mp = 0x2b6,
                    scar_gl_xmags_mp = 0x2b7,
                    scar_heartbeat_mp = 0x299,
                    scar_heartbeat_reflex_mp = 0x2b8,
                    scar_heartbeat_shotgun_mp = 0x2b9,
                    scar_heartbeat_silencer_mp = 0x2ba,
                    scar_heartbeat_thermal_mp = 0x2bb,
                    scar_heartbeat_xmags_mp = 700,
                    scar_mp = 0x293,
                    scar_reflex_mp = 0x29a,
                    scar_reflex_shotgun_mp = 0x2bd,
                    scar_reflex_silencer_mp = 0x2be,
                    scar_reflex_xmags_mp = 0x2bf,
                    scar_shotgun_attach_mp = 0x29c,
                    scar_shotgun_mp = 0x29b,
                    scar_shotgun_silencer_mp = 0x2c0,
                    scar_shotgun_thermal_mp = 0x2c1,
                    scar_shotgun_xmags_mp = 0x2c2,
                    scar_silencer_mp = 0x29d,
                    scar_silencer_thermal_mp = 0x2c3,
                    scar_silencer_xmags_mp = 0x2c4,
                    scar_thermal_mp = 670,
                    scar_thermal_xmags_mp = 0x2c5,
                    scar_xmags_mp = 0x29f,
                    scavenger_bag_mp = 0x482,
                    semtex_mp = 0x47a,
                    smoke_grenade_mp = 0x47d,
                    spas12_eotech_fmj_mp = 0x3a1,
                    spas12_eotech_grip_mp = 930,
                    spas12_eotech_mp = 0x39b,
                    spas12_eotech_silencer_mp = 0x3a3,
                    spas12_eotech_xmags_mp = 0x3a4,
                    spas12_fmj_grip_mp = 0x3a5,
                    spas12_fmj_mp = 0x39c,
                    spas12_fmj_reflex_mp = 0x3a6,
                    spas12_fmj_silencer_mp = 0x3a7,
                    spas12_fmj_xmags_mp = 0x3a8,
                    spas12_grip_mp = 0x39d,
                    spas12_grip_reflex_mp = 0x3a9,
                    spas12_grip_silencer_mp = 0x3aa,
                    spas12_grip_xmags_mp = 0x3ab,
                    spas12_mp = 0x39a,
                    spas12_reflex_mp = 0x39e,
                    spas12_reflex_silencer_mp = 940,
                    spas12_reflex_xmags_mp = 0x3ad,
                    spas12_silencer_mp = 0x39f,
                    spas12_silencer_xmags_mp = 0x3ae,
                    spas12_xmags_mp = 0x3a0,
                    stealth_bomb_mp = 0x49a,
                    stinger_mp = 0x2fd,
                    striker_eotech_fmj_mp = 0x362,
                    striker_eotech_grip_mp = 0x363,
                    striker_eotech_mp = 860,
                    striker_eotech_silencer_mp = 0x364,
                    striker_eotech_xmags_mp = 0x365,
                    striker_fmj_grip_mp = 870,
                    striker_fmj_mp = 0x35d,
                    striker_fmj_reflex_mp = 0x367,
                    striker_fmj_silencer_mp = 0x368,
                    striker_fmj_xmags_mp = 0x369,
                    striker_grip_mp = 0x35e,
                    striker_grip_reflex_mp = 0x36a,
                    striker_grip_silencer_mp = 0x36b,
                    striker_grip_xmags_mp = 0x36c,
                    striker_mp = 0x35b,
                    striker_reflex_mp = 0x35f,
                    striker_reflex_silencer_mp = 0x36d,
                    striker_reflex_xmags_mp = 0x36e,
                    striker_silencer_mp = 0x360,
                    striker_silencer_xmags_mp = 0x36f,
                    striker_xmags_mp = 0x361,
                    tavor_acog_fmj_mp = 0x2d3,
                    tavor_acog_gl_mp = 0x2d4,
                    tavor_acog_heartbeat_mp = 0x2d5,
                    tavor_acog_mp = 0x2c7,
                    tavor_acog_shotgun_mp = 0x2d6,
                    tavor_acog_silencer_mp = 0x2d7,
                    tavor_acog_xmags_mp = 0x2d8,
                    tavor_eotech_fmj_mp = 0x2d9,
                    tavor_eotech_gl_mp = 730,
                    tavor_eotech_heartbeat_mp = 0x2db,
                    tavor_eotech_mp = 0x2c8,
                    tavor_eotech_shotgun_mp = 0x2dc,
                    tavor_eotech_silencer_mp = 0x2dd,
                    tavor_eotech_xmags_mp = 0x2de,
                    tavor_fmj_gl_mp = 0x2df,
                    tavor_fmj_heartbeat_mp = 0x2e0,
                    tavor_fmj_mp = 0x2c9,
                    tavor_fmj_reflex_mp = 0x2e1,
                    tavor_fmj_shotgun_mp = 0x2e2,
                    tavor_fmj_silencer_mp = 0x2e3,
                    tavor_fmj_thermal_mp = 740,
                    tavor_fmj_xmags_mp = 0x2e5,
                    tavor_gl_heartbeat_mp = 0x2e6,
                    tavor_gl_mp = 0x2ca,
                    tavor_gl_reflex_mp = 0x2e7,
                    tavor_gl_silencer_mp = 0x2e8,
                    tavor_gl_thermal_mp = 0x2e9,
                    tavor_gl_xmags_mp = 0x2ea,
                    tavor_heartbeat_mp = 0x2cc,
                    tavor_heartbeat_reflex_mp = 0x2eb,
                    tavor_heartbeat_shotgun_mp = 0x2ec,
                    tavor_heartbeat_silencer_mp = 0x2ed,
                    tavor_heartbeat_thermal_mp = 750,
                    tavor_heartbeat_xmags_mp = 0x2ef,
                    tavor_mp = 710,
                    tavor_reflex_mp = 0x2cd,
                    tavor_reflex_shotgun_mp = 0x2f0,
                    tavor_reflex_silencer_mp = 0x2f1,
                    tavor_reflex_xmags_mp = 0x2f2,
                    tavor_shotgun_attach_mp = 0x2cf,
                    tavor_shotgun_mp = 0x2ce,
                    tavor_shotgun_silencer_mp = 0x2f3,
                    tavor_shotgun_thermal_mp = 0x2f4,
                    tavor_shotgun_xmags_mp = 0x2f5,
                    tavor_silencer_mp = 720,
                    tavor_silencer_thermal_mp = 0x2f6,
                    tavor_silencer_xmags_mp = 0x2f7,
                    tavor_thermal_mp = 0x2d1,
                    tavor_thermal_xmags_mp = 760,
                    tavor_xmags_mp = 0x2d2,
                    throwingknife_mp = 0x47f,
                    throwingknife_rhand_mp = 0x4a9,
                    tmp_akimbo_fmj_mp = 110,
                    tmp_akimbo_mp = 0x68,
                    tmp_akimbo_silencer_mp = 0x6f,
                    tmp_akimbo_xmags_mp = 0x70,
                    tmp_eotech_fmj_mp = 0x71,
                    tmp_eotech_mp = 0x69,
                    tmp_eotech_silencer_mp = 0x72,
                    tmp_eotech_xmags_mp = 0x73,
                    tmp_fmj_mp = 0x6a,
                    tmp_fmj_reflex_mp = 0x74,
                    tmp_fmj_silencer_mp = 0x75,
                    tmp_fmj_xmags_mp = 0x76,
                    tmp_mp = 0x67,
                    tmp_reflex_mp = 0x6b,
                    tmp_reflex_silencer_mp = 0x77,
                    tmp_reflex_xmags_mp = 120,
                    tmp_silencer_mp = 0x6c,
                    tmp_silencer_xmags_mp = 0x79,
                    tmp_xmags_mp = 0x6d,
                    ump45_acog_fmj_mp = 0x114,
                    ump45_acog_mp = 0x10b,
                    ump45_acog_rof_mp = 0x115,
                    ump45_acog_silencer_mp = 0x116,
                    ump45_acog_xmags_mp = 0x117,
                    ump45_akimbo_fmj_mp = 280,
                    ump45_akimbo_mp = 0x10c,
                    ump45_akimbo_rof_mp = 0x119,
                    ump45_akimbo_silencer_mp = 0x11a,
                    ump45_akimbo_xmags_mp = 0x11b,
                    ump45_eotech_fmj_mp = 0x11c,
                    ump45_eotech_mp = 0x10d,
                    ump45_eotech_rof_mp = 0x11d,
                    ump45_eotech_silencer_mp = 0x11e,
                    ump45_eotech_xmags_mp = 0x11f,
                    ump45_fmj_mp = 270,
                    ump45_fmj_reflex_mp = 0x120,
                    ump45_fmj_rof_mp = 0x121,
                    ump45_fmj_silencer_mp = 290,
                    ump45_fmj_thermal_mp = 0x123,
                    ump45_fmj_xmags_mp = 0x124,
                    ump45_mp = 0x10a,
                    ump45_reflex_mp = 0x10f,
                    ump45_reflex_rof_mp = 0x125,
                    ump45_reflex_silencer_mp = 0x126,
                    ump45_reflex_xmags_mp = 0x127,
                    ump45_rof_mp = 0x110,
                    ump45_rof_silencer_mp = 0x128,
                    ump45_rof_thermal_mp = 0x129,
                    ump45_rof_xmags_mp = 0x12a,
                    ump45_silencer_mp = 0x111,
                    ump45_silencer_thermal_mp = 0x12b,
                    ump45_silencer_xmags_mp = 300,
                    ump45_thermal_mp = 0x112,
                    ump45_thermal_xmags_mp = 0x12d,
                    ump45_xmags_mp = 0x113,
                    usp_akimbo_fmj_mp = 0x18,
                    usp_akimbo_mp = 0x13,
                    usp_akimbo_silencer_mp = 0x19,
                    usp_akimbo_xmags_mp = 0x1a,
                    usp_fmj_mp = 20,
                    usp_fmj_silencer_mp = 0x1b,
                    usp_fmj_tactical_mp = 0x1c,
                    usp_fmj_xmags_mp = 0x1d,
                    usp_mp = 0x12,
                    usp_silencer_mp = 0x15,
                    usp_silencer_tactical_mp = 30,
                    usp_silencer_xmags_mp = 0x1f,
                    usp_tactical_mp = 0x16,
                    usp_tactical_xmags_mp = 0x20,
                    usp_xmags_mp = 0x17,
                    uzi_acog_fmj_mp = 0xa8,
                    uzi_acog_mp = 0x9f,
                    uzi_acog_rof_mp = 0xa9,
                    uzi_acog_silencer_mp = 170,
                    uzi_acog_xmags_mp = 0xab,
                    uzi_akimbo_fmj_mp = 0xac,
                    uzi_akimbo_mp = 160,
                    uzi_akimbo_rof_mp = 0xad,
                    uzi_akimbo_silencer_mp = 0xae,
                    uzi_akimbo_xmags_mp = 0xaf,
                    uzi_eotech_fmj_mp = 0xb0,
                    uzi_eotech_mp = 0xa1,
                    uzi_eotech_rof_mp = 0xb1,
                    uzi_eotech_silencer_mp = 0xb2,
                    uzi_eotech_xmags_mp = 0xb3,
                    uzi_fmj_mp = 0xa2,
                    uzi_fmj_reflex_mp = 180,
                    uzi_fmj_rof_mp = 0xb5,
                    uzi_fmj_silencer_mp = 0xb6,
                    uzi_fmj_thermal_mp = 0xb7,
                    uzi_fmj_xmags_mp = 0xb8,
                    uzi_mp = 0x9e,
                    uzi_reflex_mp = 0xa3,
                    uzi_reflex_rof_mp = 0xb9,
                    uzi_reflex_silencer_mp = 0xba,
                    uzi_reflex_xmags_mp = 0xbb,
                    uzi_rof_mp = 0xa4,
                    uzi_rof_silencer_mp = 0xbc,
                    uzi_rof_thermal_mp = 0xbd,
                    uzi_rof_xmags_mp = 190,
                    uzi_silencer_mp = 0xa5,
                    uzi_silencer_thermal_mp = 0xbf,
                    uzi_silencer_xmags_mp = 0xc0,
                    uzi_thermal_mp = 0xa6,
                    uzi_thermal_xmags_mp = 0xc1,
                    uzi_xmags_mp = 0xa7,
                    wa2000_acog_fmj_mp = 0x31b,
                    wa2000_acog_heartbeat_mp = 0x31c,
                    wa2000_acog_mp = 0x315,
                    wa2000_acog_silencer_mp = 0x31d,
                    wa2000_acog_xmags_mp = 0x31e,
                    wa2000_fmj_heartbeat_mp = 0x31f,
                    wa2000_fmj_mp = 790,
                    wa2000_fmj_silencer_mp = 800,
                    wa2000_fmj_thermal_mp = 0x321,
                    wa2000_fmj_xmags_mp = 0x322,
                    wa2000_heartbeat_mp = 0x317,
                    wa2000_heartbeat_silencer_mp = 0x323,
                    wa2000_heartbeat_thermal_mp = 0x324,
                    wa2000_heartbeat_xmags_mp = 0x325,
                    wa2000_mp = 0x314,
                    wa2000_silencer_mp = 0x318,
                    wa2000_silencer_thermal_mp = 0x326,
                    wa2000_silencer_xmags_mp = 0x327,
                    wa2000_thermal_mp = 0x319,
                    wa2000_thermal_xmags_mp = 0x328,
                    wa2000_xmags_mp = 0x31a
                }
            }
        }

        public static class SinglePlayer
        {
            public static void Cbuf_AddText(string Text)
            {
                byte[] buffer = new byte[0x530];
                byte[] buffer2 = new byte[] { 0x38, 0x60, 0, 0, 60, 0x80, 2, 0x10, 0x30, 0x84, 80, 0, 0x4b, 250, 0x6d, 0x95 };
                byte[] buffer3 = new byte[] { 
                    0x81, 0x22, 0x38, 0xfc, 0x81, 0x69, 0, 0, 0x88, 11, 0, 12, 0x2f, 0x80, 0, 0, 
                    0x41, 0x9e, 0, 140, 0x80, 0x7b, 0, 0, 0x7c, 0x63, 7, 180, 0x4b, 0xdf, 0xa5, 0x55
                 };
                byte[] bytes = new byte[0];
                bytes = Encoding.UTF8.GetBytes(Text);
                Mw2Library.PS3.SetMemory(0x2105000, bytes);
                Mw2Library.PS3.SetMemory(0x2860d0, buffer2);
                Thread.Sleep(15);
                Mw2Library.PS3.SetMemory(0x2860d0, buffer3);
                Mw2Library.PS3.SetMemory(0x2105000, buffer);
            }
        }
    }


