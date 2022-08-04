using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FallenAngelHandy.Core;

namespace FallenAngelHandy
{
    public partial class GameSimulation : Form
    {
        private Timer timerStats = new Timer();
        public GameSimulation()
        {
            InitializeComponent();
            timerStats.Interval = 30000;
            timerStats.Tick += TimerStats_Tick;
        }

        private void trkLust_ValueChanged(object sender, EventArgs e) 
            => Game.Status.Pleasure = trkLust.Value;

        private void trkPain_ValueChanged(object sender, EventArgs e) 
            => Game.Status.Pain = trkPain.Value;

        private void trkHead_ValueChanged(object sender, EventArgs e) 
            => Game.Status.Head = trkHead.Value;

        private void trkBreast_ValueChanged(object sender, EventArgs e) 
            => Game.Status.Breasts = trkBreast.Value;

        private void trkPenis_ValueChanged(object sender, EventArgs e) 
            => Game.Status.Penis = trkPenis.Value;

        private void trkPussy_ValueChanged(object sender, EventArgs e) 
            => Game.Status.Vagina = trkPussy.Value;

        private void trkAss_ValueChanged(object sender, EventArgs e) 
            => Game.Status.Anus = trkAss.Value;

        private void btnRandom_Click(object sender, EventArgs e)
        {

            timerStats.Start();
            var rnd = new Random();
            trkLust.Value = rnd.Next(0, 100);
            trkPain.Value = rnd.Next(0, 100);
            trkHead.Value = rnd.Next(0, 100);
            trkBreast.Value = rnd.Next(0, 100);
            trkPenis.Value = rnd.Next(0, 100);
            trkPussy.Value = rnd.Next(0, 100);
            trkAss.Value = rnd.Next(0, 100);
        }

        private void TimerStats_Tick(object sender, EventArgs e)
        {
            btnRandom_Click(sender,e);
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            
            Player.GameEventHandler("hit_pain", new NameValueCollection
            {
                { "strength", new Random().Next(2, 16).ToString() }
            });
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            var gallery = cmbGallery.SelectedItem.ToString();
            if (gallery == "Random")
            {
                var gallerys = GalleryRepository.GetNames();
                gallery = gallerys[new Random().Next(0, gallerys.Count)];
            }
            PlayerScript.GameEventHandler("gallery", new NameValueCollection
            {
                { "code",gallery }
            });    
        }

        private void GameSimulation_Load(object sender, EventArgs e)
        {

            cmbGallery.Items.Add("Random");
            cmbGallery.SelectedIndex = 0;
            cmbGallery.Items.AddRange(GalleryRepository.GetNames().ToArray());
        }
    }
}

