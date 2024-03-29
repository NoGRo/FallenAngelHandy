﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Timer = System.Timers.Timer;
using FallenAngelHandy.Core;
using System.IO;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using System.Text.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using CsvHelper;
using System.Globalization;

namespace FallenAngelHandy
{
    public partial class FrmGame : Form
    {

        List<string> keys = new List<string>();
        GameSimulation gameSimulation = new GameSimulation();

        public FrmGame()
        {

            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            Config.Load();
            GameListener.Init();
            //GalleryBuilder.Init();
            GalleryRepository.LoadGalleryFromFolder();
            GameListener.GameEventArrive += GameListener_GameEventArrive;

            Player.Init();
            PlayerScript.Init();
            
            HandyService.init();
            HandyService.Connect();
            ButtplugService.init();

            ButtplugService.StatusChange += ButtplugService_StatusChange;
            HandyService.StatusChange += HandyService_StatusChange;
            Player.Init();
            loadForm();
        }



        private void loadForm()
        {
            chkFiller.Checked = Game.Config.Filler;
            chkSexScenes.Checked = Game.Config.SexScenes;
            chkAttack.Checked = Game.Config.Attacks;
            chkInvincibility.Checked = Game.Config.Invincibility;
            chkForceFucking.Checked = Game.Config.ForceFucking;
            optInputJoystick.Checked = Game.Config.useJoystick;
            optInputKeyboard.Checked = !Game.Config.useJoystick;
            cmbScripts.Text = Game.Config.GalleryUseVariant;
            txtHandyKey.Text = Game.Config.HandyKey;
            btnRestore.Enabled = Game.Config.UnlockFull;
            btnUnlock.Enabled = !Game.Config.UnlockFull;

        }
        private void GameListener_GameEventArrive(object sender, string e)
        {
            txtLog.AppendText(e + Environment.NewLine);
        }

