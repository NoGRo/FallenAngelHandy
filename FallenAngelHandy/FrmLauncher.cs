
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
            GalleryBuilder.Init();

            GameListener.GameEventArrive += GameListener_GameEventArrive;

            Player.Init();
            PlayerScript.Init();
            
            HandyService.init();

            HandyService.Connect(Game.Config.HandyKey);
            ButtplugService.init();
            ButtplugService.StatusChange += ButtplugService_StatusChange;
            Player.StatusChange += Player_StatusChange;
            Player.Init();
            loadForm();

            cmbFiller.SelectedIndex = 0;
            cmbFiller.Enabled = false;
            
        }
        private void loadForm()
        {
            chkFiller.Checked = Game.Config.Filler;
            chkSexScenes.Checked = Game.Config.SexScenes;
            chkAttack.Checked = Game.Config.Attacks;
            cmbScripts.Text = Game.Config.GalleryUseVariant;
            cmbVibrator.Text = Game.Config.VibratorMode;
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
            }));
        }

        private void Player_StatusChange(object sender, string e)
        {
            Invoke(new MethodInvoker(() =>
            {
                lblGameStatus.Text = e;
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

            File.WriteAllText(
                path: basepath + $"AAA_MasterCmds.txt",
                contents:
                string.Join("\r\n", finalFiles.Keys.Select(x => $"del AAA_{x}.mp4"))
                + "\r\n"+
                string.Join("\r\n", finalFiles.Keys.Select(x => $".\\ffmpeg.exe -f concat -i AAA_{x}.txt -c copy -bsf:a aac_adtstoasc AAA_{x}.mp4"))
            );
        }

        private void cmbScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Game.Config.GalleryUseVariant = cmbScripts.Text;
            GalleryRepository.Init();
            Config.Save();
        }
        private void cmbVibrator_SelectedIndexChanged(object sender, EventArgs e)
        {
            Game.Config.VibratorMode = cmbVibrator.Text;
            Config.Save();
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


    }
}
