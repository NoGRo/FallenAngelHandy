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
        //Core
        public string ButtplugUrl { get; set; } = "ws://localhost:12345/buttplug";
        public string ListenerHost { get; set; } = "http://127.0.0.1:5050/game/";
        public string HandyKey { get; set; } = "";
        public string ExePath { get; set; } = "Fallen Angel Marielle.exe";
        public string GalleryPath { get; set; } = @"Gallery";


        public readonly string UserDataPath = userDataPath;
        private static string userDataPath = $"{Environment.GetEnvironmentVariable("LocalAppData")}\\Fallen_Angel\\";

        public string GalleryUseVariant { get; set; } = null;


        //Filler            
        public int MinSpeed { get; set; } = 60;
        public int MaxSpeed { get; set; } = 150;
        public int MinLength { get; set; } = 70;
        public int MaxLength { get; set; } = 90;

        public int MinDamage { get; set; } = 15;
        public int CriticalDamage { get; set; } = 70;
        public int ExtremeDamage { get; set; } = 90;
        public int CriticalSpeed { get; set; } = 190;

        public int Delay { get; set; } = 900;

        //Attack
        public int AttackSpeed { get; set; } = 350;
        public int HitSpeed { get; set; } = 450;

        public int LaserSpeedMin { get; set; } = 220;
        public int LaserSpeedMax { get; set; } = 220;
        public int LaserLength { get; set; } = 26;


        public bool Attacks { get; set; } = true;
        public bool SexScenes { get; set; } = true;
        public bool Filler { get; set; } = true;
        public bool Invincibility { get; set; } = true;
        public bool ForceFucking { get; set; } = true;



        private static string path = userDataPath + "LauncherConfig.json";

        public static void Load() 
        {
            try
            {
                if (!File.Exists(path))
                    Save();
                
                Game.Config = JsonSerializer.Deserialize<Config>(File.ReadAllText(path));
            }
            catch 
            {

            }
        }
        public static void Save() {
            try
            {
                File.WriteAllText(path, JsonSerializer.Serialize(Game.Config,new JsonSerializerOptions { WriteIndented = true}));
                
                var pathMar = userDataPath + "controls.mar";
                var controls = JsonSerializer.Deserialize<dynamic>(File.ReadAllText(pathMar));
                
                controls.Root[0].invicibily = Game.Config.Invincibility ? 1.0 : 0.0;
                controls.Root[0].force_fucking = Game.Config.ForceFucking ? 1.0 : 0.0;

                File.WriteAllText(path, JsonSerializer.Serialize(controls));

            }
            catch { 
            }
        }

    }
}