        private void ButtplugService_StatusChange(object sender, string e)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblStatus.Text = $"Status: {e}";
                btnLaunch.Enabled = ButtplugService.isReady;
                if ((e.Contains("Device Found") && ButtplugService.isReady)
                 || (e.Contains("Scanning For Devices") && txtHandyKey.Text == ""))
                {
                    DeviceSelector.SelectedTab = DeviceSelector.TabPages[1];
                }
                

            }));
        }

        private void HandyService_StatusChange(object sender, string e)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblHandyStatus.Text = $"Status: {e}";
                btnLaunch.Enabled = HandyService.isReady;
                if (HandyService.isReady && e.Contains("Uploading"))
                    DeviceSelector.SelectedTab = DeviceSelector.TabPages[0];
            }));
        }

        private void btnSimulate_Click(object sender, EventArgs e)
        {
            gameSimulation.Show();
        }

        private void chkAttack_CheckedChanged(object sender, EventArgs e)
        { 
            Game.Config.Attacks = chkAttack.Checked;
            Config.Save();
        }

        private void chkSexScenes_CheckedChanged(object sender, EventArgs e)
        {
            Game.Config.SexScenes = chkSexScenes.Checked;
            Config.Save();
        }

        private void chkFiller_CheckedChanged(object sender, EventArgs e)
        {
            Game.Config.Filler = chkFiller.Checked;
            Config.Save();
        }

        private void txtLog_DoubleClick(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            Process.Start(Game.Config.ExePath);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ButtplugService.Connect();
        }

        private void btnGenerateGallery_Click(object sender, EventArgs e)
        {
            string basepath = @"D:\Programacion\FallenAngelHandy\Fallen_Angel_eDIT\Gallery\";

            Dictionary<string, List<string>> finalFiles = ReadBundles(basepath);
            //PackBundle(basepath, finalFiles);
            // UnpackBundle("D:\\Programacion\\FallenAngelHandy\\Fallen_Angel_eDIT\\NewGalleries\\", finalFiles);
            //WriteCsvDefinition("D:\\Programacion\\FallenAngelHandy\\Fallen_Angel_eDIT\\NewGalleries\\", finalFiles);

            File.WriteAllText(
                path: basepath + $"AAA_MasterCmds.txt",
                contents:
                string.Join("\r\n", finalFiles.Keys.Select(x => $"del AAA_{x}.mp4"))
                + "\r\n" +
                string.Join("\r\n", finalFiles.Keys.Select(x => $".\\ffmpeg.exe -f concat -i AAA_{x}.txt -c copy -bsf:a aac_adtstoasc AAA_{x}.mp4"))
            );

        }
        private void WriteCsvDefinition(string basepath, Dictionary<string, List<string>> finalFiles) 
        {
            var definitions = new List<GalleryDefinition>();
            foreach (var key in finalFiles.Keys)
            {
                var bundle = JsonSerializer.Deserialize<FunScriptFile>(File.ReadAllText(basepath + $"AAA_{key}.funscript"));

                var index = 0;
                
                foreach (var gal in finalFiles[key])
                {
                    definitions.Add(new GalleryDefinition
                    {
                        StartTime= 14000 * index,
                        EndTime = 14000 * (index + 1) ,
                        FileName = key,
                        Name = gal,
                        Loop = true
                    });  
                    index++;
                };
            }
            using (var writer = new StreamWriter(@$"{basepath}\Definitions.csv"))
            {
                var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(definitions); // where values implements IEnumerable
            }
        }
        private   void UnpackBundle(string basepath, Dictionary<string, List<string>> finalFiles)
        {
            foreach (var key in finalFiles.Keys)
            {

               var bundle = JsonSerializer.Deserialize<FunScriptFile>(File.ReadAllText(basepath + $"AAA_{key}.funscript"));




                var index = 0;
                foreach (var gal in finalFiles[key])
                {
                    var funScriptFile = new FunScriptFile();
                    funScriptFile.actions = bundle.actions
                                            .Where(x => x.at <= (14000 * (index + 1)) && x.at > 14000 * index)
                                            .Select(x =>new FunScriptAction { 
                                                pos = x.pos, 
                                                at = x.at - (14000 * index)
                                            }).ToList();
                    funScriptFile.Save($"{basepath}/UnPack/{gal}.funscript");
                    index++;
                };
                

            }
        }

        private  Dictionary<string, List<string>> ReadBundles(string basepath)
        {
            var lines = File.ReadAllText(basepath + "enemies.txt").Split("\r\n").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim());

            var finalFiles = new Dictionary<string, List<string>>();

            var currSection = new List<string>();
            var allGalleries = GalleryRepository.GetNames();
            foreach (var line in lines)
            {
                if (line.StartsWith("#"))
                {
                    currSection = new List<string>();
                    finalFiles.Add(line.Replace("#", ""), currSection);
                    continue;
                }
                currSection.AddRange(allGalleries
                                        .Where(x => x.StartsWith(line)));

            }

            return finalFiles;
        }
        private  void PackBundle(string basepath, Dictionary<string, List<string>> finalFiles)
        {
            foreach (var key in finalFiles.Keys)
            {
                File.WriteAllText(
                    path: basepath + $"AAA_{key}.txt",
                    contents: string.Join("\r\n", finalFiles[key].Select(x => $"file '{x}.mp4'"))
                    );


                var sb = new ScriptBuilder();
                foreach (var gal in finalFiles[key])
                {
                    //gallery
                    //sb.AddGallery(gal);

                    //Marks
                    sb.AddCommandMillis(14000, 50);

                };
                new FunScriptFile(sb.Generate())
                    .Save(basepath + $"AAA_{key}.funscript");

            }
        }

        private void cmbScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Game.Config.GalleryUseVariant = cmbScripts.Text;
            Config.Save();
        }

        private void chkInvincibility_CheckedChanged(object sender, EventArgs e)
        {
            Game.Config.Invincibility = chkInvincibility.Checked;
            Config.Save();
        }

        private void txtHandyKey_TextChanged(object sender, EventArgs e)
        {
            Game.Config.HandyKey = txtHandyKey.Text;
            Config.Save();
        }

        private void chkForceFucking_CheckedChanged(object sender, EventArgs e)
        {
            Game.Config.ForceFucking = chkForceFucking.Checked;
            Config.Save();
        }

        private void btnHandyConnect_Click(object sender, EventArgs e)
        {
            HandyService.Connect();
        }

        private void btnConnect_Click_1(object sender, EventArgs e)
        {
            ButtplugService.Connect();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Game.Config.useJoystick = optInputJoystick.Checked;
            Config.Save(true);
        }


        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"{Game.Config.UserDataPath}\save.mar"))
                File.Copy($@"{Game.Config.UserDataPath}\Backup_save.mar", $@"{Game.Config.UserDataPath}\save.mar", true);
            
            Game.Config.UnlockFull = false;
            Config.Save();
            loadForm();
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if(File.Exists(@$"{Game.Config.UserDataPath}\save.mar"))
                File.Copy($@"{Game.Config.UserDataPath}\save.mar", $@"{Game.Config.UserDataPath}\Backup_save.mar",true);

            if (File.Exists(@".\save.mar"))
            {
                File.Copy(@".\save.mar", $@"{Game.Config.UserDataPath}\save.mar", true);
            }

            Game.Config.UnlockFull = true;
            Config.Save();
            loadForm();
        }

    }
}
