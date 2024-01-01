﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MGS2_MC.CommonObjects;

namespace MGS2_MC
{
    internal class MGS2Constants
    {
        /*useful information from @ANTIBigBoss irt GoG port CT:
         * 
______________________________
Values to make some cheats work
----------------------------------------------------
Regular cheats
  - Walk through walls: 39 = on, 36=off
  - Enable Stealth: 64 = invisible :not from cameras ='(]
 
Weapon values
 - If you want to disable weapons: set value to -1 or 65535
 - this also means for items.
 - Whitesnoop ~
Alert mode = Put 1
Evasion = just lock for infinite evation
Caution = 5000 or whatever number to get Caution
------
Knockout (normal) takes 9 punches
         * 
         * !!!!!!!!!!! Unfortunately, it seems like the offsets in the GoG version are NOT translating to the MC version :( !!!!!!!
         * (Statics we can manipulate) = (type) -- (original offset in hex == original offset in decimal)
         * 
         * ------------
         * "Main" stuff
         * ------------
         * MaxLife = 2 bytes
         * AmmoInClip = 4 bytes -- 618B2C == 6392620
         * FPV State = array bytes -- 9C18C == 639372
         * FPV = byte -- 618B03 == 6392579
         * CurrentItem = 2 bytes -- D8AEC6 == 14200518
         * CurrentLevel = 7 char string -- D8ADEC == 14200300
         * CurrentWeapon = 2 bytes -- D8AEC4 == 14200516
         * Life = 2 bytes -- 618BD0 == 6392784
         * LifeText = 15 char string
         * End if found = 1 byte
         * Walk through walls = 1 byte
         * Walk through walls(soft) = 1 byte
         * Set character at load = 7 char string -- D8C374 == 14205812
         * VR end(34)?? = 4 bytes 
         * Set start point ROT = 2 bytes -- D8FE28 == 14220840
         * Set start point X = 2 bytes -- D8FE30 == 14220848
         * Set start point Y = 2 bytes -- D8FE34 == 14220852
         * Set start point Z = 2 bytes -- D8FE38 == 14220856
         * Load level = 16 char string -- D8C384 == 14205828
         * Previous level = 7 char string -- 664FC4 == 6705092
         * Difficulty = 1 byte -- D8C368 == 14205800
         * Difficulty(RO) = 1 byte -- D8ADD0 == 14200272
         * Screen visible(codec eg?) = 1 byte -- 9B7044 == 10186820
         * X(?) = float -- 6164E0 == 6382816
         * Y(?) = float -- 6164E4 == 6382820
         * Z(?) = float -- 6164E8 == 6382824
         * LocationX(?) = byte? -- 4A9910 == 4888848
         * LocationY(?) = 2 bytes -- 4A990B == 4888843
         * 
         * ----------------
         * VR mission stuff
         * ----------------
         * Bomb disposal = 2 bytes -- B60CFC == 11930876
         * Enemies = 2 bytes -- B6DE20 == 11984416
         * Score = 4 bytes -- B60C20 == 11930656
         * Targets = 2 bytes -- B60C04 == 11930628
         * Time = 2 bytes -- B60CF8 == 11930872
         * 
         * ----------------
         * Alert mode stuff
         * ----------------
         * Caution = 4 bytes -- 6160C8 == 6381768
         * Current Mode = 1 byte -- D8AEDA == 14200538
         * 
         * ------------
         * Random stuff
         * ------------
         * Character luminance = float -- 5FE990 == 6285712
         * Flip screen = float -- 5FE2F4 == 6284020
         * Hud horizontal placement = float -- 5FE610 == 6284816
         * Hud vertical placement = float -- 5FE614 == 6284820
         * Function starter? = 4 bytes -- B60A0C == 11930124
         * FOV = float -- 5FE1C8 == 6283720
         * Horizontal Stretch = float -- 5FE1B4 == 6283700
         * Vertical stretch = float -- 5FE1A0 == 6283680
         * Screen Y pos = float -- 5FD704 == 6280964
         */


