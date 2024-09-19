using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class Aimbot
    {
       
        public class Offsets
        {
            public static uint
                VectoAngles = 0x2590A8,
                SetClientViewAngles = 0x16CBE0,
                G_Client = 0x14E2200,
                G_ClientSize = 0x3700,
                G_Entity = 0x1319800,
                G_EntitySize = 0x280;
        }

        public class Buttons
        {
            public static string
                DpadUp = "+actionslot 1",
                DpadDown = "+actionslot 2",
                DpadRight = "+actionslot 4",
                DpadLeft = "+actionslot 3",
                Cross = "+gostand",
                Circle = "+stance",
                Triangle = "weapnext",
                Square = "+usereload",
                R3 = "+melee",
                R2 = "+frag",
                R1 = "+attack",
                L3 = "+breath_sprint",
                L2 = "+smoke",
                L1 = "+speed_throw",
                Select = "togglescores",
                Start = "togglemenu";
        }
        public static bool ButtonPressed(int client, string Button)
        {
            if (ObrisBaseV3.Form1.PS3.Extension.ReadString(0x34750E9F + ((uint)client * 0x97F80)) == Button)
                return true;
            else return false;
        }

        public static uint G_Client(int Client, uint Mod = 0x0)
        {
            return Offsets.G_Client + (Offsets.G_ClientSize * (uint)Client) + Mod;
        }

        public static uint G_Entity(int Client, uint Mod = 0x0)
        {
            return Offsets.G_Entity + (Offsets.G_EntitySize * (uint)Client) + Mod;
        }

        public static bool ReturnPlayerActivity(int Client)
        {
            return ObrisBaseV3.Form1.PS3.Extension.ReadString(G_Client(Client, 0x3290)) != "";
        }

        public static bool ReturnPlayerLifeStatus(int Client)
        {
            return ObrisBaseV3.Form1.PS3.Extension.ReadByte(G_Client(Client, 0x345C)) != 0x01;
        }

        public static float[] ReturnOrigin(int Client)
        {
            float[] Origin = new float[3];
            Origin[0] = ObrisBaseV3.Form1.PS3.Extension.ReadFloat(G_Client(Client, 0x1C));
            Origin[1] = ObrisBaseV3.Form1.PS3.Extension.ReadFloat(G_Client(Client, 0x20));
            Origin[2] = ObrisBaseV3.Form1.PS3.Extension.ReadFloat(G_Client(Client, 0x24));

            return Origin;
        }

        public static int ReturnNearestPlayer(int Client)
        {
            int NearestPlayer = -1;
            float Closest = 0xFFFFFFFF;
            float[] Distance3D = new float[3];
            float Difference = new float();
            for (int i = 0; i < 18; i++)
            {
                Distance3D[0] = ReturnOrigin(i)[0] - ReturnOrigin(Client)[0];
                Distance3D[1] = ReturnOrigin(i)[1] - ReturnOrigin(Client)[1];
                Distance3D[2] = ReturnOrigin(i)[2] - ReturnOrigin(Client)[2];

                Difference = (float)(Math.Sqrt((Distance3D[0] * Distance3D[0]) + (Distance3D[1] * Distance3D[1]) + (Distance3D[2] * Distance3D[2])));

                if ((i != Client))
                {
                    if (ReturnPlayerActivity(i) && ReturnPlayerLifeStatus(i))
                    {
                        if (Difference < Closest)
                        {
                            NearestPlayer = i;
                            Closest = Difference;
                        }
                    }
                }
            }
            return NearestPlayer;
        }

        public static void SetClientViewAngles(int Client, float[] Angles)
        {
            ObrisBaseV3.Form1.PS3.Extension.WriteFloat(0x10004000, Angles[0]);
            ObrisBaseV3.Form1.PS3.Extension.WriteFloat(0x10004004, Angles[1]);
            ObrisBaseV3.Form1.PS3.Extension.WriteFloat(0x10004008, Angles[2]);
            RPC1.Call(Offsets.VectoAngles, 0x10004000, 0x1000400C);
            RPC1.Call(Offsets.SetClientViewAngles, G_Entity(Client), 0x1000400C);
        }

        public static void DoAimbot(int Client)
        {
            if (ButtonPressed(Client, Buttons.L1) || ButtonPressed(Client, Buttons.L1 + Buttons.R1))
            {
                SetClientViewAngles(Client, ReturnOrigin(ReturnNearestPlayer(Client)));
            }
        }
    }

