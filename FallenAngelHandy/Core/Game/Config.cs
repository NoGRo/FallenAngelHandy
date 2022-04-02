using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace FallenAngelHandy
{
    public class Config
    {
        public string ButtplugUrl { get; set; } = "ws://localhost:12345/buttplug";
        public string ExePath { get; set; } = "Fallen Angel Marielle.exe";
            
        public int MinSpeed { get; set; } = 50;
        public int MaxSpeed { get; set; } = 100;
        public int MinLength { get; set; } = 70;
        public int MaxLength { get; set; } = 100;
        public int CriticalSpeed { get; set; } = 190;
        public int MinDamage { get; set; } = 15;
        public int CriticalDamage { get; set; } = 70;
        public int Delay { get; set; } = 900;


        public int AttackSpeed { get; set; } = 350;
        public int HitSpeed { get; set; } = 450;

        public int LaserSpeedMin { get; set; } = 220;
        public int LaserSpeedMax { get; set; } = 220;
        public int LaserLength { get; set; } = 26;


        public bool Attacks { get; set; } = true;
        public bool SexScenes { get; set; } = true;
        public bool SexScenesTimeSkip { get; set; } = true;
        public bool Filler { get; set; } = true;
        
    }
}