        /* 12/10: Interesting CE learnings -
         * 
         * Certain game stats are tracked GLOBALLY and reset on launch(kill count, shot count, holdups, choke outs, prolly more)
         * It looks like the memory accesses these counts AT LEAST once on game load, once on screen load and twice on gameplay
         * 
         * 
         * 
         * 12/23: More CE investigations - 
         * (all of these findings are based off of a NG, Hard file. need to confirm it is consistent across difficulties/NG state
         * Pullup count is stored -90 bytes from dynamic anchor
         * SnakeHasCold is stored -128 bytes: cannot be changed after snake has already used cold meds, can change before
         * Currently equipped weapon is -68 bytes
         * Currently equipped item is -130 bytes
         * Something related to sneezing is at -108(2bytes). Whenever snake sneezes, it changes to 2D 00, then changes to another value. maybe time for next sneeze?
         * -154 and -146 seem to be related to player position? unsure.
         * -134 seems to represent current stance(00 is standing, 01 is crouching, 02 is prone: not quite sure how to leverage these yet)
         * 
         * Things that might be interesting to find/mess around with:
         * 
         * - Can we change max health? On hard its set to 75, but i can't see _what_ is changing it
         * - Can we manually modify boss health?
         * - Are there any AOBs we can use to find static strings/values?
         */

        #region True Constants
        public const string PROCESS_NAME = "METAL GEAR SOLID2";
        #endregion

        #region Item Table
        public const int RationOffset = 0;
        public const int SnakeScopeOffset = 2;
        public const int ColdMedicineOffset = 4;
        public const int BandageOffset = 6;
        public const int PentazeminOffset = 8;
        public const int BDUOffset = 10;
        public const int BodyArmorOffset = 12;
        public const int StealthOffset = 14;
        public const int MineDetectorOffset = 16;
        public const int SensorAOffset = 18;
        public const int SensorBOffset = 20;
        public const int NightVisionGogglesOffset = 22;
        public const int ThermalGogglesOffset = 24;
        public const int RaidenScopeOffset = 26;
        public const int DigitalCameraOffset = 28;
        public const int Box1Offset = 30;
        public const int CigarettesOffset = 32;
        public const int CardOffset = 34;
        public const int ShaverOffset = 36;
        public const int PhoneOffset = 38;
        public const int Camera1Offset = 40;
        public const int Box2Offset = 42;
        public const int Box3Offset = 44;
        public const int WetBoxOffset = 46;
        public const int APSensorOffset = 48;
        public const int Box4Offset = 50;
        public const int Box5Offset = 52;
        public const int UnknownItemOffset = 54; //razor?
        public const int SocomSuppressorOffset = 56;
        public const int AKSuppressorOffset = 58;
        public const int Camera2Offset = 60;
        public const int BandanaOffset = 62;
        public const int DogTagsOffset = 64;
        public const int MODiscOffset = 66;
        public const int USPSuppressorOffset = 68;
        public const int InfinityWigOffset = 70;
        public const int BlueWigOffset = 72;
        public const int OrangeWigOffset = 74;
        public const int ColorWigOffset = 76;
        public const int ColorWig2Offset = 78;
        #endregion

        #region Weapon Table
        public const int M9Offset = 0;
        public const int USPOffset = 2;
        public const int SOCOMOffset = 4;
        public const int PSG1Offset = 6;
        public const int RGB6Offset = 8;
        public const int NikitaOffset = 10;
        public const int StingerOffset = 12;
        public const int ClaymoreOffset = 14;
        public const int C4Offset = 16;
        public const int ChaffGrenadeOffset = 18;
        public const int StunGrenadeOffset = 20;
        public const int DMicOffset = 22;
        public const int HighFrequencyBladeOffset = 24;
        public const int CoolantOffset = 26;
        public const int AKS74uOffset = 28;
        public const int MagazineOffset = 30;
        public const int GrenadeOffset = 32;
        public const int M4Offset = 34;
        public const int PSG1TOffset = 36;
        public const int DMic2Offset = 38;
        public const int BookOffset = 40;
        #endregion
    };
}
