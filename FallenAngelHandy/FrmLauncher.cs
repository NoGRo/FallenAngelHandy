
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
namespace FallenAngelHandy
{
    public partial class FrmLauncher : Form
    {
        Timer timerGallery = new Timer(14000);
        Timer timerGalleryKeys = new Timer(1000);
        List<string> keys = new List<string>();
        GameSimulation gameSimulation = new GameSimulation();

        public FrmLauncher()
        {
            timerGallery.Elapsed += TimerGallery_Elapsed;
            timerGalleryKeys.Elapsed += TimerGalleryActive_Elapsed;
 
            InitializeComponent();
        }

        private void TimerGalleryActive_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!keys.Any())
                return;

            SendKeys.SendWait(keys.First());
            keys.RemoveAt(0);
        }

        private void TimerGallery_Elapsed(object sender, ElapsedEventArgs e)
        {
            keys =  new List<string> { "x","s","a","a" };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillerPlayer.Play();
        }

        
        private void button7_Click(object sender, EventArgs e)
        {
            Launcher.Config.ButtplugUrl =  txtButtplugUrl.Text;
            ButtplugService.init();
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            ButtplugService.init();
            GameListener.Init();
            GameListener.GameEventArrive += GameListener_GameEventArrive;
            ButtplugService.StatusChange += ButtplugService_StatusChange;
            Player.StatusChange += Player_StatusChange;
            cmbFiller.SelectedIndex = 0;
            cmbFiller.Enabled = false;
            Player.Init();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            timerGallery.Start();
            timerGalleryKeys.Start();
        }


        private void chkAttack_CheckedChanged(object sender, EventArgs e) 
            => Launcher.Config.Attacks = chkAttack.Checked;

        private void chkSexScenes_CheckedChanged(object sender, EventArgs e) 
            => Launcher.Config.SexScenes = chkSexScenes.Checked;

        private void chkFiller_CheckedChanged(object sender, EventArgs e) 
            => Launcher.Config.Filler = chkFiller.Checked;

        private void txtLog_DoubleClick(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            Process.Start("Fallen Angel Marielle.exe");
        }
    }
}
