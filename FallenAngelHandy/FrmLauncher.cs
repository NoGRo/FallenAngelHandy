
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
            ButtplugService.init();
            GameListener.Init();
            GalleryRepository.Init();
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

        private void chkAttack_CheckedChanged(object sender, EventArgs e) 
            => Game.Config.Attacks = chkAttack.Checked;

        private void chkSexScenes_CheckedChanged(object sender, EventArgs e) 
            => Game.Config.SexScenes = chkSexScenes.Checked;

        private void chkFiller_CheckedChanged(object sender, EventArgs e) 
            => Game.Config.Filler = chkFiller.Checked;

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
    }
}
