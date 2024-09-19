using PS3Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObrisBaseV3
{
    public partial class Form1 : Form
    {
        public Form1(){InitializeComponent();
            label7.ForeColor = Color.Red;
            label8.ForeColor = Color.Red;}
        private static PS3API DEX = new PS3API(); 
        public static PS3API PS3 = new PS3API(SelectAPI.TargetManager); 
        public int client { get; set; }        
        public static int[] CurPlayer = new int[18]; 
        int[] MenuScroll = new int[18]; 
        public static string[] Menu_Title = new string[18]; 
        bool[] MenuOpen = new bool[18]; 
        public static string[] Status = new string[18];
        bool[] IsVerified = new bool[18]; 
        string[] SubMenu = new string[18]; int[] MaxScroll = new int[18]; 
        public static string[] NewMenuStr = new string[18]; 
        public static uint fxBirthTime = 0x90; 
        public static uint fxDecayDuration = 0x9c; 
        public static uint fxDecayStartTime = 0x98; 
        public static uint fxLetterTime = 0x94; 
        public static string Names;
        public static string NamesVer; 
        public static string Statusz; 
        bool nigga; 
        bool MenuRunning;
        private void button1_Click(object sender, EventArgs e) {  } 
        private void button2_Click(object sender, EventArgs e) {  } 

       
        private void FastSpawnerz(int client)
        {
            
            SetIcon(120 + (client * 18), IsVerShader(client), -100, -100, client, 600, 900, 0, 0, 0, 0, 0, 0);//120 - background
            SetText(121 + (client * 18), IsVerText(client), 275, 25, client, "" + getMenu(SubMenu[client], client) + "", 99, 1.5, 999, 1, 255, 255, 255, 0, 0, 0, 153, 255);//" + getMenu(SubMenu[i], i) + "
            SetText(122 + (client * 18), IsVerText(client), 225, 390, client, "Project Hybrid", 99, 2.5, 1, 999, 255, 255, 255, 0, 255, 0, 0, 255);
            SetIcon(123 + (client * 18), IsVerShader(client), 250, 30, client, 12, 12, 1, 40, 255, 255, 255, 0);//189 - Scrollbar
            SetText(124 + (client * 18), IsVerText(client), -80, 210, client, " \n PRESS [{+attack}] / [{+speed_throw}] TO SCROLL \n PRESS [{+gostand}] TO SELECT \n Press [{+usereload}] TO GO BACK", 99, 1.5, 1, 999, 255, 255, 255, 0, 255, 255, 255, 0);//207 - dev by
            MatrixText(666 + (client * 18), IsVerText(client), 265, 110, client, "^2Welcome " + ClientNames((uint)dataGridView1.CurrentRow.Index) + " To", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 0, 0, 0, 0);
            MatrixText(667 + (client * 18), IsVerText(client), 190, 130, client, "^3Project Hybrid 1.14 Made by Mx444\n             ^5Enjoy The Menu", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 0, 0, 0, 0);
            MatrixText(668 + (client * 18), IsVerText(client), 155, 180, client, "^5Project Hybrid 1.14 , ^3Press [{+melee}] For Menu", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 255, 255, 255, 60);
            SetIcon(126 + (client * 18), IsVerShader(client), 175, 385, client, 40, 40, 1, 36, 255, 255, 255, 0);
            SetIcon(127 + (client * 18), IsVerShader(client), 455, 385, client, 40, 40, 1, 36, 255, 255, 255, 0);
            SetIcon(125 + (client * 18), IsVerShader(client), 315, 205, client, 50, 50, 1, 36, 255, 255, 255, 255);
            System.Threading.Thread.Sleep(8400);
            SetIcon(125 + (client * 18), IsVerShader(client), 315, 205, client, 50, 50, 1, 36, 255, 255, 255, 0);
        }
        private string getMenu(string status, int client)
        {
            if (SubMenu[client] == "Main")
            {
                if (status == "Verified")
                {
                    return "^6Verified Menu\nCredits";
                }
                else if (status == "VIP")
                {
                    return "Fun Menu\n^6Verified\n^3VIP Menu\n^7Vision Menu\nModel Menu\nWeapon Menu\nCredits";
                }
                else if (status == "Co-Host")
                {
                    return "Main Mods Menu\nFun Menu\nMessage Menu\n^6Verified Menu\n^3VIP Menu\n^5Co-Host Menu\n^7Vision Menu\nModel Menu\nWeapon Menu\nBullet Type Menu\nTeleport Menu\nMap Menu\nPlayers Menu\nExtra Menu\nCredits";
                }
                else
                {
                    return "Main Mods Menu\nFun Menu\nAimbot Menu\nMessage Menu\n^6Verified Menu\n^3VIP Menu\n^5Co-Host Menu\n^2Host Menu\n^7Vision Menu\nModel Menu\nWeapon Menu\nBullet Type Menu\nTeleport Menu\nMap Menu\nGame mode Menu\nLobby Menu\nPlayers Menu\nExtra Menu\nAll Players Menu\nCredits";
                }
            }
            else
            {
                return "";
            }
            
        }

        private string getMenuString(string curSub, int client)
        {
            if (curSub == "Main")
                return getMenu(SubMenu[client], client);
            else if (curSub == "Main Mods Menu")
                return "God mode\nNo Clip\nUFO mode\nInfinite Ammo\nRed Boxes\nConstant UAV\nFreeze\nField Of View\nSet All Perks\nExplosive Bullets\nNo Recoil\nSuicide\nChange Team\nAkimbo Primary\nAkimbo Secondary";
            else if (curSub == "Fun Menu")
                return "CrossHair +\nSpeed x2\nSpeed x4\nSpeed x6\nLaser\nWallhack";
            else if (curSub == "Aimbot Menu")
                return "Normal Aimbot\nMedium Aimbot\n180 Aimbot";
            else if (curSub == "Message Menu")
                return "Mod Menu\nWelcome Player\nYoutube\nYes\nNo\nCFG = ^6GAY";
            else if (curSub == "^6Verified Menu")
                return "Lefs Side Gun\nLaser\nUAV\nFOV\nCrosshair +";
            else if (curSub == "^3VIP Menu")
                return "God mode\nInfinite Ammo\nUFO mode\nAll Perks\nRed Boxes\nNo Recoil\nThird Person";
            else if (curSub == "^5Co-Host Menu")
                return "Skate Mod\nDisable Weapon Switch\nDisable Weapon\nDisable ADS\nNight Vision Dpad \nPing Text\nSpectator God mode";
            else if (curSub == "^2Host Menu")
                return "Force Host\nAnti Join\nAnti Quit\nAdd 1 Bot\nFPS";
            else if (curSub == "^7Vision Menu")
                return "Default\nac130_inverted\nairport_green\ncheat_bw\ncobra_sunset1\ncobra_sunset2\ncobra_sunset3\narmada_water\ncliffhanger_extreme\ncheat_invert\nRed\nBlue\nYello\nGreen\nNuke\nMissile Cam";
            else if (curSub == "Model Menu")
                return "Green Care Package\nRed Care Package\nS&D Bomb\nNetrual Flag \nLaptop\nHarriar\nAC130\nSentry Gun\nLittle Bird\nUAV\nPavelow\nDefault Vehicle\nInvisible\nDefault";
            else if (curSub == "Weapon Menu")
                return "Default Weapon\nAC130 25mm\nAC130 40mm\nAC130 105mm\nGold Desert Eagle\nM16A4 Nootube";
            else if (curSub == "Bullet Type Menu")
                return "Cobra 20mm\nCobra Minigun\nPavelow Miniun\nSentry Gun\nCobra Missiles\nHarrier 20mm\nHarrier Missiles\nLittle Bird 20mm";
            else if (curSub == "Teleport Menu")
                return "Save Position\nTeleport All To Position\nTeleport Everyone To Me\nTeleport All To Sky\nTeleport All To Space\nTeleport All Under Map";
            else if (curSub == "Map Menu")
                return "Afghan\nDerail\nEstate\nFavela\nInvasion\nKarachi\nRundown\nScrapyard\nSub Base\nTerminal\nUnderpass\nSkidrow\nWasteland";
            else if (curSub == "Players")
                return GetNames(client);
            else if (curSub == "Game mode Menu")
                return "Team Deathmatch\nFree For All\nDomination\nSearch And Destroy\nSabotage\nCapture The Flag\nHeadquarters\nGlobal Thermonuclear War";
            else if (curSub == "Lobby Menu")
                return "Lobby Pro Mod\nLobby Jump\nLobby Speed\nNo Fall Damage\nGravity\nTimescale\nKnock Back\nKnife Range\nNo Kill Cam\nRanked Match\nFast Restart\nUnlimited Nuke";
            else if (curSub == "PlayerOpts")
                return "Give Access\nGive VIP\nGive Co-Host\nRemove Access\nLevel 70\nPrestige 10\nUnlock All\nKick\nKick Whit Error\nGive Lag\nDerank\nKill And Scare\nBan Player\nTeleport To Sky\nTeleport To Space\nTeleport Under Map\nTeleport To Me\nTeleport Everyone To Client\nFreeze PS3\nRename";
            else if (curSub == "Extra Menu")
                return "Disable Jump\nDisable Sprint\nDefault\nAutoProne\nAuto Kill\nKill And Scare";
            else if (curSub == "All Players Menu")
                return "Level 70\nUnlock All\nPrestige 1\nPrestige 2\nPrestige 3\nPestige 4\nPrestige 5\nPrestige 6\nPrestige 7\nPrestige 8\nPrestige 9\nPrestige 10\nPrestige 11\nDerank All";
            else if (curSub == "Credits")
                return "";
            else
                return "Main";
        }

        private void MenuSelection(int client, int scrollNum, string Sub)
        {
            if (scrollNum == 0 && Sub == "Main")
            {
                LoadSub(client, "Main Mods Menu");
            }
            else if (scrollNum == 0 && Sub == "Main Mods Menu")
            {
                GodmodeTog1(client);
            }
            else if (scrollNum == 1 && Sub == "Main Mods Menu")
            {
                NoClip1(client);
            }
            else if (scrollNum == 2 && Sub == "Main Mods Menu")
            {
                BindNoClip1(client);
            }
            else if (scrollNum == 3 && Sub == "Main Mods Menu")
            {
                InfiniteAmmo1(client);
            }
            else if (scrollNum == 4 && Sub == "Main Mods Menu")
            {
                RedBoxes1(client);
            }
            else if (scrollNum == 5 && Sub == "Main Mods Menu")
            {
                UAV1(client);
            }
            else if (scrollNum == 6 && Sub == "Main Mods Menu")
            {
                Freeze1(client);
            }
            else if (scrollNum == 7 && Sub == "Main Mods Menu")
            {
                FOV(client);
            }
            else if (scrollNum == 8 && Sub == "Main Mods Menu")
            {
                AllPerks1(client);
            }
            else if (scrollNum == 9 && Sub == "Main Mods Menu")
            {
                Explosive1(client);
            }
            else if (scrollNum == 10 && Sub == "Main Mods Menu")
            {
                NoRecoil1(client);
            }
            else if (scrollNum == 11 && Sub == "Main Mods Menu")
            {
                Suicide1(client);
            }
            else if (scrollNum == 12 && Sub == "Main Mods Menu")
            {
                ChangeTeam1(client);
            }
            else if (scrollNum == 13 && Sub == "Main Mods Menu")
            {
                Akimbo1st(client);
            }
            else if (scrollNum == 14 && Sub == "Main Mods Menu")
            {
                Akimbo2st(client);
            }
            if (scrollNum == 1 && Sub == "Main")
            {
                LoadSub(client, "Fun Menu");
            }
            else if (scrollNum == 0 && Sub == "Fun Menu")
            {
                Cross1(client);
            }
            else if (scrollNum == 1 && Sub == "Fun Menu")
            {
                Speedx2(client);
            }
            else if (scrollNum == 2 && Sub == "Fun Menu")
            {
                Speedx4(client);
            }
            else if (scrollNum == 3 && Sub == "Fun Menu")
            {
                Speedx6(client);
            }
            else if (scrollNum == 4 && Sub == "Fun Menu")
            {
                Laser(client);
            }
            else if (scrollNum == 5 && Sub == "Fun Menu")
            {
                Mw2Library.Mods.Clients.Give.Wallhack(client);
            }

            if (scrollNum == 2 && Sub == "Main")
            {
                LoadSub(client, "Aimbot Menu");
            }
            else if (scrollNum == 0 && Sub == "Aimbot Menu")
            {
                NormalAimbot(client);
            }
            else if (scrollNum == 1 && Sub == "Aimbot Menu")
            {
                MediumAimbot(client);
            }
            else if (scrollNum == 2 && Sub == "Aimbot Menu")
            {
                Aimbot180(client);
            }
            else if (scrollNum == 3 && Sub == "Aimbot Menu")
            {

                StartAimbot((uint)client);
            }
            if (scrollNum == 3 && Sub == "Main")
            {
                LoadSub(client, "Message Menu");
            }
            else if (scrollNum == 0 && Sub == "Message Menu")
            {

                MatrixText(669 + (client * 18), IsVerText(client), 155, 180, client, "Project Hybrid 1.14 by Mx444", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 255, 0, 0, 60);
   
            
            }
            else if (scrollNum == 1 && Sub == "Message Menu")

            {
                MatrixText(669 + (client * 18), IsVerText(client), 155, 180, client, "Welcome : " + ClientNames((uint)dataGridView1.CurrentRow.Index), 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 0, 255, 0, 60);

            }
            else if (scrollNum == 2 && Sub == "Message Menu")
            {
                MatrixText(669 + (client * 18), IsVerText(client), 155, 180, client, "www.YouTube.com/444xMoDz", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 0, 0, 255, 60);

            }
            else if (scrollNum == 3 && Sub == "Message Menu")
            {
                MatrixText(669 + (client * 18), IsVerText(client), 155, 180, client, "Yes", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 0, 255, 255, 60);

            }
            else if (scrollNum == 4 && Sub == "Message Menu")
            {
                MatrixText(669 + (client * 18), IsVerText(client), 155, 180, client, "No", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 255, 255, 0, 60);

            }
            else if (scrollNum == 5 && Sub == "Message Menu")
            {
                MatrixText(669 + (client * 18), IsVerText(client), 155, 180, client, "CFG = ^6GAY", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 255, 255, 0, 60);

            }
            else if (scrollNum == 4 && Sub == "Main")
            {
                LoadSub(client, "^6Verified Menu");
            }
            else if (scrollNum == 0 && Sub == "^6Verified Menu")
            {
                Left1(client);
            }
            else if (scrollNum == 1 && Sub == "^6Verified Menu")
            {
                Laser(client);
            }
            else if (scrollNum == 2 && Sub == "^6Verified Menu")
            {
                UAV1(client);
            }
            else if (scrollNum == 3 && Sub == "^6Verified Menu")
            {
                FOV(client);
            }
            else if (scrollNum == 4 && Sub == "^6Verified Menu")
            {
                Cross1(client);
            }
            else if (scrollNum == 5 && Sub == "Main")
            {
                LoadSub(client, "^3VIP Menu");
            }
            else if (scrollNum == 0 && Sub == "^3VIP Menu")
            {
                GodmodeTog1(client);
            }
            else if (scrollNum == 1 && Sub == "^3VIP Menu")
            {
               InfiniteAmmo1(client);
            }
            else if (scrollNum == 2 && Sub == "^3VIP Menu")
            {
                BindNoClip1(client);
            }
            else if (scrollNum == 3 && Sub == "^3VIP Menu")
            {
                
                AllPerks1(client);
            }
            else if (scrollNum == 4 && Sub == "^3VIP Menu")
            {
                RedBoxes1(client);
            }
            else if (scrollNum == 5 && Sub == "^3VIP Menu")
            {
                NoRecoil1(client);
            }
            else if (scrollNum == 6 && Sub == "^3VIP Menu")
            {
                ThirdPerson(client);
            }
            else if (scrollNum == 6 && Sub == "Main")
            {
                LoadSub(client, "^5Co-Host Menu");
            }
            else if (scrollNum == 0 && Sub == "^5Co-Host Menu")
            {
                Skatemodz(client);
            }
            else if (scrollNum == 1 && Sub == "^5Co-Host Menu")
            {
                DWeapon1(client);
            }
            else if (scrollNum == 2 && Sub == "^5Co-Host Menu")
            {
                DWeapon11(client);
            }
            else if (scrollNum == 3 && Sub == "^5Co-Host Menu")
            {
                ADS(client);
            }
            else if (scrollNum == 4 && Sub == "^5Co-Host Menu")
            {
                Night(client);   
            }
            else if (scrollNum == 5 && Sub == "^5Co-Host Menu")
            {
                PingT(client);
               
            }
            else if (scrollNum == 6 && Sub == "^5Co-Host Menu")
            {
                GodSp(client);

            }
            else if (scrollNum == 7 && Sub == "Main")
            {
                LoadSub(client, "^2Host Menu");
            }
            else if (scrollNum == 0 && Sub == "^2Host Menu")
            {
                ForceHost(client);
            }
            else if (scrollNum == 1 && Sub == "^2Host Menu")
            {
                Join(client);
            }
            else if (scrollNum == 2 && Sub == "^2Host Menu")
            {
                quit(client);
            }
            else if (scrollNum == 3 && Sub == "^2Host Menu")
            {
                Mw2Library.RPC.Bots(1);
                iPrintln(client,"Added :^2 1 Bot");
            }
            else if (scrollNum == 4 && Sub == "^2Host Menu")
            {
                FPS1(client);
            }
            else if (scrollNum == 8 && Sub == "Main")
            {
                LoadSub(client, "^7Vision Menu");
            }
            else if (scrollNum == 0 && Sub == "^7Vision Menu")
            {
                Vision(client,"default");
                iPrintlnBold(client, "^2Vision Set To :^3Default Vision");
            }
            else if (scrollNum == 1 && Sub == "^7Vision Menu")
            {
                Vision(client, "ac130_inverted");
                iPrintlnBold(client, "^2Vision Set To :^3ac130_inverted");
            }
            else if (scrollNum == 2 && Sub == "^7Vision Menu")
            {
                Vision(client, "airport_green");
                iPrintlnBold(client, "^2Vision Set To :^3airport_green");
            }
            else if (scrollNum == 3 && Sub == "^7Vision Menu")
            {
                Vision(client, "cheat_bw");
                iPrintlnBold(client, "^2Vision Set To :^3chet_bw");
            }
            else if (scrollNum == 4 && Sub == "^7Vision Menu")
            {
                Vision(client, "cobra_sunset1");
                iPrintlnBold(client, "^2Vision Set To :^32cobra_sunset1");
            }
            else if (scrollNum == 5 && Sub == "^7Vision Menu")
            {
                Vision(client, "cobra_sunset2");
                iPrintlnBold(client, "^2Vision Set To :^32cobra_sunset2");
            }
            else if (scrollNum == 6 && Sub == "^7Vision Menu")
            {
                Vision(client, "cobra_sunset3");
                iPrintlnBold(client, "^2Vision Set To :^3cobra_sunset3");
            }
            else if (scrollNum == 7 && Sub == "^7Vision Menu")
            {
                Vision(client, "armada_water");
                iPrintlnBold(client, "^2Vision Set To :^3armanda_water");
            }
            else if (scrollNum == 8 && Sub == "^7Vision Menu")
            {
                Vision(client, "cliffhanger_extreme");
                iPrintlnBold(client, "^2Vision Set To :^3cliffhanger_extreme");
            }
            else if (scrollNum == 9 && Sub == "^7Vision Menu")
            {
                Vision(client, "cheat_invert");
                iPrintlnBold(client, "^2Vision Set To :^3cheat_invert");

            }
            else if (scrollNum == 10 && Sub == "^7Vision Menu")
            {
                Mw2Library.Mods.Clients.Visions.Colors.Red(client);
            }
            else if (scrollNum == 11 && Sub == "^7Vision Menu")
            {
                Mw2Library.Mods.Clients.Visions.Colors.Blue(client);
            }
            else if (scrollNum == 12 && Sub == "^7Vision Menu")
            {
                Mw2Library.Mods.Clients.Visions.Colors.Yellow(client);
            }
            else if (scrollNum == 13 && Sub == "^7Vision Menu")
            {
                Mw2Library.Mods.Clients.Visions.Colors.Green(client);
            }
            else if (scrollNum == 14 && Sub == "^7Vision Menu")
            {
                Mw2Library.Mods.Clients.Visions.Nuke.Nuke_mp(client);
            }
            else if (scrollNum == 15 && Sub == "^7Vision Menu")
            {
                Mw2Library.Mods.Clients.Visions.MissileCam(client);
            }
            else if (scrollNum == 9 && Sub == "Main")
            {
                LoadSub(client, "Model Menu");
            }
            else if (scrollNum == 0 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.CarePackage_Friendly);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Friendly Care Package");
            }
            else if (scrollNum == 1 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint) client, Mw2Library.Offsets.Models.CarePackage_Enemy);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Enemy Care Package");
            }
             else if (scrollNum == 2 && Sub == "Model Menu")
            {
            Mw2Library.Offsets.Models.SetModel((uint) client, Mw2Library.Offsets.Models.S_And_D_Bomb);
            Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Charge");
            }
              else if (scrollNum == 3 && Sub == "Model Menu")
            {
             Mw2Library.Offsets.Models.SetModel((uint) client, Mw2Library.Offsets.Models.S_And_D_Bomb);
            Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Charge");
            }
            else if (scrollNum == 4 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.Netrual_Flag);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Netrual Flag");
            }
            else if (scrollNum == 5 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.Laptop);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Laptop");
            }
            else if (scrollNum == 6 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.Harriar_White);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Harriar");
            }
            else if (scrollNum == 7 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.AC130);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2AC-130");
            }
            else if (scrollNum == 8 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.SentryGun);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Sentry Gun");
            }
            else if (scrollNum == 9 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.LittleBird);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Little Bird");
            }
            else if (scrollNum == 10 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.UAV);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2UAV");
            }
            else if (scrollNum == 11 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint)client, Mw2Library.Offsets.Models.Pavelow_White);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Pavelow");
            }
            else if (scrollNum == 12 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint) client, Mw2Library.Offsets.Models.Default_Vehicle);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Default Vehicle");
            }
              else if (scrollNum == 13 && Sub == "Model Menu")
            {
                Mw2Library.Offsets.Models.SetModel((uint) client, Mw2Library.Offsets.Models.Invisible);
                Mw2Library.RPC.iPrintln(client, "^1Model set to: ^2Invisible");
            }
               else if (scrollNum == 14 && Sub == "Model Menu")
            {
               Mw2Library.Offsets.Models.SetModel((uint) client, Mw2Library.Offsets.Models.Default_TF141);
               Mw2Library.RPC.iPrintln(client, "^1Model: ^2Reset");
           }
            else if (scrollNum == 10 && Sub == "Main")
            {
                LoadSub(client, "Weapon Menu");
            }
            else if (scrollNum == 0 && Sub == "Weapon Menu")
            {
                Mw2Library.Mods.Clients.Weapon.DefaultWeapon(client);
            }
            else if (scrollNum == 1 && Sub == "Weapon Menu")
            {
                Mw2Library.Mods.Clients.Weapon.AC130._25mm(client);
            }
            else if (scrollNum == 2 && Sub == "Weapon Menu")
            {
                Mw2Library.Mods.Clients.Weapon.AC130._40mm(client);
            }
            else if (scrollNum == 3 && Sub == "Weapon Menu")
            {
                Mw2Library.Mods.Clients.Weapon.AC130._105mm(client);
            }
            else if (scrollNum == 4 && Sub == "Weapon Menu")
            {
                Mw2Library.Mods.Clients.Weapon.GoldDesertEagle(client);
            }
            else if (scrollNum == 5 && Sub == "Weapon Menu")
            {
                Mw2Library.Mods.Clients.Weapon.M16A4Noobtube(client);
            }
            else if (scrollNum == 11 && Sub == "Main")
            {
                LoadSub(client, "Bullet Type Menu");
            }
            else if (scrollNum == 0 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.Cobra20mm(client);
            }
            else if (scrollNum == 1 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.CobraMinigun(client);
            }
            else if (scrollNum == 2 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.PavelowMinigun(client);
            }
            else if (scrollNum == 3 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.SentryMinigun(client);
            }
            else if (scrollNum == 4 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.CobraMissiles(client);
            }
            else if (scrollNum == 5 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.Harrier20mm(client);
            }
            else if (scrollNum == 6 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.HarrierMissiles(client);
            }
            else if (scrollNum == 7 && Sub == "Bullet Type Menu")
            {
                Mw2Library.Mods.Clients.Bullets.LittleBird20mm(client);
            }
            else if (scrollNum == 12 && Sub == "Main")
            {
                LoadSub(client, "Teleport Menu");
            }
            else if (scrollNum == 0 && Sub == "Teleport Menu")
            {
                Mw2Library.Mods.Clients.Teleport.SavePosition(client);
            }
            else if (scrollNum == 1 && Sub == "Teleport Menu")
            {
                Mw2Library.Mods.Clients.Teleport.EveryoneToPosition(client);
            }
            else if (scrollNum == 2 && Sub == "Teleport Menu")
            {
                Mw2Library.Mods.Clients.Teleport.EveryoneToClient(client);
            }
            else if (scrollNum == 3 && Sub == "Teleport Menu")
            {
                Mw2Library.Mods.Clients.Teleport.ClientToSky(0);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(1);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(2);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(3);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(4);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(5);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(6);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(7);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(8);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(9);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(10);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(11);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(12);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(13);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(14);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(15);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(16);
                Mw2Library.Mods.Clients.Teleport.ClientToSky(17);
            }
            else if (scrollNum == 4 && Sub == "Teleport Menu")
            {
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(0);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(1);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(2);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(3);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(4);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(5);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(6);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(7);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(8);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(9);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(10);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(11);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(12);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(13);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(14);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(15);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(16);
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(17);
            }
            else if (scrollNum == 5 && Sub == "Teleport Menu")
            {
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(0);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(1);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(2);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(3);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(4);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(5);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(6);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(7);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(8);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(9);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(10);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(11);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(12);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(13);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(14);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(15);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(16);
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(17);
            }
            else if (scrollNum == 13 && Sub == "Main")
            {
                LoadSub(client, "Map Menu");
            }
            else if (scrollNum == 0 && Sub == "Map Menu")
            {
                Cbuf_AddText(client,"map mp_afghan ");
            }
            else if (scrollNum == 1 && Sub == "Map Menu")
            {
                Cbuf_AddText(client,"map mp_derail ");
            }
            else if (scrollNum == 2 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_estate ");
            }
            else if (scrollNum == 3 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_favela ");
            }
            else if (scrollNum == 4 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_highrise ");
            }
            else if (scrollNum == 5 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_invasion ");
            }
            else if (scrollNum == 6 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_checkpoint ");
            }
            else if (scrollNum == 7 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_quarry ");
            }
            else if (scrollNum == 8 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_rundown ");
            }
            else if (scrollNum == 9 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_rust ");
            }
            else if (scrollNum == 10 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_boneyard ");
            }
            else if (scrollNum == 11 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_nightshift ");
            }
            else if (scrollNum == 12 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_subbase ");
            }
            else if (scrollNum == 13 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_terminal ");
            }
            else if (scrollNum == 14 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_underpass ");
            }
            else if (scrollNum == 15 && Sub == "Map Menu")
            {
                Cbuf_AddText(client, "map mp_brecourt ");
            }
            else if (scrollNum == 14 && Sub == "Main")
            {
                LoadSub(client, "Game mode Menu");
            }
            else if (scrollNum == 0 && Sub == "Game mode Menu")
            {
                Cbuf_AddText(client,"g_gametype war ");
                Thread.Sleep(100);
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 1 && Sub == "Game mode Menu")
            {
                Cbuf_AddText(client, "g_gametype dm ");
                Thread.Sleep(100);
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 2 && Sub == "Game mode Menu")
            {
               Cbuf_AddText(client,"g_gametype dom ");
                Thread.Sleep(100);
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 3 && Sub == "Game mode Menu")
            {
                Cbuf_AddText(client, "g_gametype sd ");
                Thread.Sleep(100);
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 4 && Sub == "Game mode Menu")
            {
                Cbuf_AddText(client, "g_gametype sab ");
                Thread.Sleep(100);
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 5 && Sub == "Game mode Menu")
            {
                Cbuf_AddText(client, "g_gametype ctf ");
                Thread.Sleep(100);
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 6 && Sub == "Game mode Menu")
            {
                Cbuf_AddText(client, "g_gametype koth ");
                Thread.Sleep(100);
                Cbuf_AddText(client, "fast_restart 1");
            }
            else if (scrollNum == 7 && Sub == "Game mode Menu")
            {
                Cbuf_AddText(client, "g_gametype gtnw ");
                Thread.Sleep(100);
                Cbuf_AddText(client,"fast_restart 1");
            }
            else if (scrollNum == 15 && Sub == "Main")
            {
                LoadSub(client, "Lobby Menu");
            }
            else if (scrollNum == 0 && Sub == "Lobby Menu")
            {
                ProMod(client);
            }
            else if (scrollNum == 1 && Sub == "Lobby Menu")
            {
                SuperJump(client);
            }
            else if (scrollNum == 2 && Sub == "Lobby Menu")
            {
                SuperSpeed(client);
            }
            else if (scrollNum == 3 && Sub == "Lobby Menu")
            {
                FallDamage(client);
            }
            else if (scrollNum == 4 && Sub == "Lobby Menu")
            {
                LobbyGravity(client);
            }
            else if (scrollNum == 5 && Sub == "Lobby Menu")
            {
                
                if (timerscale == false)
                {
                    Timescale(client);
                    timerscale = true;
                }
                else
                {
                    Thread.Sleep(10);
                    Mw2Library.RPC.Cbuf_AddText("reset fixedtime");
                    iPrintln(client, "Timescale : ^2Reset");
                    Thread.Sleep(10);
                    Mw2Library.RPC.Cbuf_AddText("reset fixedtime");
                    timerscale = false;
                  
                }
               
            }
            else if (scrollNum == 6 && Sub == "Lobby Menu")
            {
                KnockBack(client);
            }
            else if (scrollNum == 7 && Sub == "Lobby Menu")
            {
                KnifeRange(client);
            }
            else if (scrollNum == 8 && Sub == "Lobby Menu")
            {
                KillCam(client);
            }
            else if (scrollNum == 9 && Sub == "Lobby Menu")
            {
                Mw2Library.RPC.Cbuf_AddText("onlinegame 1");
                Mw2Library.RPC.Cbuf_AddText("onlinegameandhost 1");
                Mw2Library.RPC.Cbuf_AddText("xblive_privatematch 0");
                Mw2Library.RPC.Cbuf_AddText("xblive_rankedmatch 1");
                Thread.Sleep(1);
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 10 && Sub == "Lobby Menu")
            {
                Mw2Library.RPC.Cbuf_AddText("fast_restart 1");
            }
            else if (scrollNum == 11 && Sub == "Lobby Menu")
            {
                Nuke(client);
            }
            else if (scrollNum == 12 && Sub == "Lobby Menu")
            {
             

            }
            else if (scrollNum == 13 && Sub == "Lobby Menu")
            {
              
            
        
            }
            else if (scrollNum == 14 && Sub == "Lobby Menu")
            {
              
            }
            else if (scrollNum == 16 && Sub == "Main")
            {
                LoadSub(client, "Players");
            }
          
            else if (Sub == "Players")
            {
                LoadSub(client, "PlayerOpts");
                iPrintln(client, "Selected: " + GetNamesVer(CurPlayer[client]) + "\nStatus: " + Status[CurPlayer[client]] + "");
            }
            else if (scrollNum == 0 && Sub == "PlayerOpts")
            {
                 GivePlayerVerified(client);
            }
            else if (scrollNum == 1 && Sub == "PlayerOpts")
            {
                GivePlayerVIP(client);
            }
            else if (scrollNum == 2 && Sub == "PlayerOpts")
            {
                GivePlayerCOHost(client);
            }
            else if (scrollNum == 3 && Sub == "PlayerOpts")
            {
                iPrintlnBold(CurPlayer[client], "^2The Host Has ^1REMOVED ^2Your Access"); iPrintln(CurPlayer[client], "^2Press [{+melee}] To Open The Menu!"); Status[CurPlayer[client]] = "Un-Verified"; IsVerified[CurPlayer[client]] = false; SubMenu[CurPlayer[client]] = "Nigger"; iPrintln(client, "^2" + GetNamesVer(CurPlayer[client]) + " ^1REMOVED ^3Access");
            }
            else if (scrollNum == 4 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Challenges.Level70(client);
                iPrintlnBold(client,"Level : ^270");
            }
            else if (scrollNum == 5 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Challenges.Prestige._10(client);
                iPrintlnBold(client, "Prestige : ^210");
            }
            else if (scrollNum == 6 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Challenges.GiveUnlockAll(client);
                
            }
            else if (scrollNum == 7 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Kick(client);
            }
            else if (scrollNum == 8 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.KickWithWarning(client);
            }
            else if (scrollNum == 9 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Give.Lag(client);
        }
             else if (scrollNum == 10 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Challenges.GiveDerankAll(client);
            }
            else if (scrollNum == 11 && Sub == "PlayerOpts")
            {
                
                Mw2Library.Mods.Clients.KillAndScare(client);
            }
            else if (scrollNum == 12 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Kick(client);
            }
            else if (scrollNum == 13 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Teleport.ClientToSky(client);
            }
            else if (scrollNum == 14 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Teleport.ClientToSpace(client);
            }
            else if (scrollNum == 15 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Teleport.ClientUnderMap(client);
            }
            else if (scrollNum == 16 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Teleport.ClientToHost(client);
            }
            else if (scrollNum == 17 && Sub == "PlayerOpts")
            {
                Mw2Library.Mods.Clients.Teleport.EveryoneToClient(client);
            }
            else if (scrollNum == 18 && Sub == "PlayerOpts")
            {
                Mw2Library.RPC.FreezePS3(client,"");
            }
            else if (scrollNum == 19 && Sub == "PlayerOpts")
            {

                PS3.Extension.WriteString(Mw2Library.Offsets.Clients.NameInGame + ((uint)(client * 0x3980)),Mw2Library.RPC.KeyBoard(client, "Rename Player" , "Client Name....", 60));
              
            }
            else if (scrollNum == 17 && Sub == "Main")
            {
                LoadSub(client, "Extra Menu");
            }
            else if (scrollNum == 0 && Sub == "Extra Menu")
            {
                Mw2Library.Mods.Clients.MovementFlags.DisableJump(client);
            }
            else if (scrollNum == 1 && Sub == "Extra Menu")
            {
                Mw2Library.Mods.Clients.MovementFlags.DisableSprint(client);
            }
            else if (scrollNum == 3 && Sub == "Extra Menu")
            {
                Mw2Library.Mods.Clients.MovementFlags.Default(client);
            }
            else if (scrollNum == 4 && Sub == "Extra Menu")
            {
                Mw2Library.Mods.Clients.MovementFlags.AutoProne(client);
            }
            else if (scrollNum == 5 && Sub == "Extra Menu")
            {
                if (autoprone == false)
                {
                    Mw2Library.Mods.Clients.AutoKill.On(client);
                    autoprone = true;
                }
                else
                {

                    Mw2Library.Mods.Clients.AutoKill.Off(client);
                    autoprone = false;
                }
           
            }

             else if (scrollNum == 6 && Sub == "Extra Menu")
            {
                Mw2Library.Mods.Clients.KillAndScare(client);
            }
            else if (scrollNum == 18 && Sub == "Main")
            {
                LoadSub(client, "All Players Menu");
            }
            else if (scrollNum == 0 && Sub == "All Players Menu")
            {
                Mw2Library.Mods.Clients.Challenges.Level70(0);
                Mw2Library.Mods.Clients.Challenges.Level70(1);
                Mw2Library.Mods.Clients.Challenges.Level70(2);
                Mw2Library.Mods.Clients.Challenges.Level70(3);
                Mw2Library.Mods.Clients.Challenges.Level70(4);
                Mw2Library.Mods.Clients.Challenges.Level70(5);
                Mw2Library.Mods.Clients.Challenges.Level70(6);
                Mw2Library.Mods.Clients.Challenges.Level70(7);
                Mw2Library.Mods.Clients.Challenges.Level70(8);
                Mw2Library.Mods.Clients.Challenges.Level70(9);
                Mw2Library.Mods.Clients.Challenges.Level70(10);
                Mw2Library.Mods.Clients.Challenges.Level70(11);
                Mw2Library.Mods.Clients.Challenges.Level70(12);
                Mw2Library.Mods.Clients.Challenges.Level70(13);
                Mw2Library.Mods.Clients.Challenges.Level70(14);
                Mw2Library.Mods.Clients.Challenges.Level70(15);
                Mw2Library.Mods.Clients.Challenges.Level70(16);
                Mw2Library.Mods.Clients.Challenges.Level70(17);
            }
            else if (scrollNum == 1 && Sub == "All Players Menu")
            {
                Mw2Library.Mods.Clients.Challenges.GiveUnlockAll(-1);


            }
            else if (scrollNum == 2 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._1(-1);
            }
            else if (scrollNum == 3 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._2(-1);
            }
            else if (scrollNum == 4 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._3(-1);
            }
            else if (scrollNum == 5 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._4(-1);
            }
            else if (scrollNum == 4 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._5(-1);
            }
            else if (scrollNum == 5 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._6(-1);
            }
            else if (scrollNum == 6 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._7(-1);
            }
            else if (scrollNum == 7 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._8(-1);
            }
            else if (scrollNum == 8 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._9(-1);
            }
            else if (scrollNum == 9 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._10(-1);
            }
            else if (scrollNum == 10 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.Prestige._11(-1);
            }
            else if (scrollNum == 11 && Sub == "All Players Menu")
            {

                Mw2Library.Mods.Clients.Challenges.GiveDerankAll(-1);
            }
          
            else if (scrollNum == 19 && Sub == "Main")
            {
                
                MatrixText(680 + (client * 18), IsVerText(client), 155, 180, client, "^1Mx444 : Mod Menu \n  ", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 255, 255, 255, 60);
                MatrixText(681 + (client * 18), IsVerText(client), 155, 200, client, "^2 Obris , Choco , Shark , Taylor , JML , iMCSx : Menu Base \n  ", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 255, 255, 255, 60);
                MatrixText(668 + (client * 18), IsVerText(client), 155, 220, client, "^3 Mango_Knife : Offsets + Mw2Library\n^4 MegaMister : Offsets + Flat UI", 99, 1.6, 1, 26, 36, 999, 255, 255, 255, 255, 255, 255, 255, 60);
            }
            
          
        }
        
      
        private void OpenMenu(int client)
        {
            Vision(client, "oilrig_underwater");
            ChangeAlpha(120 + ((int)client * 18), 230);
            ChangeAlpha(121 + ((int)client * 18), 255);
            ChangeAlpha(122 + ((int)client * 18), 255);
            ChangeAlpha(123 + ((int)client * 18), 189);
            ChangeAlpha(124 + ((int)client * 18), 255);
            ChangeAlpha(126 + ((int)client * 18), 255);
            ChangeAlpha(127 + ((int)client * 18), 255);
            System.Threading.Thread.Sleep(10);
            Vision(client, "oilrig_underwater");

         
        }
        private void CloseMenu(int client)
        {
            Vision(client, "default");
            ChangeAlpha(120 + ((int)client * 18), 0);
            ChangeAlpha(121 + ((int)client * 18), 0);
            ChangeAlpha(122 + ((int)client * 18), 0);
            ChangeAlpha(123 + ((int)client * 18), 0);
            ChangeAlpha(124 + ((int)client * 18), 0);
            ChangeAlpha(126 + ((int)client * 18), 0);
            ChangeAlpha(127 + ((int)client * 18), 0);
            System.Threading.Thread.Sleep(10);
            Vision(client, "default");
            
        }
        #region Aimbot
        private static void StartAimbot(uint Client)
        {
            Mw2Library.RPC.Aimbot.DoAimbot((int)Client);
        }
        #endregion
        #region Clients

        #endregion
        #region Lobby
        bool Spawn;
        private void Nuke(int client)
        {
            if (Spawn == false)
            {
                Cbuf_AddText(client,"scr_nuketimer 9999999");
                iPrintln(client, "Unlimited Nuke  : ^2ON");
                Spawn = true;
            }
            else
            {

                Cbuf_AddText(client, "reset scr_nuketimer");
                iPrintln(client, "Unlimited Nuke : ^1OFF");
                Spawn = false;
            }
        }





        bool Kill;
        private void KillCam(int client)
        {
            if (Kill == false)
            {
                Mw2Library.RPC.Cbuf_AddText("scr_game_allowkillcaom 0");
                iPrintln(client, "No Kill Cam : ^2ON");
                Kill = true;
            }
            else
            {

                Mw2Library.RPC.Cbuf_AddText("scr_game_allowkillcaom 1");
                iPrintln(client, "No Kill Cam : ^1OFF");
                Kill = false;
            }
        }



        bool Knife;
        private void KnifeRange(int client)
        {
            if (Knife == false)
            {
                Mw2Library.Mods.Lobby.MeeleRange(9999M);
                iPrintln(client, "Knife Range : ^2ON");
                Knife = true;
            }
            else
            {

                Mw2Library.RPC.Cbuf_AddText("player_meleeRange 1");
                Mw2Library.RPC.Cbuf_AddText("player_meleeWidth 1");
                Mw2Library.RPC.Cbuf_AddText("player_meleeHeight 1");
                iPrintln(client, "Knife Range : ^1OFF");
                Knife = false;
            }
        }
        bool DoHeart;
        bool Knock;
        private void KnockBack(int client)
        {
            if (Knock == false)
            {
                Mw2Library.Mods.Lobby.KnockBack(9999M);
                iPrintln(client, "Knock Back : ^2ON");
                Knock = true;
            }
            else
            {

                Mw2Library.RPC.Cbuf_AddText("reset g_knockback");
                iPrintln(client, "Knock Back : ^1OFF");
                Knock = false;
            }
        }



        bool timerscale;
        bool timer;
        private void Timescale(int client)
        {
            if (timer == false)
            {
                Mw2Library.Mods.Lobby.TimeScale(30);
                iPrintln(client, "Timescale : ^2Slow");
                timer = true;
            }
            else 
            {

                Mw2Library.Mods.Lobby.TimeScale(200);
                iPrintln(client, "Timescale : ^2Super");
                timer = false;
            }
        }
            



        bool Gravity;
        private void LobbyGravity(int client)
        {
            if (Gravity == false)
            {
                Mw2Library.Mods.Lobby.GravityScale(1M);
                iPrintln(client, "Gravity : ^2ON");
                Gravity = true;
            }
            else
            {

                Mw2Library.Mods.Lobby.GravityScale(800M);
                iPrintln(client, "Gravity : ^1OFF");
                Gravity = false;
            }
        }



        bool WallLobby;
        private void FallDamage(int client)
        {
            if (WallLobby == false)
            {
                Mw2Library.Mods.Lobby.fallDamageMax(9000M);
                iPrintln(client, "No Fall Damage : ^2ON");
                WallLobby = true;
            }
            else
            {

                Mw2Library.RPC.Cbuf_AddText("reset bg_fallDamageMaxHeight");
                iPrintln(client, "No Fall Damage : ^1OFF");
                WallLobby = false;
            }
        }


        bool SuperSpeedTog;
        private void SuperSpeed(int client)
        {
            if (SuperSpeedTog == false)
            {
                Mw2Library.Mods.Lobby.SpeedScale(800M);
                iPrintln(client, "Super Speed : ^2ON");
                SuperSpeedTog = true;
            }
            else
            {

                Mw2Library.Mods.Lobby.SpeedScale(190M);
                iPrintln(client, "Super Speed : ^1OFF");
                SuperSpeedTog = false;
            }
        }



        bool SuperJumpTog;
        private void SuperJump(int client)
        {
            if (SuperJumpTog == false)
            {
                Mw2Library.Mods.Lobby.JumpHeight(900M);
                iPrintln(client, "Super Jump : ^2ON");
                SuperJumpTog = true;
            }
            else
            {

                Mw2Library.Mods.Lobby.JumpHeight(50M);
                iPrintln(client, "Super Jump : ^1OFF");
                SuperJumpTog = false;
            }
        }



        bool JumpTog;
        private void ProMod(int client)
        {
            if (JumpTog == false)
            {
                Mw2Library.RPC.SetClientDvars(-1, "cg_fov " + "999");
                iPrintln(client, "Lobby Pro Mod : ^2ON");
                JumpTog = true;
            }
            else
            {

                Mw2Library.RPC.SetClientDvars(-1, "cg_fov 65");
                iPrintln(client, "Lobby Pro Mod : ^1OFF");
                JumpTog = false;
            }
        }
        #endregion
        #region host
        bool FPSx;
        bool autoprone;
        private void FPS1(int client)
        {
            if (FPSx == false)
            {
                Cbuf_AddText(client , "cg_drawfps 1 ");
                iPrintln(client, "FPS : ^2ON");
                FPSx = true;
            }
            else
            {

                Cbuf_AddText(client, "cg_drawfps 0 ");
                iPrintln(client, "FPS : ^1OFF");
                FPSx = false;
            }
        }



        public void Addbot(int client)
        {
            MW2RPC.CallFunc(0x002189D8, client);
        }
        bool Antiquit;
        private void quit(int client)
        {
            if (Antiquit == false)
            {
                SV_GameSendServerCommand(client, "s g_scriptmainmenu \"class\"");
                iPrintln(client, "Anti Quit : ^2ON");
                Antiquit = true;
            }
            else
            {

                SV_GameSendServerCommand(client, "s g_scriptmainmenu \"by444xMoDz\"");
                iPrintln(client, "Anti Quit : ^1OFF");
                Antiquit = false;
            }
        }


        bool Anti;
        private void Join(int client)
        {
            if (Anti == false)
            {
                Cbuf_AddText(-1, "set g_password 444xMoDz");

                iPrintln(client, "Anti Join : ^2ON");
                Anti = true;
            }
            else
            {

                Cbuf_AddText(-1, "reset set g_password ");

                iPrintln(client, "Anti Join : ^1OFF");
                Anti = false;
            }
        }


       private String KeyBoard(String Title, String PresetText, Int32 MaxLength)
        {
            MW2RPC.CallFunc(0x238070, 0, Title, PresetText, MaxLength, 0x70B4D8);
            while (PS3.Extension.ReadInt32(0x203B4C8) != 0)
            { continue; }
            return PS3.Extension.ReadString(0x2510E22);
        }

        bool ForceTog;
        private void ForceHost(int client)
        {
             if (ForceTog == false)
            {
                SV_SendServerCommand(client, "v party_connectTimeout 1000");
                SV_SendServerCommand(client, "v party_connectTimeout 1");
                SV_SendServerCommand(client, "v party_host 1");
                SV_SendServerCommand(client, "v party_hostmigration 0");
                SV_SendServerCommand(client, "v onlinegame 1");
                SV_SendServerCommand(client, "v onlinegameandhost 1");
                SV_SendServerCommand(client, "v onlineunrankedgameandhost 0");
                SV_SendServerCommand(client, "v migration_msgtimeout 0");
                SV_SendServerCommand(client, "v migration_timeBetween 999999");
                SV_SendServerCommand(client, "v migration_verboseBroadcastTime 0");
                SV_SendServerCommand(client, "v migrationPingTime 0");
                SV_SendServerCommand(client, "v bandwidthtest_duration 0");
                SV_SendServerCommand(client, "v bandwidthtest_enable 0");
                SV_SendServerCommand(client, "v bandwidthtest_ingame_enable 0");
                SV_SendServerCommand(client, "v bandwidthtest_timeout 0");
                SV_SendServerCommand(client, "v cl_migrationTimeout 0");
                SV_SendServerCommand(client, "v lobby_partySearchWaitTime 0");
                SV_SendServerCommand(client, "v bandwidthtest_announceinterval 0");
                SV_SendServerCommand(client, "v partymigrate_broadcast_interval 99999");
                SV_SendServerCommand(client, "v partymigrate_pingtest_timeout 0");
                SV_SendServerCommand(client, "v partymigrate_timeout 0");
                SV_SendServerCommand(client, "v partymigrate_timeoutmax 0");
                SV_SendServerCommand(client, "v partymigrate_pingtest_retry 0");
                SV_SendServerCommand(client, "v partymigrate_pingtest_timeout 0");
                SV_SendServerCommand(client, "v g_kickHostIfIdle 0");
                SV_SendServerCommand(client, "v sv_cheats 1");
                SV_SendServerCommand(client, "v xblive_playEvenIfDown 1");
                SV_SendServerCommand(client, "v party_hostmigration 0");
                SV_SendServerCommand(client, "v badhost_endGameIfISuck 0");
                SV_SendServerCommand(client, "v badhost_maxDoISuckFrames 0");
                SV_SendServerCommand(client, "v badhost_maxHappyPingTime 99999");
                SV_SendServerCommand(client, "v badhost_minTotalClientsForHappyTest 99999");
                SV_SendServerCommand(client, "v bandwidthtest_enable 0");
            iPrintln(client, "Force Host : ^2ON");
                    ForceTog = true;
            }
            else
            {
                SV_SendServerCommand(client, "v reset party_connectTimeout 1000");
                SV_SendServerCommand(client, "v reset party_connectTimeout 1");
                SV_SendServerCommand(client, "v reset party_host 1");
                SV_SendServerCommand(client, "v reset party_hostmigration 0");
                SV_SendServerCommand(client, "v reset onlinegame 1");
                SV_SendServerCommand(client, "v reset onlinegameandhost 1");
                SV_SendServerCommand(client, "v reset onlineunrankedgameandhost 0");
                SV_SendServerCommand(client, "v reset migration_msgtimeout 0");
                SV_SendServerCommand(client, "v reset migration_timeBetween 999999");
                SV_SendServerCommand(client, "v reset migration_verboseBroadcastTime 0");
                SV_SendServerCommand(client, "v reset migrationPingTime 0");
                SV_SendServerCommand(client, "v reset bandwidthtest_duration 0");
                SV_SendServerCommand(client, "v reset bandwidthtest_enable 0");
                SV_SendServerCommand(client, "v reset bandwidthtest_ingame_enable 0");
                SV_SendServerCommand(client, "v reset bandwidthtest_timeout 0");
                SV_SendServerCommand(client, "v reset cl_migrationTimeout 0");
                SV_SendServerCommand(client, "v reset lobby_partySearchWaitTime 0");
                SV_SendServerCommand(client, "v reset bandwidthtest_announceinterval 0");
                SV_SendServerCommand(client, "v reset partymigrate_broadcast_interval 99999");
                SV_SendServerCommand(client, "v reset partymigrate_pingtest_timeout 0");
                SV_SendServerCommand(client, "v reset partymigrate_timeout 0");
                SV_SendServerCommand(client, "v reset partymigrate_timeoutmax 0");
                SV_SendServerCommand(client, "v reset partymigrate_pingtest_retry 0");
                SV_SendServerCommand(client, "v reset partymigrate_pingtest_timeout 0");
                SV_SendServerCommand(client, "v reset g_kickHostIfIdle 0");
                SV_SendServerCommand(client, "v reset sv_cheats 1");
                SV_SendServerCommand(client, "v reset xblive_playEvenIfDown 1");
                SV_SendServerCommand(client, "v reset party_hostmigration 0");
                SV_SendServerCommand(client, "v reset badhost_endGameIfISuck 0");
                SV_SendServerCommand(client, "v reset badhost_maxDoISuckFrames 0");
                SV_SendServerCommand(client, "v reset badhost_maxHappyPingTime 99999");
                SV_SendServerCommand(client, "v reset badhost_minTotalClientsForHappyTest 99999");
                SV_SendServerCommand(client, "v reset bandwidthtest_enable 0");
                iPrintln(client, "Force Host : ^1OFF");
                ForceTog = false;
            }
        }
        #endregion
        #region co-host

        bool FPS;
        private void ThirdPerson(int client)
        {
            if (FPS == false)
            {
                Cbuf_AddText(client, "camera_thirdPerson 1");

                iPrintln(client, "Third Person : ^2ON");
                FPS = true;
            }
            else
            {

                Cbuf_AddText(client, "camera_thirdPerson 0");

                iPrintln(client, "Third Person : ^1OFF");
                FPS = false;
            }
        }






        bool GodS;
        private void GodSp(int client)
        {
            if (GodS == false)
            {

                byte[] godmode = new byte[] { 0x08 };
                PS3.SetMemory((0x014e2212 + (uint)client * 0x3700), godmode);
                iPrintln(client, "God mode Spectator : ^2ON");
                GodS = true;
            }
            else
            {


                byte[] godmode = new byte[] { 0x10 };
                PS3.SetMemory((0x014e2212 + (uint)client * 0x3700), godmode);
                iPrintln(client, "God mode Spectator : ^1OFF");
                GodS = false;
            }
        }




        bool Ping;
        private void PingT(int client)
        {
            if (Ping == false)
            {
                Cbuf_AddText(client, " cg_scoreboardPingText 1");
  
                iPrintln(client, "Ping Text : ^2ON");
                Ping = true;
            }
            else
            {

                Cbuf_AddText(client, " cg_scoreboardPingText 0");
  
                iPrintln(client, "Ping Text : ^1OFF");
                Ping = false;
            }
        }



        bool DNight;
        private void Night(int client)
        {
            if (DNight == false)
            {
                byte[] godmode = new byte[] { 0xFF };
                PS3.SetMemory((0x14e2653 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Night Dpad : ^2ON");
                DNight = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x14e2653 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Night Dpad : ^1OFF");
                DNight = false;
            }
        }



        bool DADS;
        private void ADS(int client)
        {
            if (DADS == false)
            {
                byte[] godmode = new byte[] { 0x0, 0x20 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "Disable Weapon : ^2ON");
                DADS = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x0, 0x00 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "Disable Weapon : ^1OFF");
                DADS = false;
            }
        }





        bool DWeapon12;
        private void DWeapon11(int client)
        {
            if (DWeapon12 == false)
            {
                byte[] godmode = new byte[] { 0x00,0x80 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "Disable Weapon : ^2ON");
                DWeapon12 = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x0, 0x00 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "Disable Weapon : ^1OFF");
                DWeapon12 = false;
            }
        }





        bool DWeapon;
        private void DWeapon1(int client)
        {
            if (DWeapon == false)
            {
                byte[] godmode = new byte[] { 0x08 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "Disable Weapon Switch : ^2ON");
                DWeapon = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x0,0x00 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "Disable Weapon Switch : ^1OFF");
                DWeapon = false;
            }
        }






        bool Skate;
        private void Skatemodz(int client)
        {
            if (Skate == false)
            {
                byte[] godmode = new byte[] { 0x01 };
                PS3.SetMemory((0x014e220e + (uint)client * 0x3700), godmode);
                iPrintln(client, "Skate Modz : ^2ON");
                Skate = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x014e220e + (uint)client * 0x3700), godmode);
                iPrintln(client, "Skate Modz : ^1OFF");
                Skate = false;
            }
        }
        #endregion
        #region mods
        bool LeftTog;
        private void Left1(int client)
        {
            if (LeftTog == false)
            {
                Cbuf_AddText(client, "cg_gun_y 10 ");
                iPrintln(client, "Left Side Gun : ^2ON");
                LeftTog = true;
            }
            else
            {

                Cbuf_AddText(client, "cg_gun_y 0 ");
                iPrintln(client, "Left Side Gun : ^1OFF");
                LeftTog = false;
            }
        }
        private void DefaultWeapon(int ClientInt)
        {
            PS3.SetMemory((uint)(0x14e259a + ClientInt * 0x3700), new byte[] { 0x00, 0x01, 0x0F, 0xFF, 0xFF, 0xFF });

            byte[] numArray = new byte[] { 0x00, 0x01 };
            PS3.SetMemory((uint)(0x14e2422 + ClientInt * 0x3700), numArray);

            numArray = new byte[] { 0x00, 0x01 };
            PS3.SetMemory((uint)(0x14e24b6 + ClientInt * 0x3700), numArray);
            iPrintln(client, "Default Weapon : ^2Given");
        }

        bool Akimbo2Tog;
        private void Akimbo2st(int client)
        {
            if (Akimbo2Tog == false)
            {
                byte[] godmode = new byte[] { 0x01 };
                PS3.SetMemory((0x014e245d + (uint)client * 0x3700), godmode);
                iPrintln(client, "Akimbo : ^2ON");
                Akimbo2Tog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x014e245d + (uint)client * 0x3700), godmode);
                iPrintln(client, "Akimbo : ^1OFF");
                Akimbo2Tog = false;
            }
        }






        bool zaimTog;
        private void Aimbot180(int client)
        {
            if (zaimTog == false)
            {
                Cbuf_AddText(client, "set aim_target_sentient_radius 128");
                Cbuf_AddText(client, "set aim_slowdown_debug 1");
                Cbuf_AddText(client, "set aim_lockon_debug 0");
                Cbuf_AddText(client, "set aim_lockon_region_width 640");
                Cbuf_AddText(client, "set aim_lockon_region_height 480");
                Cbuf_AddText(client, "set aim_lockon_enabled 1");
                Cbuf_AddText(client, "set aim_lockon_strength 1");
                Cbuf_AddText(client, "set aim_lockon_deflection 0");
                Cbuf_AddText(client, "set aim_autoaim_enabled 1");
                Cbuf_AddText(client, "set aim_autoaim_region_height 480");
                Cbuf_AddText(client, "set aim_autoaim_region_width 640");
                Cbuf_AddText(client, "set aim_slowdown_yaw_scale_ads 0");
                Cbuf_AddText(client, "set aim_slowdown_yaw_scale 0");
                Cbuf_AddText(client, "set aim_slowdown_pitch_scale 0");
                Cbuf_AddText(client, "set aim_slowdown_pitch_scale_ads 0");
                Cbuf_AddText(client, "set aim_slowdown_enabled 1");
                Cbuf_AddText(client, "set aim_slowdown_region_height 0");
                Cbuf_AddText(client, "set aim_slowdown_region_width 0");
                Cbuf_AddText(client, "set aim_slowdown_enabled 1");
                Cbuf_AddText(client, "set aim_aimAssistRangeScale 2");
                Cbuf_AddText(client, "set aim_autoAimRangeScale 2");
                iPrintln(client, "Aimbot 180 : ^2ON");
                zaimTog = true;
            }
            else
            {

                Cbuf_AddText(client, "reset aim_target_sentient_radius 128");
                Cbuf_AddText(client, "reset aim_slowdown_debug 1");
                Cbuf_AddText(client, "reset aim_lockon_debug 0");
                Cbuf_AddText(client, "reset aim_lockon_region_width 640");
                Cbuf_AddText(client, "reset aim_lockon_region_height 480");
                Cbuf_AddText(client, "reset aim_lockon_enabled 1");
                Cbuf_AddText(client, "reset aim_lockon_strength 1");
                Cbuf_AddText(client, "reset aim_lockon_deflection 0");
                Cbuf_AddText(client, "reset aim_autoaim_enabled 1");
                Cbuf_AddText(client, "reset aim_autoaim_region_height 480");
                Cbuf_AddText(client, "reset aim_autoaim_region_width 640");
                Cbuf_AddText(client, "reset aim_slowdown_yaw_scale_ads 0");
                Cbuf_AddText(client, "reset aim_slowdown_yaw_scale 0");
                Cbuf_AddText(client, "reset aim_slowdown_pitch_scale 0");
                Cbuf_AddText(client, "reset aim_slowdown_pitch_scale_ads 0");
                Cbuf_AddText(client, "reset aim_slowdown_enabled 1");
                Cbuf_AddText(client, "reset aim_slowdown_region_height 0");
                Cbuf_AddText(client, "reset aim_slowdown_region_width 0");
                Cbuf_AddText(client, "reset aim_slowdown_enabled 1");
                Cbuf_AddText(client, "reset aim_aimAssistRangeScale 2");
                Cbuf_AddText(client, "reset aim_autoAimRangeScale 2");
                iPrintln(client, "Aimbot 180 : ^1OFF");
                zaimTog = false;
            }
        }










        bool NaimTog;
        private void NormalAimbot(int client)
        {
            if (NaimTog == false)
            {
                Cbuf_AddText(client, "set SingleAimBot");
                Cbuf_AddText(client, "set aim_autoaim_lerp 100");
                Cbuf_AddText(client, "set aim_autoaim_region_height 480");
                Cbuf_AddText(client, "set aim_autoaim_region_width 640");
                Cbuf_AddText(client, "set aim_aimAssistRangeScale 2");
                Cbuf_AddText(client, "set aim_autoAimRangeScale 2");
                Cbuf_AddText(client, "set aim_slowdown_debug 0");
                Cbuf_AddText(client, "set aim_slowdown_region_height 0");
                Cbuf_AddText(client, "set aim_slowdown_region_width 0");
                Cbuf_AddText(client, "set aim_lockon_enabled 1");
                Cbuf_AddText(client, "set aim_lockon_strength 1");
                Cbuf_AddText(client, "set aim_lockon_deflection 0");
                Cbuf_AddText(client, "set aim_autoaim_enabled 0");
                Cbuf_AddText(client, "set aim_slowdown_yaw_scale_ads 0");
                Cbuf_AddText(client, "set aim_slowdown_pitch_scale_ads 0");
                Cbuf_AddText(client, "set aim_slowdown_enabled 1");
                Cbuf_AddText(client, "set aim_autoaim_enabled 1");
                Cbuf_AddText(client, "set aim_lockon_enabled 1");
                iPrintln(client, "Normal Aimbot : ^2ON");
                NaimTog = true;
            }
            else
            {

                Cbuf_AddText(client, "reset aim_autoaim_lerp");
                Cbuf_AddText(client, "reset aim_autoaim_region_height");
                Cbuf_AddText(client, "reset aim_autoaim_region_width");
                Cbuf_AddText(client, "reset aim_aimAssistRangeScale");
                Cbuf_AddText(client, "reset aim_autoAimRangeScale");
                Cbuf_AddText(client, "reset aim_slowdown_debug");
                Cbuf_AddText(client, "reset aim_slowdown_region_height");
                Cbuf_AddText(client, "reset aim_slowdown_region_width");
                Cbuf_AddText(client, "reset aim_lockon_enabled");
                Cbuf_AddText(client, "reset aim_lockon_strength");
                Cbuf_AddText(client, "reset aim_lockon_deflection");
                Cbuf_AddText(client, "reset aim_autoaim_enabled");
                Cbuf_AddText(client, "reset aim_slowdown_yaw_scale_ads");
                Cbuf_AddText(client, "reset aim_slowdown_pitch_scale_ads");
                Cbuf_AddText(client, "reset aim_slowdown_enabled");
                Cbuf_AddText(client, "reset aim_autoaim_enabled");
                Cbuf_AddText(client, "reset aim_lockon_enabled");
                Cbuf_AddText(client, "aim_autoaim_enabled 0");
                iPrintln(client, "Normal Aimbot : ^1OFF");
                NaimTog = false;
            }
        }

        bool MaimTog;
        private void MediumAimbot(int client)
        {
            if (MaimTog == false)
            {
                SV_GameSendServerCommand(client, "v set aim_slowdown_pitch_scale 0.4");
                SV_GameSendServerCommand(client, "v set aim_slowdown_yaw_scale 0.4");
                SV_GameSendServerCommand(client, "v set aim_slowdown_yaw_scale_ads 0.5");
                SV_GameSendServerCommand(client, "v set aim_slowdown_pitch_scale_ads 0.5");
                SV_GameSendServerCommand(client, "v set aim_slowdown_debug 1");
                SV_GameSendServerCommand(client, "v set aim_slowdown_region_height 2.85");
                SV_GameSendServerCommand(client, "v set aim_slowdown_region_width 2.85");
                SV_GameSendServerCommand(client, "v set aim_aimAssistRangeScale 1");
                SV_GameSendServerCommand(client, "v set aim_autoaim_enabled 1");
                SV_GameSendServerCommand(client, "v set aim_lockon_region_height 90");
                SV_GameSendServerCommand(client, "v set aim_lockon_region_width 90");
                SV_GameSendServerCommand(client, "v set aim_lockon_enabled 1");
                SV_GameSendServerCommand(client, "v set aim_lockon_strength 9999");
                SV_GameSendServerCommand(client, "v set aim_lockon_deflection 0.0005");
                SV_GameSendServerCommand(client, "v set seta drawLagometer 1");
                SV_GameSendServerCommand(client, "v set aim_lockon_debug 0");
                SV_GameSendServerCommand(client, "v set aim_autoaim_lerp 999");
                SV_GameSendServerCommand(client, "v set aim_autoaim_region_height 999");
                SV_GameSendServerCommand(client, "v set aim_autoaim_region_width 5000");
                SV_GameSendServerCommand(client, "v set aim_autoAimRangeScale 2");
                SV_GameSendServerCommand(client, "v set aim_input_graph_debug 0");
                SV_GameSendServerCommand(client, "v set aim_input_graph_enabled 1");
                iPrintln(client, "Medium Aimbot : ^2ON");
                MaimTog = true;
            }
            else
            {

                SV_GameSendServerCommand(client, "v reset aim_slowdown_pitch_scale 0.4");
                SV_GameSendServerCommand(client, "v reset aim_slowdown_yaw_scale 0.4");
                SV_GameSendServerCommand(client, "v reset aim_slowdown_yaw_scale_ads 0.5");
                SV_GameSendServerCommand(client, "v reset aim_slowdown_pitch_scale_ads 0.5");
                SV_GameSendServerCommand(client, "v reset aim_slowdown_debug 1");
                SV_GameSendServerCommand(client, "v reset aim_slowdown_region_height 2.85");
                SV_GameSendServerCommand(client, "v reset aim_slowdown_region_width 2.85");
                SV_GameSendServerCommand(client, "v reset aim_aimAssistRangeScale 1");
                SV_GameSendServerCommand(client, "v reset aim_autoaim_enabled 1");
                SV_GameSendServerCommand(client, "v reset aim_lockon_region_height 90");
                SV_GameSendServerCommand(client, "v reset aim_lockon_region_width 90");
                SV_GameSendServerCommand(client, "v reset aim_lockon_enabled 1");
                SV_GameSendServerCommand(client, "v reset aim_lockon_strength 9999");
                SV_GameSendServerCommand(client, "v reset aim_lockon_deflection 0.0005");
                SV_GameSendServerCommand(client, "v reset reseta drawLagometer 1");
                SV_GameSendServerCommand(client, "v reset aim_lockon_debug 0");
                SV_GameSendServerCommand(client, "v reset aim_autoaim_lerp 999");
                SV_GameSendServerCommand(client, "v reset aim_autoaim_region_height 999");
                SV_GameSendServerCommand(client, "v reset aim_autoaim_region_width 5000");
                SV_GameSendServerCommand(client, "v reset aim_autoAimRangeScale 2");
                SV_GameSendServerCommand(client, "v reset aim_input_graph_debug 0");
                SV_GameSendServerCommand(client, "v reset aim_input_graph_enabled 1");
                iPrintln(client, "Medium Aimbot : ^1OFF");
                MaimTog = false;
            }
        }

        bool WallTog;
        private void Wallhack(int client)
        {
            if (WallTog == false)
            {
                Cbuf_AddText(client, "toggle r_znear 55 5");
                iPrintln(client, "Wallhack : ^2ON");
                WallTog = true;
            }
            else
            {

                Cbuf_AddText(client, "toggle r_znear 0 0");
                iPrintln(client, "Wallhack : ^1OFF");
                WallTog = false;
            }
        }

        bool LaserTog;
        private void Laser(int client)
        {
            if (LaserTog == false)
            {
                Cbuf_AddText(client, "toggle laserforceon 0 1");
                iPrintln(client, "Laser : ^2ON");
                LaserTog = true;
            }
            else
            {

                Cbuf_AddText(client, "toggle laserforceon 1 0");
                iPrintln(client, "Laser : ^1OFF");
                LaserTog = false;
            }
        }

        bool AkimboTog;
        private void Akimbo1st(int client)
        {
            if (AkimboTog == false)
            {
                byte[] godmode = new byte[] { 0x01 };
                PS3.SetMemory((21898343 + (uint)client * 0x3700), godmode); 
                iPrintln(client, "Akimbo : ^2ON");
                AkimboTog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((21898343 + (uint)client * 0x3700), godmode); 
                iPrintln(client, "Akimbo : ^1OFF");
                AkimboTog = false;
            }
        }


        bool SpeedTog;
        private void Speedx2(int client)
        {
            if (SpeedTog == false)
            {

                byte[] godmode = new byte[] { 0x40 };
                PS3.SetMemory((0x014e543c + (uint)client * 0x3700), godmode);
                iPrintln(client, "Speed 2x : ^2ON");
                SpeedTog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x3F };
                PS3.SetMemory((0x014e543c + (uint)client * 0x3700), godmode);
                iPrintln(client, "Speed 2x : ^1OFF");
                SpeedTog = false;
            }
        }
        bool Speed6Tog;
        private void Speedx6(int client)
        {
            if (Speed6Tog == false)
            {

                byte[] godmode = new byte[] { 0x42 };
                PS3.SetMemory((0x014e543c + (uint)client * 0x3700), godmode);
                iPrintln(client, "Speed 6x : ^2ON");
                Speed6Tog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x3F };
                PS3.SetMemory((0x014e543c + (uint)client * 0x3700), godmode);
                iPrintln(client, "Speed 6x : ^1OFF");
                Speed6Tog = false;
            }
        }

        bool Speed4Tog;
        private void Speedx4(int client)
        {
            if (Speed4Tog == false)
            {

                byte[] godmode = new byte[] { 0x41};
                PS3.SetMemory((0x014e543c + (uint)client * 0x3700), godmode);
                iPrintln(client, "Speed 4x : ^2ON");
                Speed4Tog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x3F };
                PS3.SetMemory((0x014e543c + (uint)client * 0x3700), godmode);
                iPrintln(client, "Speed 4x : ^1OFF");
                Speed4Tog = false;
            }
        }





        bool CrossTog;
        private void Cross1(int client)
        {
            if (CrossTog == false)
            {

                byte[] godmode = new byte[] { 0x02 };
                PS3.SetMemory((0x014e24d3 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Crosshair : ^2ON");
                CrossTog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x014e24d3 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Crosshair : ^1OFF");
                CrossTog = false;
            }
        }



        bool NorecTog;
        private void NoRecoil1(int client)
        {
            if (NorecTog == false)
            {

                byte[] godmode = new byte[] { 0x04 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "No Recoil : ^2ON");
                NorecTog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x00,0x00 };
                PS3.SetMemory((0x014e24be + (uint)client * 0x3700), godmode);
                iPrintln(client, "No Recoil : ^1OFF");
                NorecTog = false;
            }
        }

        private void Suicide1(int client)
        {

            byte[] godmode = new byte[] { 0xC5 };
            PS3.SetMemory((0x014e2220 + (uint)client * 0x3700), godmode);
           
        }


        bool TeamTog;
        private void ChangeTeam1(int client)
        {
            if (TeamTog == false)
            {

                byte[] godmode = new byte[] { 0x01 };
                PS3.SetMemory((0x014e5453 + (uint)client * 0x3700), godmode);
                iPrintln(client, "^1Enemy");
                TeamTog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x02 };
                PS3.SetMemory((0x014e5453 + (uint)client * 0x3700), godmode);
                iPrintln(client, "^2Friendly");
                TeamTog = false;
            }
        }



        bool FOVTog;
        private void FOV(int client)
        {
            if (FOVTog == false)
            {

                SV_GameSendServerCommand(client, "v cg_fov \"80\"");
                iPrintln(client, "FOV : ^280");
                FOVTog = true;
            }
            else
            {

                SV_GameSendServerCommand(client, "v cg_fov \"65\"");
                iPrintln(client, "FOV : ^2Default");
                FOVTog = false;
            }
        }

     
        private void AllPerks1(int client)
        {


            byte[] godmode = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e262a + (uint)client * 0x3700), godmode);
            iPrintln(client, "All Perks : ^2GIVEN");
            
        }
        bool Exp1Tog;
        private void Explosive1(int client)
        {
            if (Exp1Tog == false)
            {

                byte[] godmode = new byte[] { 0xFF, 0xFF };
                PS3.SetMemory((0x014e2628 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Explosive Bullets : ^2ON");
                Exp1Tog = true;
            }
            else
            {

                byte[] godmode = new byte[] { 0x00, 0x08 };
                PS3.SetMemory((0x014e2628 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Explosive Bullets : ^1OFF");
                Exp1Tog = false;
            }
        }



        bool FreezeTog;
        private void Freeze1(int client)
        {
            if (FreezeTog == false)
            {

                byte[] godmode = new byte[] { 0x06 };
                PS3.SetMemory((0x014E5623 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Freeze : ^2ON");
                FreezeTog = true;
            }
            else
            {
                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x014E5623 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Freeze : ^1OFF");
                FreezeTog = false;
            }
        }


        bool UAVTog;
        private void UAV1(int client)
        {
            if (UAVTog == false)
            {

                byte[] godmode = new byte[] { 0x01 };
                PS3.SetMemory((0x14E54E6 + (uint)client * 0x3700), godmode);
                iPrintln(client, "UAV : ^2ON");
                UAVTog = true;
            }
            else
            {
                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x14E54E6 + (uint)client * 0x3700), godmode);
                iPrintln(client, "UAV : ^1OFF");
                UAVTog = false;
            }
        }





        bool RedTog;
        private void RedBoxes1(int client)
        {
            if (RedTog == false)
            {

                byte[] godmode = new byte[] { 0x55 };
                PS3.SetMemory((0x014e2213 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Red Boxes : ^2ON");
                RedTog = true;
            }
            else
            {
                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x014e2213 + (uint)client * 0x3700), godmode);
                iPrintln(client, "Red Boxes : ^1OFF");
                RedTog = false;
            }
        }
        private void InfiniteAmmo1(int client)
        {
            byte[] godmode = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e256c + (uint)client * 0x3700), godmode);
            byte[] godmode1 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e24ec + (uint)client * 0x3700), godmode1);
            byte[] godmode11 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e2570 + (uint)client * 0x3700), godmode11);
            byte[] godmode121 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e2554 + (uint)client * 0x3700), godmode121);
            byte[] godmode1211 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e24dc + (uint)client * 0x3700), godmode1211);
            byte[] godmode12113 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e2558 + (uint)client * 0x3700), godmode12113);
            byte[] godmode121123 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e2578 + (uint)client * 0x3700), godmode121123);
            byte[] godmode121131 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e24f4 + (uint)client * 0x3700), godmode121131);
            byte[] godmode12112311 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e2560 + (uint)client * 0x3700), godmode12112311);
            byte[] godmode121123112 = new byte[] { 0x0F, 0xFF, 0xFF, 0xFF };
            PS3.SetMemory((0x014e2578 + (uint)client * 0x3700), godmode121123112);
            iPrintln(client, "Infinite Ammo : ^2GIVEN");   
        }
        bool UFOTog;
        private void BindNoClip1(int client)
        {
            if (UFOTog == false)
            {

                byte[] godmode = new byte[] { 0x02 };
                PS3.SetMemory((0x014E5623 + (uint)client * 0x3700), godmode);
                iPrintln(client, "UFO mode : ^2ON");
                UFOTog = true;
            }
            else
            {
                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x014E5623 + (uint)client * 0x3700), godmode);
                iPrintln(client, "UFO mode : ^1OFF");
                UFOTog = false;
            }
        }

        bool NoclipTog;
        private void NoClip1(int client)
        {
            if (NoclipTog == false)
            {
                byte[] godmode = new byte[] { 0x01 };
                PS3.SetMemory((0x014E5623 + (uint)client * 0x3700), godmode);  
                iPrintln(client, "No Clip : ^2ON");
                NoclipTog = true;
            }
            else
            {
                byte[] godmode = new byte[] { 0x00 };
                PS3.SetMemory((0x014E5623 + (uint)client * 0x3700), godmode); 
                iPrintln(client, "No Clip: ^1OFF");
                NoclipTog = false;
            }
        }
        bool GodmodeTog;
        private void GodmodeTog1(int client)
        {
            if (GodmodeTog == false)
            {
                byte[] godmode = new byte[] { 0xFF, 0xFF };
                PS3.SetMemory((0x014e5429 + (uint)client * 0x3700), godmode);
                iPrintln(client, "God mode: ^2ON");
                GodmodeTog = true;
            }
            else
            {
                byte[] godmode = new byte[] { 0x00, 0x64 };
                PS3.SetMemory((0x014e5429 + (uint)client * 0x3700), godmode);
                iPrintln(client, "God mode : ^1OFF");
                GodmodeTog = false;
            }
        }
        private void Example1(int client, int scrollNum)
        {
            iPrintln(client, "Sub Menu 1: ^6Option " + scrollNum + "");
        }

        #endregion
        #region some crap
        public static void SetModel(uint client, byte[] Bytes)
        {
            PS3.SetMemory(0x1319968 + (client * 0x3700), Bytes);
        }

        public static void Vision(int client, string message)
        {
            SV_SendServerCommand(client, string.Concat("Q \"", message));
        }
        public static void SV_SendServerCommand(int clientIndex, string Command)
        {
            WritePowerPc(true);
            PS3.Extension.WriteString(0x10040010, Command);
            PS3.Extension.WriteInt32(0x10040004, clientIndex);
            PS3.Extension.WriteBool(0x10040003, true);
            bool isRunning;
            do { isRunning = DEX.Extension.ReadBool(0x10040003); } while (isRunning != false);
            WritePowerPc(false);
        }
        public static void WritePowerPc(bool Active)
        {
            byte[] NewPPC = new byte[] { 0xF8, 0x21, 0xFF, 0x61, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x01, 0x00, 0xB0, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x00, 0x60, 0x62, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x28, 0x3C, 0x60, 0x10, 0x04, 0x80, 0x63, 0x00, 0x04, 0x3C, 0xA0, 0x10, 0x04, 0x38, 0x80, 0x00, 0x00, 0x30, 0xA5, 0x00, 0x10, 0x4B, 0xE8, 0xB2, 0x7D, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x04, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x05, 0x80, 0x63, 0x00, 0x00, 0x2C, 0x03, 0x00, 0x00, 0x41, 0x82, 0x00, 0x24, 0x3C, 0x60, 0x10, 0x05, 0x30, 0x63, 0x00, 0x10, 0x4B, 0xE2, 0xF9, 0x7D, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x04, 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x10, 0x05, 0x90, 0x64, 0x00, 0x00, 0x3C, 0x60, 0x10, 0x03, 0x80, 0x63, 0x00, 0x04, 0x60, 0x62, 0x00, 0x00, 0xE8, 0x01, 0x00, 0xB0, 0x7C, 0x08, 0x03, 0xA6, 0x38, 0x21, 0x00, 0xA0, 0x4E, 0x80, 0x00, 0x20 };
            byte[] RestorePPC = new byte[] { 0x81, 0x62, 0x92, 0x84, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x21, 0xFF, 0x01, 0xFB, 0xE1, 0x00, 0xB8, 0xDB, 0x01, 0x00, 0xC0, 0x7C, 0x7F, 0x1B, 0x78, 0xDB, 0x21, 0x00, 0xC8, 0xDB, 0x41, 0x00, 0xD0, 0xDB, 0x61, 0x00, 0xD8, 0xDB, 0x81, 0x00, 0xE0, 0xDB, 0xA1, 0x00, 0xE8, 0xDB, 0xC1, 0x00, 0xF0, 0xDB, 0xE1, 0x00, 0xF8, 0xFB, 0x61, 0x00, 0x98, 0xFB, 0x81, 0x00, 0xA0, 0xFB, 0xA1, 0x00, 0xA8, 0xFB, 0xC1, 0x00, 0xB0, 0xF8, 0x01, 0x01, 0x10, 0x81, 0x2B, 0x00, 0x00, 0x88, 0x09, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00, 0x40, 0x9E, 0x00, 0x64, 0x7C, 0x69, 0x1B, 0x78, 0xC0, 0x02, 0x92, 0x94, 0xC1, 0xA2, 0x92, 0x88, 0xD4, 0x09, 0x02, 0x40, 0xD0, 0x09, 0x00, 0x0C, 0xD1, 0xA9, 0x00, 0x04, 0xD0, 0x09, 0x00, 0x08, 0xE8, 0x01, 0x01, 0x10, 0xEB, 0x61, 0x00, 0x98, 0xEB, 0x81, 0x00, 0xA0, 0x7C, 0x08, 0x03, 0xA6, 0xEB, 0xA1, 0x00, 0xA8, 0xEB, 0xC1, 0x00, 0xB0, 0xEB, 0xE1, 0x00, 0xB8, 0xCB, 0x01, 0x00, 0xC0, 0xCB, 0x21, 0x00, 0xC8 };
            if (Active == true)
                PS3.SetMemory(0x0038EDE8, NewPPC);
            else
                PS3.SetMemory(0x0038EDE8, RestorePPC);
        }
        #endregion

        //Verification Shitz

        public string GetNamesVer(int client){NamesVer = method_4Ver(method_5((uint)client));return NamesVer;}
        public string GetNames(int client){Names = "";for (int i = 0; i < 18; i++){if (method_4Ver(method_5((uint)i)) == string.Empty){Names += "Player N/A\n";}else{Names += "" + method_4Ver(method_5((uint)i)) + "\n";}}return Names;}
        private string getVerColor(string status) { if (status == "Host")                return "^2"; else if (status == "Co-Host")                return "^5"; else if (status == "Verified")                return "^3"; else                return "^1"; }
        public string GetClientName(int client) { Names = ""; for (int i = 0; i < 18; i++) { if (method_4Ver(method_5((uint)i)) == string.Empty) { Names += "" + method_4Ver(method_5((uint)i)) + ""; } } return Names; }
        private string method_4Ver(string string_5) { StringBuilder builder = new StringBuilder(); for (int i = 0; i <= (string_5.Length - 2); i += 2) { builder.Append(Convert.ToString(Convert.ToChar(int.Parse(string_5.Substring(i, 2), NumberStyles.HexNumber)))); } return builder.ToString(); }
        private string method_4(int selfNum, string string_5, int client) { StringBuilder builder = new StringBuilder(); for (int i = 0; i <= (string_5.Length - 2); i += 2) { builder.Append(Convert.ToString(Convert.ToChar(int.Parse(string_5.Substring(i, 2), NumberStyles.HexNumber)))); } string str = string.Empty; if (builder.ToString() == str) { builder.Remove(builder.Length, 0); return builder.ToString(); } return " "; }
        public static uint smethod_0(uint uint_8){return (0x14e2200 + (uint_8 * 0x3700));}
        public string method_5(uint uint_8){byte[] buffer = new byte[0x20];Ps3Memory.GetMemory(smethod_0(uint_8) + 0x3290, buffer);return BitConverter.ToString(buffer).Replace("00", string.Empty).Replace("-", string.Empty).Replace("^0", string.Empty).Replace("^1", string.Empty).Replace("^2", string.Empty).Replace("^3", string.Empty).Replace("^4", string.Empty).Replace("^5", string.Empty).Replace("^6", string.Empty).Replace("^7", string.Empty).Replace("^8", string.Empty).Replace("^9", string.Empty) + "\0";}
        public static string ClientNames(uint client){string getnames = PS3.Extension.ReadString(0x014e5408 + client * 0x3700);return getnames;}
        public void GetClients(){if (dataGridView1.RowCount == 1){dataGridView1.Rows.Add(17);}for (uint i = 0; i < 18; i++){dataGridView1[0, Convert.ToInt32(i)].Value = i;dataGridView1[1, Convert.ToInt32(i)].Value = ClientNames(i);}}
        private void RefreshGrid_Tick(object sender, EventArgs e){GetClients();}
        private int IsVerShader(int client){if (Status[client] != "Verified" || Status[client] != "Co-Host" || Status[client] != "Host")return 6;else return 0;}
        private int IsVerText(int client){if (Status[client] != "Verified" || Status[client] != "Co-Host" || Status[client] != "Host")return 1;else return 0;}

        //Functions, Huds And Shit
        public static void SV_GameSendServerCommand(int int_0, string string_0) { if (int_0 == 0x7ff) { int_0 = -1; } RPC(true); byte[] buffer = new byte[4]; byte[] bytes = Encoding.ASCII.GetBytes(string_0); Array.Resize<byte>(ref bytes, bytes.Length + 1); byte[] array = BitConverter.GetBytes(int_0); Array.Reverse(array); Ps3Memory.SetMemory(0x10040010, bytes); Ps3Memory.SetMemory(0x10040004, array); Ps3Memory.SetMemory(0x10040000, new byte[] { 0x00, 0x00, 0x00, 0x01 }); do { Ps3Memory.GetMemory(0x10040000, buffer); } while (buffer[3] != 0);            RPC(false); }
        public static void RPC(bool ENABLE) { byte[] buffer = new byte[] { 0xf8, 0x21, 0xff, 0x61, 0x7c, 8, 2, 0xa6, 0xf8, 1, 0, 0xb0, 60, 0x60, 0x10, 3, 0x80, 0x63, 0, 0, 0x60, 0x62, 0, 0, 60, 0x60, 0x10, 4, 0x80, 0x63, 0, 0, 0x2c, 3, 0, 0, 0x41, 130, 0, 40, 60, 0x60, 0x10, 4, 0x80, 0x63, 0, 4, 60, 160, 0x10, 4, 0x38, 0x80, 0, 0, 0x30, 0xa5, 0, 0x10, 0x4b, 0xe8, 0xb2, 0x7d, 0x38, 0x60, 0, 0, 60, 0x80, 0x10, 4, 0x90, 100, 0, 0, 60, 0x60, 0x10, 5, 0x80, 0x63, 0, 0, 0x2c, 3, 0, 0, 0x41, 130, 0, 0x24, 60, 0x60, 0x10, 5, 0x30, 0x63, 0, 0x10, 0x4b, 0xe2, 0xf9, 0x7d, 60, 0x80, 0x10, 5, 0x90, 100, 0, 4, 0x38, 0x60, 0, 0, 60, 0x80, 0x10, 5, 0x90, 100, 0, 0, 60, 0x60, 0x10, 3, 0x80, 0x63, 0, 4, 0x60, 0x62, 0, 0, 0xe8, 1, 0, 0xb0, 0x7c, 8, 3, 0xa6, 0x38, 0x21, 0, 160, 0x4e, 0x80, 0, 0x20 }; if (ENABLE) { Ps3Memory.SetMemory(0x38ede8, buffer); } else { byte[] RestoreRPC = new byte[] { 0x81, 0x62, 0x92, 0x84, 0x7C, 0x08, 0x02, 0xA6, 0xF8, 0x21, 0xFF, 0x01, 0xFB, 0xE1, 0x00, 0xB8, 0xDB, 0x01, 0x00, 0xC0, 0x7C, 0x7F, 0x1B, 0x78, 0xDB, 0x21, 0x00, 0xC8, 0xDB, 0x41, 0x00, 0xD0, 0xDB, 0x61, 0x00, 0xD8, 0xDB, 0x81, 0x00, 0xE0, 0xDB, 0xA1, 0x00, 0xE8, 0xDB, 0xC1, 0x00, 0xF0, 0xDB, 0xE1, 0x00, 0xF8, 0xFB, 0x61, 0x00, 0x98, 0xFB, 0x81, 0x00, 0xA0, 0xFB, 0xA1, 0x00, 0xA8, 0xFB, 0xC1, 0x00, 0xB0, 0xF8, 0x01, 0x01, 0x10, 0x81, 0x2B, 0x00, 0x00, 0x88, 0x09, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00, 0x40, 0x9E, 0x00, 0x64, 0x7C, 0x69, 0x1B, 0x78, 0xC0, 0x02, 0x92, 0x94, 0xC1, 0xA2, 0x92, 0x88, 0xD4, 0x09, 0x02, 0x40, 0xD0, 0x09, 0x00, 0x0C, 0xD1, 0xA9, 0x00, 0x04, 0xD0, 0x09, 0x00, 0x08, 0xE8, 0x01, 0x01, 0x10, 0xEB, 0x61, 0x00, 0x98, 0xEB, 0x81, 0x00, 0xA0, 0x7C, 0x08, 0x03, 0xA6, 0xEB, 0xA1, 0x00, 0xA8, 0xEB, 0xC1, 0x00, 0xB0, 0xEB, 0xE1, 0x00, 0xB8, 0xCB, 0x01, 0x00, 0xC0, 0xCB, 0x21, 0x00, 0xC8, 0xCB, 0x41, 0x00, 0xD0, 0xCB, 0x61, 0x00, 0xD8, 0xCB, 0x81, 0x00, 0xE0, 0xCB, 0xA1, 0x00, 0xE8, 0xCB, 0xC1, 0x00, 0xF0, 0xCB, 0xE1, 0x00, 0xF8, 0x38, 0x21, 0x01, 0x00, 0x4E, 0x80, 0x00, 0x20, 0x81, 0x23, 0x05, 0x38, 0x3B, 0x81, 0x00, 0x70, 0x3B, 0x61, 0x00, 0x80, 0x38, 0x69, 0x15, 0x48, 0x7F, 0x84, 0xE3, 0x78, 0x78, 0x63, 0x00, 0x20, 0x7F, 0x65, 0xDB, 0x78, 0x3B, 0xA9, 0x15, 0x40, 0x4B, 0xFF, 0xFD, 0x51, 0x7F, 0xE9, 0xFB, 0x78, 0xC0, 0x01, 0x00, 0x70, 0x7B, 0xBE, 0x00, 0x20, 0xC3, 0x22, 0x92, 0x88, 0xD4, 0x09, 0x02, 0x50, 0xC0, 0x01, 0x00, 0x74, 0xD0, 0x09, 0x00, 0x04, 0xC1, 0xA1, 0x00, 0x78, 0xD1, 0xA9, 0x00, 0x08, 0xC0, 0x01, 0x00, 0x7C, 0xD0, 0x09, 0x00, 0x0C, 0x7F, 0xE9, 0xFB, 0x78, 0xC1, 0xA1, 0x00, 0x80, 0xD5, 0xA9, 0x02, 0x60, 0xC0, 0x01, 0x00, 0x84, 0xD0, 0x09, 0x00, 0x04, 0xC1, 0xA1, 0x00, 0x88, 0xD1, 0xA9, 0x00, 0x08, 0xC0, 0x01, 0x00, 0x8C, 0xD0, 0x09, 0x00, 0x0C, 0x7F, 0xE9, 0xFB, 0x78, 0xC1, 0x7E, 0x00, 0x10, 0xC1, 0xBE, 0x00, 0x14, 0xFF, 0x60, 0x58, 0x50, 0xC1, 0x9E, 0x00, 0x0C, 0xED, 0xB9, 0x68, 0x28, 0xED, 0x8B, 0x03, 0x32, 0xC0, 0x02, 0x92, 0x94, 0xD4, 0x09, 0x02, 0x40 }; Array.Resize<byte>(ref RestoreRPC, buffer.Length); Ps3Memory.SetMemory(0x38ede8, RestoreRPC); } }
        private void iPrintln(int clientNumber, string Txt){SV_GameSendServerCommand(clientNumber, "f \"" + Txt + "\"");}
        private void iPrintlnBold(int clientNumber, string Txt){SV_GameSendServerCommand(clientNumber, "g \"" + Txt + "\"");}
        private void Cbuf_AddText(int client, string dvar) { byte[] DFT = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; byte[] RPCON = new byte[] { 0x38, 0x60, 0x00, 0x00, 0x3C, 0x80, 0x02, 0x00, 0x30, 0x84, 0x50, 0x00, 0x4B, 0xF8, 0x63, 0xFD }; byte[] RPCOFF = new byte[] { 0x81, 0x22, 0x45, 0x10, 0x81, 0x69, 0x00, 0x00, 0x88, 0x0B, 0x00, 0x0C, 0x2F, 0x80, 0x00, 0x00 }; byte[] cbuf = new byte[] { }; cbuf = Encoding.UTF8.GetBytes(dvar); Ps3Memory.SetMemory(0x2005000, cbuf); Ps3Memory.SetMemory(0x253AB8, RPCON); System.Threading.Thread.Sleep(15); Ps3Memory.SetMemory(0x253AB8, RPCOFF); Ps3Memory.SetMemory(0x2005000, DFT); }
        private void ErrorFix_Tick(object sender, EventArgs e)
        {SV_GameSendServerCommand(-1, "v loc_warnings \"0\"");
            SV_GameSendServerCommand(-1, "v loc_warningsAsErrors \"0\"");
        SV_GameSendServerCommand(-1, "v motd \"^7Project ^1Hybrid ^71.14 Made by Mx444");}
        //Above here is the MOTD for your mod menu, if you want to infect all clients to know your youtube, menu name, ect..
        //
        //
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e){}
        public static void SetIcon(int index, int type, int x, int y, int client, int width, int height, int sort, int material, int r, int g, int b, int a) { uint Ind = Convert.ToUInt32(index); uint Elem = 0x12E9858 + ((uint)Ind * 0xB4); byte[] _Type = new byte[0]; byte[] _Client = new byte[0]; byte[] _X = new byte[0]; byte[] _Y = new byte[0]; byte[] _Sort = new byte[0]; byte[] ColorR = new byte[0]; byte[] ColorG = new byte[0]; byte[] ColorB = new byte[0]; byte[] ColorA = new byte[0]; byte[] _Width = new byte[0]; byte[] _Height = new byte[0]; byte[] MaterialIndex = new byte[0]; byte[] GlowR = new byte[0]; byte[] GlowG = new byte[0]; byte[] GlowB = new byte[0]; byte[] GlowA = new byte[0]; _Type = BitConverter.GetBytes(type); Array.Reverse(_Type); _Client = BitConverter.GetBytes(client); _X = BitConverter.GetBytes(Convert.ToSingle(x)); _Y = BitConverter.GetBytes(Convert.ToSingle(y)); _Sort = BitConverter.GetBytes((float)sort); ColorR = BitConverter.GetBytes(r); Array.Resize(ref ColorR, 1); ColorG = BitConverter.GetBytes(g); Array.Resize(ref ColorG, 1); ColorB = BitConverter.GetBytes(b); Array.Resize(ref ColorB, 1); ColorA = BitConverter.GetBytes(a); Array.Resize(ref ColorA, 1); _Width = BitConverter.GetBytes(width); _Height = BitConverter.GetBytes(height); MaterialIndex = BitConverter.GetBytes(material); Array.Reverse(_X); Array.Reverse(_Y); Array.Reverse(_Width); Array.Reverse(_Height); Array.Reverse(MaterialIndex); Array.Reverse(_Client); Array.Reverse(_Sort); Ps3Memory.SetMemory(Elem, _Type); Ps3Memory.SetMemory(Elem + 0x8, _X); Ps3Memory.SetMemory(Elem + 0x4, _Y); Ps3Memory.SetMemory(Elem + 0x84 + 4, _Sort); Ps3Memory.SetMemory(Elem + 0x34, ColorR); Ps3Memory.SetMemory(Elem + 0x35, ColorG); Ps3Memory.SetMemory(Elem + 0x36, ColorB); Ps3Memory.SetMemory(Elem + 0x37, ColorA); Ps3Memory.SetMemory(Elem + 0x4c, MaterialIndex); Ps3Memory.SetMemory(Elem + 0x44, _Width); Ps3Memory.SetMemory(Elem + 0x48, _Height); Ps3Memory.SetMemory(Elem + 0xa8, _Client); System.Threading.Thread.Sleep(50); }
        public void DestroyElem(int Index){uint Elem = 0x12E9858 + ((uint)Index * 0xB4);Ps3Memory.SetMemory(Elem, new byte[0xB4]);}
        public static void MatrixText(int index, int type, int x, int y, int client, string Text, int font_, double fontScale, int sort, int width, int height, int material, int r, int g, int b, int a, int glowR, int glowG, int glowB, int glowA)        {            uint Ind = Convert.ToUInt32(index);            uint Elem = 0x12E9858 + (Ind * 0xB4);            if (Text == "")            {                byte[] text = new byte[0];                text = BitConverter.GetBytes(0);                Array.Reverse(text);                Ps3Memory.SetMemory(Elem + 0x84, text);            }            else            {                byte[] text = new byte[0];                text = BitConverter.GetBytes(MakeStr(Text));                Array.Reverse(text);                Ps3Memory.SetMemory(Elem + 0x84, text);            }            byte[] _Text = new byte[0];            byte[] _Type = new byte[0];            byte[] _Client = new byte[0];            byte[] _X = new byte[0];            byte[] _Y = new byte[0];            byte[] _Font = new byte[0];            byte[] _FontScale = new byte[0];            byte[] _Sort = new byte[0];            byte[] ColorR = new byte[0];            byte[] ColorG = new byte[0];            byte[] ColorB = new byte[0];            byte[] ColorA = new byte[0];            byte[] GlowR = new byte[0];            byte[] GlowG = new byte[0];            byte[] GlowB = new byte[0];            byte[] GlowA = new byte[0];            _Type = BitConverter.GetBytes(type);            Array.Reverse(_Type);            _Client = BitConverter.GetBytes(client);            _X = BitConverter.GetBytes(Convert.ToSingle(x));            _Y = BitConverter.GetBytes(Convert.ToSingle(y));            _Font = BitConverter.GetBytes(font_);            _FontScale = BitConverter.GetBytes((float)fontScale);            _Sort = BitConverter.GetBytes((float)sort);            ColorR = BitConverter.GetBytes(r);            Array.Resize(ref ColorR, 1);            ColorG = BitConverter.GetBytes(g);            Array.Resize(ref ColorG, 1);            ColorB = BitConverter.GetBytes(b);            Array.Resize(ref ColorB, 1);            ColorA = BitConverter.GetBytes(a);            Array.Resize(ref ColorA, 1);            Array.Reverse(_X);            Array.Reverse(_Y);            Array.Reverse(_Font);            Array.Reverse(_FontScale);            Array.Reverse(_Sort);            Array.Reverse(_Client);            GlowR = BitConverter.GetBytes(glowR);            Array.Resize(ref GlowR, 1);            GlowG = BitConverter.GetBytes(glowG);            Array.Resize(ref GlowG, 1);            GlowB = BitConverter.GetBytes(glowB);            Array.Resize(ref GlowB, 1);            GlowA = BitConverter.GetBytes(glowA);            Array.Resize(ref GlowA, 1);            Ps3Memory.SetMemory(Elem, _Type);            Ps3Memory.SetMemory(Elem + 0x88, new byte[] { 0x44, 0x7A, 0x40, 0x00 });            Ps3Memory.SetMemory(Elem + 0x8, _X);            Ps3Memory.SetMemory(Elem + 0x4, _Y);            Ps3Memory.SetMemory(Elem + 0x84, _Text);            Ps3Memory.SetMemory(Elem + 0x14, _FontScale);            Ps3Memory.SetMemory(Elem + 0x28, _Font);            Ps3Memory.SetMemory(Elem + 0x84 + 4, _Sort);            Ps3Memory.SetMemory(Elem + 0x34, ColorR);            Ps3Memory.SetMemory(Elem + 0x35, ColorG);            Ps3Memory.SetMemory(Elem + 0x36, ColorB);            Ps3Memory.SetMemory(Elem + 0x37, ColorA);            Ps3Memory.SetMemory(Elem + 0xa8, _Client);            Ps3Memory.SetMemory(Elem + 0x8C, GlowR);            Ps3Memory.SetMemory(Elem + 0x8D, GlowG);            Ps3Memory.SetMemory(Elem + 0x8E, GlowB);            Ps3Memory.SetMemory(Elem + 0x8F, GlowA);            Ps3Memory.SetMemory(Elem + 0xB3, new byte[] { 0x01 });            DEX.Extension.WriteInt32(Elem + fxBirthTime, (Int32)DEX.Extension.ReadUInt32(0x12E0304));            DEX.Extension.WriteInt32(Elem + fxLetterTime, 90);           DEX.Extension.WriteInt32(Elem + fxDecayStartTime, 7500);            DEX.Extension.WriteInt32(Elem + fxDecayDuration, 1000);          System.Threading.Thread.Sleep(30);        }
        public static void SetText(int index, int type, int x, int y, int client, string Text, int font_, double fontScale, int sort, int material, int r, int g, int b, int a, int glowR, int glowG, int glowB, int glowA) { uint Ind = Convert.ToUInt32(index); uint Elem = 0x12E9858 + (Ind * 0xB4); if (Text == "") { byte[] text = new byte[0]; text = BitConverter.GetBytes(0); Array.Reverse(text); Ps3Memory.SetMemory(Elem + 0x84, text); } else { byte[] text = new byte[0]; text = BitConverter.GetBytes(MakeStr(Text)); Array.Reverse(text); Ps3Memory.SetMemory(Elem + 0x84, text); } byte[] _Text = new byte[0]; byte[] _Type = new byte[0]; byte[] _Client = new byte[0]; byte[] _X = new byte[0]; byte[] _Y = new byte[0]; byte[] _Font = new byte[0]; byte[] _FontScale = new byte[0]; byte[] _Sort = new byte[0]; byte[] ColorR = new byte[0]; byte[] ColorG = new byte[0]; byte[] ColorB = new byte[0]; byte[] ColorA = new byte[0]; byte[] MaterialIndex = new byte[0]; byte[] GlowR = new byte[0]; byte[] GlowG = new byte[0]; byte[] GlowB = new byte[0]; byte[] GlowA = new byte[0]; _Type = BitConverter.GetBytes(type); Array.Reverse(_Type); _Client = BitConverter.GetBytes(client); _X = BitConverter.GetBytes(Convert.ToSingle(x)); _Y = BitConverter.GetBytes(Convert.ToSingle(y)); _Font = BitConverter.GetBytes(font_); _FontScale = BitConverter.GetBytes((float)fontScale); _Sort = BitConverter.GetBytes((float)sort); ColorR = BitConverter.GetBytes(r); Array.Resize(ref ColorR, 1); ColorG = BitConverter.GetBytes(g); Array.Resize(ref ColorG, 1); ColorB = BitConverter.GetBytes(b); Array.Resize(ref ColorB, 1); ColorA = BitConverter.GetBytes(a); Array.Resize(ref ColorA, 1); MaterialIndex = BitConverter.GetBytes(material); Array.Reverse(_X); Array.Reverse(_Y); Array.Reverse(_Font); Array.Reverse(_FontScale); Array.Reverse(_Sort); Array.Reverse(MaterialIndex); Array.Reverse(_Client); GlowR = BitConverter.GetBytes(glowR); Array.Resize(ref GlowR, 1); GlowG = BitConverter.GetBytes(glowG); Array.Resize(ref GlowG, 1); GlowB = BitConverter.GetBytes(glowB); Array.Resize(ref GlowB, 1); GlowA = BitConverter.GetBytes(glowA); Array.Resize(ref GlowA, 1); Ps3Memory.SetMemory(Elem, _Type); Ps3Memory.SetMemory(Elem + 0x88, new byte[] { 0x44, 0x7A, 0x40, 0x00 }); Ps3Memory.SetMemory(Elem + 0x8, _X); Ps3Memory.SetMemory(Elem + 0x4, _Y); Ps3Memory.SetMemory(Elem + 0x84, _Text); Ps3Memory.SetMemory(Elem + 0x14, _FontScale); Ps3Memory.SetMemory(Elem + 0x28, _Font); Ps3Memory.SetMemory(Elem + 0x84 + 4, _Sort); Ps3Memory.SetMemory(Elem + 0x34, ColorR); Ps3Memory.SetMemory(Elem + 0x35, ColorG); Ps3Memory.SetMemory(Elem + 0x36, ColorB); Ps3Memory.SetMemory(Elem + 0x37, ColorA); Ps3Memory.SetMemory(Elem + 0x4c, MaterialIndex); Ps3Memory.SetMemory(Elem + 0xa8, _Client); Ps3Memory.SetMemory(Elem + 0x8C, GlowR); Ps3Memory.SetMemory(Elem + 0x8D, GlowG); Ps3Memory.SetMemory(Elem + 0x8E, GlowB); Ps3Memory.SetMemory(Elem + 0x8F, GlowA); Ps3Memory.SetMemory(Elem + 0xB3, new byte[] { 0x01 }); System.Threading.Thread.Sleep(50); }
        private void LoadSub(int client, string submenu)
        {
            if (submenu == "PlayerOpts")
            {
                CurPlayer[client] = MenuScroll[client];
            }
            SubMenu[client] = submenu;
            MenuScroll[client] = 0;
            NewMenuStr[client] = getMenuString(SubMenu[client], client);
            LoadText(client, NewMenuStr[client]);
            FuncScroll(client, MenuScroll[client]);
        }
        private void LoadText(int ID, string newText)
        {
            if (ID == 0)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 1)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 2)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 3)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 4)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 5)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 6)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 7)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 8)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 9)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 10)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 11)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 12)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 13)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 14)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 15)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 16)
            {
                ChangeText(121 + (ID * 18), newText);
            }
            if (ID == 17)
            {
                ChangeText(121 + (ID * 18), newText);
            }
        }
        public static uint FadeOverTime(uint elem, short Time, Byte R = 0, Byte G = 0, Byte B = 0, Byte A = 0){UInt32 Elem = 0x12E9858 + (elem) * 0xB4;DEX.Extension.WriteInt32(Elem + 0x3C, (Int32)DEX.Extension.ReadUInt32(0x12E0304));DEX.Extension.WriteBytes(Elem + 0x38, DEX.Extension.ReadBytes(Elem + 0x34, 4));DEX.Extension.WriteInt32(Elem + 0x40, (int)(Time * 1000 + 0.5));DEX.Extension.WriteBytes(Elem + 0x34, new Byte[] { R, G, B, A });return elem;}
        public static uint MoveOverTime(uint Elem, short Time, float X, float Y){uint elem = 0x12E9858 + (Elem) * 0xB4; DEX.Extension.WriteFloat(elem + 100, DEX.Extension.ReadFloat(elem + 8));DEX.Extension.WriteFloat(elem + 0x60, DEX.Extension.ReadFloat(elem + 4));DEX.Extension.WriteInt32(elem + 0x74, Time);DEX.Extension.WriteInt32(elem + 0x70, (Int32)DEX.Extension.ReadUInt32(0x12E0304));DEX.Extension.WriteFloat(elem + 8, X);DEX.Extension.WriteFloat(elem + 4, Y);return Elem;}
        public static uint MakeStr(string Text) { RPC(true); double num = 1.5; DateTime.UtcNow.AddSeconds(num); byte[] buffer1 = new byte[4]; byte[] buffer4 = new byte[4]; byte[] array = new byte[4]; if (Text == null) { Text = "-"; } byte[] bytes = Encoding.ASCII.GetBytes(Text); Array.Resize<byte>(ref bytes, bytes.Length + 1); Ps3Memory.SetMemory(0x10050010, bytes); Ps3Memory.SetMemory(0x10050000, new byte[] { 0x00000001 }); System.Threading.Thread.Sleep(25); Ps3Memory.GetMemory(0x10050004, array); Array.Reverse(array); RPC(false); return BitConverter.ToUInt32(array, 0); }
        private void SetShader(int index, int width, int height) { uint Ind = Convert.ToUInt32(index); uint Elem = 0x12E9858 + ((uint)Ind * 0xB4); byte[] _Width = BitConverter.GetBytes(width); byte[] _Height = BitConverter.GetBytes(height); Array.Reverse(_Width); Array.Reverse(_Height); Ps3Memory.SetMemory(Elem + 0x44, _Width); Ps3Memory.SetMemory(Elem + 0x48, _Height); }
        private void ChangeAlpha(int index, int alpha) { uint Ind = Convert.ToUInt32(index); uint Elem = 0x12E9858 + ((uint)Ind * 0xB4); byte[] ColorA = BitConverter.GetBytes(alpha); Array.Resize(ref ColorA, 1); Ps3Memory.SetMemory(Elem + 0x37, ColorA); }
        private void MoveScroller(uint index, float Y)
        {
            uint Ind = Convert.ToUInt32(index);
            uint Elem = 0x12E9858 + ((uint)Ind * 0xB4);
            MoveOverTime(index + ((uint)client * 18), 50, 240, Y);
            Thread.Sleep(10);
        }
        private string getLocalName(){string host;byte[] yournamebytes = new byte[18];Ps3Memory.GetMemory(0x01f9f11c, yournamebytes);host = Encoding.ASCII.GetString(yournamebytes);host.Replace(Convert.ToChar(0x0).ToString(), string.Empty);return host;}
        private void ChangeText(int index, string Txt)
        {
            uint Ind = Convert.ToUInt32(index);
            uint Elem = 0x12E9858 + (Ind * 0xB4);
            byte[] text = BitConverter.GetBytes(MakeStr(Txt));
            Array.Reverse(text);
            Ps3Memory.SetMemory(Elem + 0x84, text);
        }
        private void ChangeTitle(int index, string Txt)
        {
            uint Ind = Convert.ToUInt32(index);
            uint Elem = 0x12E9858 + (Ind * 0xB4);
            byte[] text = BitConverter.GetBytes(MakeStr(Txt));
            Array.Reverse(text);
            Ps3Memory.SetMemory(Elem + 0x84, text);
        }
        private void removeAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ps3Memory.SetMemory((0x014E2224 + ((uint)dataGridView1.CurrentRow.Index * 0x3700)), new byte[] { 0x00, 0x00, 0x00 });
            Status[dataGridView1.CurrentRow.Index] = "Un-Verified";
            IsVerified[dataGridView1.CurrentRow.Index] = false;
            iPrintln(dataGridView1.CurrentRow.Index, "^1Access Removed Nigga!");
            iPrintlnBold(dataGridView1.CurrentRow.Index, "^1Access Removed Nigga!");
        }

        private void verifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //
        private void vIPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //
        private void coHostToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //
        private void hostToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        //
          
        private void GivePlayerVerified(int client)
        {
            iPrintlnBold(CurPlayer[client], "^2The Host Has Given You Verified Access!"); 
            iPrintln(CurPlayer[client], "^2Press [{+melee}] To Open The Menu!"); 
            iPrintln(CurPlayer[client], "^2Press [{+gostand}] To Select!");
            iPrintln(CurPlayer[client], "^2Press [{+usereload}] To Go Back"); 
            iPrintln(CurPlayer[client], "^2Press [{+attack}] / [{+speed_throw}] To Scroll");
            Status[CurPlayer[client]] = "Verified"; IsVerified[CurPlayer[client]] = true; 
            getMenu("Verified",CurPlayer[client]);
            SubMenu[CurPlayer[client]] = "Verified"; 
            iPrintln(client, "^2" + GetNamesVer(CurPlayer[client]) + " ^3Given ^2Verified ^3Access");
            if (CurPlayer[client] == 0) { if (backgroundWorker1.IsBusy == false) { backgroundWorker1.RunWorkerAsync();
            } } else if (CurPlayer[client] == 1) { if (backgroundWorker2.IsBusy == false) { backgroundWorker2.RunWorkerAsync();
            } } else if (CurPlayer[client] == 2) { if (backgroundWorker3.IsBusy == false) { backgroundWorker3.RunWorkerAsync();
            } } else if (CurPlayer[client] == 3) { if (backgroundWorker4.IsBusy == false) { backgroundWorker4.RunWorkerAsync();
            } } else if (CurPlayer[client] == 4) { if (backgroundWorker5.IsBusy == false) { backgroundWorker5.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 5) { if (backgroundWorker6.IsBusy == false) { backgroundWorker6.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 6) { if (backgroundWorker7.IsBusy == false) { backgroundWorker7.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 7) { if (backgroundWorker8.IsBusy == false) { backgroundWorker8.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 8) { if (backgroundWorker9.IsBusy == false) { backgroundWorker9.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 9) { if (backgroundWorker10.IsBusy == false) { backgroundWorker10.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 10) { if (backgroundWorker11.IsBusy == false) { backgroundWorker11.RunWorkerAsync();
            } } else if (CurPlayer[client] == 11) { if (backgroundWorker12.IsBusy == false) { backgroundWorker12.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 12) { if (backgroundWorker13.IsBusy == false) { backgroundWorker13.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 13) { if (backgroundWorker14.IsBusy == false) { backgroundWorker14.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 14) { if (backgroundWorker15.IsBusy == false) { backgroundWorker15.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 15) { if (backgroundWorker16.IsBusy == false) { backgroundWorker16.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 16) { if (backgroundWorker17.IsBusy == false) { backgroundWorker17.RunWorkerAsync(); 
            } } else if (CurPlayer[client] == 17) { if (backgroundWorker18.IsBusy == false) { backgroundWorker18.RunWorkerAsync(); 
            } }
        }


        private void GivePlayerVIP(int client)
        {
            iPrintlnBold(CurPlayer[client], "^2The Host Has Given You VIP Access!"); iPrintln(CurPlayer[client], "^2Press [{+melee}] To Open The Menu!"); iPrintln(CurPlayer[client], "^2Press [{+gostand}] To Select!"); iPrintln(CurPlayer[client], "^2Press [{+usereload}] To Go Back"); iPrintln(CurPlayer[client], "^2Press [{+attack}] / [{+speed_throw}] To Scroll"); Status[CurPlayer[client]] = "VIP"; IsVerified[CurPlayer[client]] = true; SubMenu[CurPlayer[client]] = "Main"; iPrintln(client, "^2" + GetNamesVer(CurPlayer[client]) + " ^3Given ^2VIP ^3Access"); if (CurPlayer[client] == 0) { if (backgroundWorker1.IsBusy == false) { backgroundWorker1.RunWorkerAsync(); } } else if (CurPlayer[client] == 1) { if (backgroundWorker2.IsBusy == false) { backgroundWorker2.RunWorkerAsync(); } } else if (CurPlayer[client] == 2) { if (backgroundWorker3.IsBusy == false) { backgroundWorker3.RunWorkerAsync(); } } else if (CurPlayer[client] == 3) { if (backgroundWorker4.IsBusy == false) { backgroundWorker4.RunWorkerAsync(); } } else if (CurPlayer[client] == 4) { if (backgroundWorker5.IsBusy == false) { backgroundWorker5.RunWorkerAsync(); } } else if (CurPlayer[client] == 5) { if (backgroundWorker6.IsBusy == false) { backgroundWorker6.RunWorkerAsync(); } } else if (CurPlayer[client] == 6) { if (backgroundWorker7.IsBusy == false) { backgroundWorker7.RunWorkerAsync(); } } else if (CurPlayer[client] == 7) { if (backgroundWorker8.IsBusy == false) { backgroundWorker8.RunWorkerAsync(); } } else if (CurPlayer[client] == 8) { if (backgroundWorker9.IsBusy == false) { backgroundWorker9.RunWorkerAsync(); } } else if (CurPlayer[client] == 9) { if (backgroundWorker10.IsBusy == false) { backgroundWorker10.RunWorkerAsync(); } } else if (CurPlayer[client] == 10) { if (backgroundWorker11.IsBusy == false) { backgroundWorker11.RunWorkerAsync(); } } else if (CurPlayer[client] == 11) { if (backgroundWorker12.IsBusy == false) { backgroundWorker12.RunWorkerAsync(); } } else if (CurPlayer[client] == 12) { if (backgroundWorker13.IsBusy == false) { backgroundWorker13.RunWorkerAsync(); } } else if (CurPlayer[client] == 13) { if (backgroundWorker14.IsBusy == false) { backgroundWorker14.RunWorkerAsync(); } } else if (CurPlayer[client] == 14) { if (backgroundWorker15.IsBusy == false) { backgroundWorker15.RunWorkerAsync(); } } else if (CurPlayer[client] == 15) { if (backgroundWorker16.IsBusy == false) { backgroundWorker16.RunWorkerAsync(); } } else if (CurPlayer[client] == 16) { if (backgroundWorker17.IsBusy == false) { backgroundWorker17.RunWorkerAsync(); } } else if (CurPlayer[client] == 17) { if (backgroundWorker18.IsBusy == false) { backgroundWorker18.RunWorkerAsync(); } }
        }

        private void GivePlayerCOHost(int client)
        {
            iPrintlnBold(CurPlayer[client], "^2The Host Has Given You Co-Host Access!"); iPrintln(CurPlayer[client], "^2Press [{+melee}] To Open The Menu!"); iPrintln(CurPlayer[client], "^2Press [{+gostand}] To Select!"); iPrintln(CurPlayer[client], "^2Press [{+usereload}] To Go Back"); iPrintln(CurPlayer[client], "^2Press [{+attack}] / [{+speed_throw}] To Scroll"); Status[CurPlayer[client]] = "Co-Host"; IsVerified[CurPlayer[client]] = true; SubMenu[CurPlayer[client]] = "Main"; iPrintln(client, "^2" + GetNamesVer(CurPlayer[client]) + " ^3Given ^2Co-Host ^3Access"); if (CurPlayer[client] == 0) { if (backgroundWorker1.IsBusy == false) { backgroundWorker1.RunWorkerAsync(); } } else if (CurPlayer[client] == 1) { if (backgroundWorker2.IsBusy == false) { backgroundWorker2.RunWorkerAsync(); } } else if (CurPlayer[client] == 2) { if (backgroundWorker3.IsBusy == false) { backgroundWorker3.RunWorkerAsync(); } } else if (CurPlayer[client] == 3) { if (backgroundWorker4.IsBusy == false) { backgroundWorker4.RunWorkerAsync(); } } else if (CurPlayer[client] == 4) { if (backgroundWorker5.IsBusy == false) { backgroundWorker5.RunWorkerAsync(); } } else if (CurPlayer[client] == 5) { if (backgroundWorker6.IsBusy == false) { backgroundWorker6.RunWorkerAsync(); } } else if (CurPlayer[client] == 6) { if (backgroundWorker7.IsBusy == false) { backgroundWorker7.RunWorkerAsync(); } } else if (CurPlayer[client] == 7) { if (backgroundWorker8.IsBusy == false) { backgroundWorker8.RunWorkerAsync(); } } else if (CurPlayer[client] == 8) { if (backgroundWorker9.IsBusy == false) { backgroundWorker9.RunWorkerAsync(); } } else if (CurPlayer[client] == 9) { if (backgroundWorker10.IsBusy == false) { backgroundWorker10.RunWorkerAsync(); } } else if (CurPlayer[client] == 10) { if (backgroundWorker11.IsBusy == false) { backgroundWorker11.RunWorkerAsync(); } } else if (CurPlayer[client] == 11) { if (backgroundWorker12.IsBusy == false) { backgroundWorker12.RunWorkerAsync(); } } else if (CurPlayer[client] == 12) { if (backgroundWorker13.IsBusy == false) { backgroundWorker13.RunWorkerAsync(); } } else if (CurPlayer[client] == 13) { if (backgroundWorker14.IsBusy == false) { backgroundWorker14.RunWorkerAsync(); } } else if (CurPlayer[client] == 14) { if (backgroundWorker15.IsBusy == false) { backgroundWorker15.RunWorkerAsync(); } } else if (CurPlayer[client] == 15) { if (backgroundWorker16.IsBusy == false) { backgroundWorker16.RunWorkerAsync(); } } else if (CurPlayer[client] == 16) { if (backgroundWorker17.IsBusy == false) { backgroundWorker17.RunWorkerAsync(); } } else if (CurPlayer[client] == 17) { if (backgroundWorker18.IsBusy == false) { backgroundWorker18.RunWorkerAsync(); } }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void FuncScroll(int client, int scroll)
        {
            int math = 30 + (18 * scroll);
            MoveScroller(123 + ((uint)client * 18), math);
        }
        private int getMaxScroll(string Submenu, int client)
        {
            if (Submenu == "Main")
                return getScrollMain(client);
            else if (Submenu == "Main Mods Menu")
                return 14;
            else if (Submenu == "Fun Menu")
                return 5;
            else if (Submenu == "Aimbot Menu")
                return 2;
            else if (Submenu == "Message Menu")
                return 5;
            else if (Submenu == "^6Verified Menu")
                return 4;
            else if (Submenu == "^3VIP Menu")
                return 6;
            else if (Submenu == "^5Co-Host Menu")
                return 5;
            else if (Submenu == "^2Host Menu")
                return 4;
            else if (Submenu == "^7Vision Menu")
                return 15;
            else if (Submenu == "Model Menu")
                return 13;
            else if (Submenu == "Weapon Menu")
                return 5;
            else if (Submenu == "Bullet Type Menu")
                return 7;
            else if (Submenu == "Teleport Menu")
                return 5;
            else if (Submenu == "Map Menu")
                return 13;
            else if (Submenu == "Game mode Menu")
                return 7;
            else if (Submenu == "Lobby Menu")
                return 11;
            else if (Submenu == "Players")
                return 18;
            else if (Submenu == "PlayerOpts")
                return 19;
            else if (Submenu == "Extra Menu")
                return 5;
            else if (Submenu == "All Players Menu")
                return 13;
            else if (Submenu == "Credits")
                return 0;
            else
                return 0;
        }
        private int getScrollMain(int client)
        {
            if (Status[client] == "Host")
                return 19;
            else if (Status[client] == "Co-Host")
                return 15;
            else if (Status[client] == "VIP")
                return 6;
            else if (Status[client] == "Verified")
                return 1;
            else
                return 0;
        }


        //Buttons Shit
        public static Boolean IsButtonR3(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DD + ((UInt32)client * 0x3700)), buffer);if (buffer[2] == 0x04){return true;}return false;}
        public static Boolean IsButtonR2(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DD + ((UInt32)client * 0x3700)), buffer);if (buffer[1] == 0x40){return true;}return false;}
        public static Boolean IsButtonL2(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DD + ((UInt32)client * 0x3700)), buffer);if (buffer[1] == 0x80){return true;}return false;}
        public static Boolean IsButtonL3(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DD + ((UInt32)client * 0x3700)), buffer);if (buffer[1] == 0x20){return true;}return false;}
        public static Boolean IsButtonX(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DD + ((UInt32)client * 0x3700)), buffer);if (buffer[1] == 0x04){return true;}return false;}
        public static Boolean IsButtonR1(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DD + ((UInt32)client * 0x3700)), buffer);if (buffer[2] == 0x01){return true;}return false;}
        public static Boolean IsButtonSq(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DE + ((UInt32)client * 0x3700)), buffer);if (buffer[1] == 0x20){return true;}return false;}
        public static Boolean IsButtonL1(Int32 client){Byte[] buffer = new Byte[3];Ps3Memory.GetMemory((0x014E53DE + ((UInt32)client * 0x3700)), buffer);if (buffer[0] == 0x08){return true;}return false;}
        


        bool SpawnCheck0;bool SpawnCheck1;bool SpawnCheck2;bool SpawnCheck3;bool SpawnCheck4;bool SpawnCheck5;bool SpawnCheck6;bool SpawnCheck7;bool SpawnCheck8;bool SpawnCheck9;bool SpawnCheck10;bool SpawnCheck11;bool SpawnCheck12;bool SpawnCheck13;bool SpawnCheck14;bool SpawnCheck15;bool SpawnCheck16;bool SpawnCheck17;


        

        

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 0;
            if (SpawnCheck0 == false)
            {
                FastSpawnerz(client);
                SpawnCheck0 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {

                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 1;
            if (SpawnCheck1 == false)
            {
                FastSpawnerz(client);
                SpawnCheck1 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 2;
            if (SpawnCheck2 == false)
            {
                FastSpawnerz(client);
                SpawnCheck2 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                            
                            
                           
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                               
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                                
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                            
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 3;
            if (SpawnCheck3 == false)
            {
                FastSpawnerz(client);
                SpawnCheck3 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 4;
            if (SpawnCheck4 == false)
            {
                FastSpawnerz(client);
                SpawnCheck4 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 5;
            if (SpawnCheck5 == false)
            {
                FastSpawnerz(client);
                SpawnCheck5 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 6;
            if (SpawnCheck6 == false)
            {
                FastSpawnerz(client);
                SpawnCheck6 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker8_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 7;
            if (SpawnCheck7 == false)
            {
                FastSpawnerz(client);
                SpawnCheck7 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker9_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 8;
            if (SpawnCheck8 == false)
            {
                FastSpawnerz(client);
                SpawnCheck8 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker10_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 9;
            if (SpawnCheck9 == false)
            {
                FastSpawnerz(client);
                SpawnCheck9 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker11_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 10;
            if (SpawnCheck10 == false)
            {
                FastSpawnerz(client);
                SpawnCheck10 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker12_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 11;
            if (SpawnCheck11 == false)
            {
                FastSpawnerz(client);
                SpawnCheck11 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker13_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 12;
            if (SpawnCheck12 == false)
            {
                FastSpawnerz(client);
                SpawnCheck12 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker14_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 13;
            if (SpawnCheck13 == false)
            {
                FastSpawnerz(client);
                SpawnCheck13 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker15_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 14;
            if (SpawnCheck14 == false)
            {
                FastSpawnerz(client);
                SpawnCheck14 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker16_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 15;
            if (SpawnCheck15 == false)
            {
                FastSpawnerz(client);
                SpawnCheck15 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker17_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 16;
            if (SpawnCheck16 == false)
            {
                FastSpawnerz(client);
                SpawnCheck16 = true;
            }
            for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }
        private void backgroundWorker18_DoWork(object sender, DoWorkEventArgs e)
        {
            Ps3Memory.Connect();
            int client = 17;
            if (SpawnCheck17 == false)
            {
                FastSpawnerz(client);
                SpawnCheck17 = true;
            }
                        for (; ; )
            {
                if (MenuRunning == true)
                {
                    {
                        if (IsButtonR3(client) && MenuOpen[client] == false && IsVerified[client] == true)//&& MenuOpen[client] == false
                        {
                            OpenMenu(client);
                            MenuOpen[client] = true;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x04 });
                        }
                        if (IsButtonL1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]--;
                            if (MenuScroll[client] < 0)
                            {
                                MenuScroll[client] = getMaxScroll(SubMenu[client], client);
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonR1(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuScroll[client]++;
                            if (MenuScroll[client] > getMaxScroll(SubMenu[client], client))
                            {
                                MenuScroll[client] = 0;
                            }
                            FuncScroll(client, MenuScroll[client]);
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] == "Main")
                        {
                            CloseMenu(client);
                            MenuOpen[client] = false;
                            Ps3Memory.SetMemory((0x014E5623 + ((uint)client * 0x3700)), new byte[] { 0x00 });
                        }
                        if (IsButtonSq(client) && MenuOpen[client] == true && IsVerified[client] == true && SubMenu[client] != "Main")
                        {
                            LoadSub(client, "Main");
                            Thread.Sleep(20);
                        }
                        if (IsButtonX(client) && MenuOpen[client] == true && IsVerified[client] == true)
                        {
                            MenuSelection(client, MenuScroll[client], SubMenu[client]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            
           
        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            
            GetClients();
            Mw2Library.RPC.RemovelocWarnings(-1);
            SV_GameSendServerCommand(-1, "v clanname {PH}");
            for (int i = 0; i < 18; i++)
            { Status[i] = "Un-Verified";
            flatButton2.BaseColor = Color.Blue;
            groupBox1.ForeColor = Color.Blue;
            formSkin1.FlatColor = Color.Blue;
        }
    
    }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (PS3.GetCurrentAPI() == SelectAPI.TargetManager)
                {
                    PS3.ConnectTarget(0);
                    flatButton1.BaseColor = Color.Blue;
                    flatButton1.Text = "Connected !";
                    label7.Text = "DEX Connected ";
                    label7.ForeColor = Color.Blue;
                }
            }
            catch
            {
                flatButton1.BaseColor = Color.Black;
                flatButton1.Text = "Error !";

            }
            try
            {
                if (PS3.GetCurrentAPI() == SelectAPI.ControlConsole)
                {
                    PS3.ConnectTarget();
                    label7.Text = "CEX Connected ";
                    flatButton1.BaseColor = Color.Blue;
                    flatButton1.Text = "Connected !";
                    label7.ForeColor = Color.Blue;
                    PS3.CCAPI.Notify(CCAPI.NotifyIcon.FRIEND, ("Welcome CEX User"));
                    PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Single);

                }
            }
            catch
            {
                flatButton1.BaseColor = Color.Black;
                flatButton1.Text = "Error !";
                label7.Text = "Error!";
                label7.ForeColor = Color.Black;
            }
           
    }

        private void flatTextBox1_TextChanged(object sender, EventArgs e)
        {
            Ps3Memory.SetMemory(0x01f9f11c, Encoding.ASCII.GetBytes(flatTextBox1.Text + "\0"));
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void flatCheckBox1_CheckedChanged(object sender)
        {
            if (nigga == false) { RefreshGrid.Enabled = true; RefreshGrid.Start(); nigga = true; } else { RefreshGrid.Enabled = false; RefreshGrid.Stop(); nigga = false; }
        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            ﻿try
             {
                 if (PS3.GetCurrentAPI() == SelectAPI.TargetManager)
                 {

                     if (System.IO.File.Exists("C:/Program Files/SN Systems/PS3/bin/ps3debugger.exe"))
                     {
                         System.IO.File.Exists("C:/Program Files/SN Systems/PS3/bin/ps3debugger.exe");

                     }
                     else if (System.IO.File.Exists("C:/Program Files (x86)/SN Systems/PS3/bin/ps3debugger.exe"))
                     {
                         System.IO.File.Exists("C:/Program Files (x86)/SN Systems/PS3/bin/ps3debugger.exe");

                     }

                     else
                     {
                         DialogResult dialogResult = MessageBox.Show("Could not locate Debugger.\nPlease copy Debugger to the current directory\nC:/Program Files/SN Systems/PS3/bin/<here>", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                         if (dialogResult == DialogResult.OK)
                         {


                             Close();

                         }
                     }
                 }
                 Ps3Memory.AttachProcess();
                 DEX.AttachProcess();
                 if (PS3.AttachProcess())
                 Ps3Memory.AttachProcess();
                 flatButton3.BaseColor = Color.Blue;
                 formSkin1.BorderColor = Color.Blue;
                 groupBox1.ForeColor = Color.Blue;
                 flatTextBox1.ForeColor = Color.Blue;
                 flatButton3.Text = "Attached !";
                 label8.Text = "Ps3 Attached";
                 label8.ForeColor = Color.Blue;
                 Mw2Library.RPC.CallFunction.Init();
                 Ps3Memory.SetMemory(0x10030000, new byte[] { 0x00, 0x72, 0x4C, 0x38 });
                 Ps3Memory.SetMemory(0x10030004, new byte[] { 0x00, 0x73, 0x4B, 0xE8 });
                 Ps3Memory.SetMemory(0x1d0ce6c, new byte[1]); byte[] GetName = new byte[22];
                 Ps3Memory.GetMemory(0x01f9f11c, GetName);
                 flatTextBox1.Text = Encoding.ASCII.GetString(GetName);
                 byte[] FPSTEXTREMOVE = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                 PS3.SetMemory(0x577570, FPSTEXTREMOVE);
                 PS3.CCAPI.Notify(CCAPI.NotifyIcon.FRIEND, ("Game Attached. Thanks for using Project Hybrid 1.14"));
                 PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Double);


             }
             catch
             {
                 flatButton3.BaseColor = Color.Black;
                 flatButton3.Text = "Error !";
                 MessageBox.Show("No game process found", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);

             }
          
        }

        private void radioButton1_CheckedChanged(object sender)
        {
            PS3.ChangeAPI(SelectAPI.ControlConsole);
        }

        private void radioButton2_CheckedChanged(object sender)
        {
            PS3.ChangeAPI(SelectAPI.TargetManager);
        }

        private void verifiedMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Status[dataGridView1.CurrentRow.Index] = "Verified";
            IsVerified[dataGridView1.CurrentRow.Index] = true;
            SubMenu[dataGridView1.CurrentRow.Index] = "^6Verified Menu";

            MenuRunning = true;
            if (dataGridView1.CurrentRow.Index == 0)
            {
                if (backgroundWorker1.IsBusy == false)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else if (dataGridView1.CurrentRow.Index == 1)
            {
                if (backgroundWorker2.IsBusy == false)
                {
                    backgroundWorker2.RunWorkerAsync();
                }
            }
            else if (dataGridView1.CurrentRow.Index == 2)
            {
                if (backgroundWorker3.IsBusy == false)
                { backgroundWorker3.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 3)
            {
                if (backgroundWorker4.IsBusy == false)
                {
                    backgroundWorker4.RunWorkerAsync();
                }
            }
            else if (dataGridView1.CurrentRow.Index == 4)
            {
                if (backgroundWorker5.IsBusy == false)
                { backgroundWorker5.RunWorkerAsync(); }
            }
            else if
                (dataGridView1.CurrentRow.Index == 5)
            {
                if (backgroundWorker6.IsBusy == false)
                { backgroundWorker6.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 6)
            {
                if (backgroundWorker7.IsBusy == false)
                { backgroundWorker7.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 7)
            {
                if (backgroundWorker8.IsBusy == false)
                { backgroundWorker8.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 8)
            {
                if (backgroundWorker9.IsBusy == false)
                { backgroundWorker9.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 9)
            {
                if (backgroundWorker10.IsBusy == false)
                { backgroundWorker10.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 10)
            {
                if (backgroundWorker11.IsBusy == false)
                { backgroundWorker11.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 11)
            {
                if (backgroundWorker12.IsBusy == false)
                { backgroundWorker12.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 12)
            {
                if (backgroundWorker13.IsBusy == false)
                { backgroundWorker13.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 13)
            {
                if (backgroundWorker14.IsBusy == false)
                { backgroundWorker14.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 14)
            {
                if (backgroundWorker15.IsBusy == false)
                { backgroundWorker15.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 15)
            {
                if (backgroundWorker16.IsBusy == false)
                { backgroundWorker16.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 16)
            {
                if (backgroundWorker17.IsBusy == false)
                { backgroundWorker17.RunWorkerAsync(); }
            }
            else if (dataGridView1.CurrentRow.Index == 17)
            {
                if (backgroundWorker18.IsBusy == false)
                { backgroundWorker18.RunWorkerAsync(); }
            }
        }

        private void vipMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Status[dataGridView1.CurrentRow.Index] = "VIP";
            IsVerified[dataGridView1.CurrentRow.Index] = true;
            SubMenu[dataGridView1.CurrentRow.Index] = "Main";
            MenuRunning = true;
            if (dataGridView1.CurrentRow.Index == 0) { if (backgroundWorker1.IsBusy == false) { backgroundWorker1.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 1) { if (backgroundWorker2.IsBusy == false) { backgroundWorker2.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 2) { if (backgroundWorker3.IsBusy == false) { backgroundWorker3.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 3) { if (backgroundWorker4.IsBusy == false) { backgroundWorker4.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 4) { if (backgroundWorker5.IsBusy == false) { backgroundWorker5.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 5) { if (backgroundWorker6.IsBusy == false) { backgroundWorker6.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 6) { if (backgroundWorker7.IsBusy == false) { backgroundWorker7.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 7) { if (backgroundWorker8.IsBusy == false) { backgroundWorker8.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 8) { if (backgroundWorker9.IsBusy == false) { backgroundWorker9.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 9) { if (backgroundWorker10.IsBusy == false) { backgroundWorker10.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 10) { if (backgroundWorker11.IsBusy == false) { backgroundWorker11.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 11) { if (backgroundWorker12.IsBusy == false) { backgroundWorker12.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 12) { if (backgroundWorker13.IsBusy == false) { backgroundWorker13.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 13) { if (backgroundWorker14.IsBusy == false) { backgroundWorker14.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 14) { if (backgroundWorker15.IsBusy == false) { backgroundWorker15.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 15) { if (backgroundWorker16.IsBusy == false) { backgroundWorker16.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 16) { if (backgroundWorker17.IsBusy == false) { backgroundWorker17.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 17) { if (backgroundWorker18.IsBusy == false) { backgroundWorker18.RunWorkerAsync(); } }
        }

        private void coHostMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
                        Status[dataGridView1.CurrentRow.Index] = "Co-Host";
            IsVerified[dataGridView1.CurrentRow.Index] = true;
            SubMenu[dataGridView1.CurrentRow.Index] = "Main";
            MenuRunning = true;
            if (dataGridView1.CurrentRow.Index == 0) { if (backgroundWorker1.IsBusy == false) { backgroundWorker1.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 1) { if (backgroundWorker2.IsBusy == false) { backgroundWorker2.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 2) { if (backgroundWorker3.IsBusy == false) { backgroundWorker3.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 3) { if (backgroundWorker4.IsBusy == false) { backgroundWorker4.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 4) { if (backgroundWorker5.IsBusy == false) { backgroundWorker5.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 5) { if (backgroundWorker6.IsBusy == false) { backgroundWorker6.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 6) { if (backgroundWorker7.IsBusy == false) { backgroundWorker7.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 7) { if (backgroundWorker8.IsBusy == false) { backgroundWorker8.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 8) { if (backgroundWorker9.IsBusy == false) { backgroundWorker9.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 9) { if (backgroundWorker10.IsBusy == false) { backgroundWorker10.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 10) { if (backgroundWorker11.IsBusy == false) { backgroundWorker11.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 11) { if (backgroundWorker12.IsBusy == false) { backgroundWorker12.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 12) { if (backgroundWorker13.IsBusy == false) { backgroundWorker13.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 13) { if (backgroundWorker14.IsBusy == false) { backgroundWorker14.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 14) { if (backgroundWorker15.IsBusy == false) { backgroundWorker15.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 15) { if (backgroundWorker16.IsBusy == false) { backgroundWorker16.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 16) { if (backgroundWorker17.IsBusy == false) { backgroundWorker17.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 17) { if (backgroundWorker18.IsBusy == false) { backgroundWorker18.RunWorkerAsync(); } }
        }

        private void hostMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status[dataGridView1.CurrentRow.Index] = "Host";
            IsVerified[dataGridView1.CurrentRow.Index] = true;
            SubMenu[dataGridView1.CurrentRow.Index] = "Main";
            MenuRunning = true;
            if (dataGridView1.CurrentRow.Index == 0) { if (backgroundWorker1.IsBusy == false) { backgroundWorker1.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 1) { if (backgroundWorker2.IsBusy == false) { backgroundWorker2.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 2) { if (backgroundWorker3.IsBusy == false) { backgroundWorker3.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 3) { if (backgroundWorker4.IsBusy == false) { backgroundWorker4.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 4) { if (backgroundWorker5.IsBusy == false) { backgroundWorker5.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 5) { if (backgroundWorker6.IsBusy == false) { backgroundWorker6.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 6) { if (backgroundWorker7.IsBusy == false) { backgroundWorker7.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 7) { if (backgroundWorker8.IsBusy == false) { backgroundWorker8.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 8) { if (backgroundWorker9.IsBusy == false) { backgroundWorker9.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 9) { if (backgroundWorker10.IsBusy == false) { backgroundWorker10.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 10) { if (backgroundWorker11.IsBusy == false) { backgroundWorker11.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 11) { if (backgroundWorker12.IsBusy == false) { backgroundWorker12.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 12) { if (backgroundWorker13.IsBusy == false) { backgroundWorker13.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 13) { if (backgroundWorker14.IsBusy == false) { backgroundWorker14.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 14) { if (backgroundWorker15.IsBusy == false) { backgroundWorker15.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 15) { if (backgroundWorker16.IsBusy == false) { backgroundWorker16.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 16) { if (backgroundWorker17.IsBusy == false) { backgroundWorker17.RunWorkerAsync(); } } else if (dataGridView1.CurrentRow.Index == 17) { if (backgroundWorker18.IsBusy == false) { backgroundWorker18.RunWorkerAsync(); } } }

        private void removeAccessToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ps3Memory.SetMemory((0x014E2224 + ((uint)dataGridView1.CurrentRow.Index * 0x3700)), new byte[] { 0x00, 0x00, 0x00 });
            Status[dataGridView1.CurrentRow.Index] = "Un-Verified";
            IsVerified[dataGridView1.CurrentRow.Index] = false;
            iPrintln(dataGridView1.CurrentRow.Index, "^1Access Removed Nigga!");
            iPrintlnBold(dataGridView1.CurrentRow.Index, "^1Access Removed Nigga!");
        }

        private void formSkin1_Click(object sender, EventArgs e)
        {

        }

        private void flatButton4_Click(object sender, EventArgs e)
        {
            DestroyElem(120);
            DestroyElem(121);
            DestroyElem(122);
            DestroyElem(123);
            DestroyElem(124);
            DestroyElem(125);
            DestroyElem(126);
            DestroyElem(127);
            flatButton4.Text = "Destroyed";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Advertise_Tick(object sender, EventArgs e)
        {
            Mw2Library.RPC.iPrintln(-1,Mw2Library.RPC.KeyBoard(client,"","",32));
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            SetText(122 + (client * 18), IsVerText(-1), 100, 100, -1, Mw2Library.RPC.KeyBoard(-1,"","",20), 99, 2.5, 1, 999, 255, 255, 255, 255, 0, 0, 0, 0);
            DoHeart1.Enabled = false;
            DoHeart2.Enabled = true;
        }

        private void DoHeart2_Tick(object sender, EventArgs e)
        {
            SetText(122 + (client * 18), IsVerText(-1), 100, 100, -1, Mw2Library.RPC.KeyBoard(-1, "", "", 20), 99, 2.5, 1, 999, 255, 255, 255, 255, 0, 0, 0, 0);
            DoHeart2.Enabled = false;
            DoHeart1.Enabled = true;
        }
        }
        }

    

