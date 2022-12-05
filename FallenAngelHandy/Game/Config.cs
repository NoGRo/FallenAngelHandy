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

        public string GalleryUseVariant { get; set; } = "detailed";

        public bool useJoystick { get; set; } = true;
        public bool UnlockFull { get; set; } = false;

        //Attack
        public int AttackSpeed { get; set; } = 350;
        public int HitSpeed { get; set; } = 450;

        public int LaserSpeedMin { get; set; } = 220;
        public int LaserSpeedMax { get; set; } = 220;
        public int LaserLength { get; set; } = 26;


        public bool Attacks { get; set; } = true;
        public bool SexScenes { get; set; } = true;
        public bool Filler { get; set; } = true;

        public bool Invincibility { get; set; } = false;
        public bool ForceFucking { get; set; } = false;

        private static string path = userDataPath + "LauncherConfig.json";

        public static void Load() 
        {
            try
            {
                if (!File.Exists(path))
                    Save();

                string json = File.ReadAllText(path);
                Game.Config = JsonSerializer.Deserialize<Config>(json);
            }
            catch 
            {

            }
        }
        public static void Save(bool updateControls = false) 
        {
            var pathConfig = userDataPath + "LauncherConfig.json";
            File.WriteAllText(pathConfig, JsonSerializer.Serialize(Game.Config,new JsonSerializerOptions { WriteIndented = true}));

            if (updateControls)
            {
                var pathMar = userDataPath + "controls.mar";
                if (!File.Exists(pathMar))
                {
                    if (!File.Exists("controls.mar"))
                        return;

                    File.Copy("controls.mar", pathMar,true);
                }


                string json = File.ReadAllText(pathMar);
                if(json.Substring(json.Length - 1, 1) == "\0")
                    json = json.Substring(0,json.Length - 1);
                var MarFile = JsonSerializer.Deserialize<ControlsConfig>(json);

           
                var controls = MarFile.Root[0];

                controls.invincibility = Game.Config.Invincibility ? 1.0 : 0.0;
                controls.force_fucking = Game.Config.ForceFucking ? 1.0 : 0.0;
                controls.zoomdefault =  Game.Config.ForceFucking ? "Far (automatic)" :controls.zoomdefault;
                if (updateControls)
                {
                    if (Game.Config.useJoystick)
                    {
                        controls.control_zoomminus = 189;
                        controls.control_zoomplus = 187;
                        controls.control_pause = 27;
                        controls.control_attack = 88;
                        controls.control_interact = 66;
                        controls.control_jump = 65;
                        controls.control_run = 84;
                        controls.control_left = 37;
                        controls.control_right = 39;
                        controls.control_up = 38;
                        controls.control_down = 40;
                    }
                    else
                    {
                        controls.control_zoomplus = 107;
                        controls.control_left = 37;
                        controls.control_attack = 83;
                        controls.control_pause = 27;
                        controls.control_interact = 68;
                        controls.control_jump = 32;
                        controls.control_run = 65;
                        controls.control_down = 40;
                        controls.control_zoomminus = 109;
                        controls.control_zoomreset = 106;
                        controls.control_right = 39;
                        controls.control_up = 38;
                    }
                }
                File.WriteAllText(pathMar, JsonSerializer.Serialize(MarFile));
            }
        }
    }
}

