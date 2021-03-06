
namespace FallenAngelHandy
{
    partial class FrmGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGame));
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtButtplugUrl = new System.Windows.Forms.TextBox();
            this.cmbScripts = new System.Windows.Forms.ComboBox();
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblGameStatus = new System.Windows.Forms.Label();
            this.chkFiller = new System.Windows.Forms.CheckBox();
            this.chkSexScenes = new System.Windows.Forms.CheckBox();
            this.chkAttack = new System.Windows.Forms.CheckBox();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnSimulate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbVibrator = new System.Windows.Forms.ComboBox();
            this.grpConnection.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(6, 20);
            this.lblUrl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(93, 15);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "Web Socket URL";
            // 
            // txtButtplugUrl
            // 
            this.txtButtplugUrl.Location = new System.Drawing.Point(105, 18);
            this.txtButtplugUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtButtplugUrl.Name = "txtButtplugUrl";
            this.txtButtplugUrl.Size = new System.Drawing.Size(204, 23);
            this.txtButtplugUrl.TabIndex = 1;
            this.txtButtplugUrl.Text = "ws://localhost:12345/buttplug";
            // 
            // cmbScripts
            // 
            this.cmbScripts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScripts.FormattingEnabled = true;
            this.cmbScripts.Items.AddRange(new object[] {
            "Default",
            "Slow"});
            this.cmbScripts.Location = new System.Drawing.Point(337, 14);
            this.cmbScripts.Margin = new System.Windows.Forms.Padding(2);
            this.cmbScripts.Name = "cmbScripts";
            this.cmbScripts.Size = new System.Drawing.Size(129, 23);
            this.cmbScripts.TabIndex = 10;
            this.cmbScripts.SelectedIndexChanged += new System.EventHandler(this.cmbScripts_SelectedIndexChanged);
            // 
            // grpConnection
            // 
            this.grpConnection.Controls.Add(this.lblStatus);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.txtButtplugUrl);
            this.grpConnection.Controls.Add(this.lblUrl);
            this.grpConnection.Location = new System.Drawing.Point(8, 7);
            this.grpConnection.Margin = new System.Windows.Forms.Padding(2);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Padding = new System.Windows.Forms.Padding(2);
            this.grpConnection.Size = new System.Drawing.Size(470, 65);
            this.grpConnection.TabIndex = 5;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Buttplug";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 42);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Status:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(316, 18);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(101, 20);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblGameStatus);
            this.groupBox3.Controls.Add(this.cmbVibrator);
            this.groupBox3.Controls.Add(this.cmbScripts);
            this.groupBox3.Controls.Add(this.chkFiller);
            this.groupBox3.Controls.Add(this.chkSexScenes);
            this.groupBox3.Controls.Add(this.chkAttack);
            this.groupBox3.Location = new System.Drawing.Point(8, 76);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(470, 86);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "React To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Gallery Variant:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblGameStatus
            // 
            this.lblGameStatus.AutoSize = true;
            this.lblGameStatus.Location = new System.Drawing.Point(359, 19);
            this.lblGameStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGameStatus.Name = "lblGameStatus";
            this.lblGameStatus.Size = new System.Drawing.Size(0, 15);
            this.lblGameStatus.TabIndex = 11;
            // 
            // chkFiller
            // 
            this.chkFiller.AutoSize = true;
            this.chkFiller.Checked = true;
            this.chkFiller.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFiller.Location = new System.Drawing.Point(169, 18);
            this.chkFiller.Margin = new System.Windows.Forms.Padding(2);
            this.chkFiller.Name = "chkFiller";
            this.chkFiller.Size = new System.Drawing.Size(51, 19);
            this.chkFiller.TabIndex = 6;
            this.chkFiller.Text = "Filler";
            this.chkFiller.UseVisualStyleBackColor = true;
            this.chkFiller.CheckedChanged += new System.EventHandler(this.chkFiller_CheckedChanged);
            // 
            // chkSexScenes
            // 
            this.chkSexScenes.AutoSize = true;
            this.chkSexScenes.Checked = true;
            this.chkSexScenes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSexScenes.Location = new System.Drawing.Point(78, 18);
            this.chkSexScenes.Margin = new System.Windows.Forms.Padding(2);
            this.chkSexScenes.Name = "chkSexScenes";
            this.chkSexScenes.Size = new System.Drawing.Size(83, 19);
            this.chkSexScenes.TabIndex = 3;
            this.chkSexScenes.Text = "Sex Scenes";
            this.chkSexScenes.UseVisualStyleBackColor = true;
            this.chkSexScenes.CheckedChanged += new System.EventHandler(this.chkSexScenes_CheckedChanged);
            // 
            // chkAttack
            // 
            this.chkAttack.AutoSize = true;
            this.chkAttack.Checked = true;
            this.chkAttack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAttack.Location = new System.Drawing.Point(6, 18);
            this.chkAttack.Margin = new System.Windows.Forms.Padding(2);
            this.chkAttack.Name = "chkAttack";
            this.chkAttack.Size = new System.Drawing.Size(65, 19);
            this.chkAttack.TabIndex = 1;
            this.chkAttack.Text = "Attacks";
            this.chkAttack.UseVisualStyleBackColor = true;
            this.chkAttack.CheckedChanged += new System.EventHandler(this.chkAttack_CheckedChanged);
            // 
            // btnLaunch
            // 
            this.btnLaunch.Enabled = false;
            this.btnLaunch.Location = new System.Drawing.Point(536, 142);
            this.btnLaunch.Margin = new System.Windows.Forms.Padding(2);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(78, 20);
            this.btnLaunch.TabIndex = 7;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnSimulate
            // 
            this.btnSimulate.Location = new System.Drawing.Point(496, 118);
            this.btnSimulate.Margin = new System.Windows.Forms.Padding(2);
            this.btnSimulate.Name = "btnSimulate";
            this.btnSimulate.Size = new System.Drawing.Size(118, 20);
            this.btnSimulate.TabIndex = 8;
            this.btnSimulate.Text = "Simulate Game";
            this.btnSimulate.UseVisualStyleBackColor = true;
            this.btnSimulate.Click += new System.EventHandler(this.btnSimulate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::FallenAngelHandy.Properties.Resource.Marielle;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(496, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(8, 183);
            this.txtLog.Margin = new System.Windows.Forms.Padding(2);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(649, 193);
            this.txtLog.TabIndex = 14;
            this.txtLog.Text = "";
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            this.txtLog.DoubleClick += new System.EventHandler(this.txtLog_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(246, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Vibrator Mode:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // cmbVibrator
            // 
            this.cmbVibrator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVibrator.FormattingEnabled = true;
            this.cmbVibrator.Items.AddRange(new object[] {
            "Speed",
            "Position"});
            this.cmbVibrator.Location = new System.Drawing.Point(337, 44);
            this.cmbVibrator.Margin = new System.Windows.Forms.Padding(2);
            this.cmbVibrator.Name = "cmbVibrator";
            this.cmbVibrator.Size = new System.Drawing.Size(129, 23);
            this.cmbVibrator.TabIndex = 10;
            this.cmbVibrator.SelectedIndexChanged += new System.EventHandler(this.cmbVibrator_SelectedIndexChanged);
            // 
            // FrmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 291);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnSimulate);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmGame";
            this.Text = "Fallen Angel Buttplug Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtButtplugUrl;
        private System.Windows.Forms.GroupBox grpConnection;
        public System.Windows.Forms.ComboBox cmbScripts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSexScenes;
        private System.Windows.Forms.CheckBox chkAttack;
        private System.Windows.Forms.CheckBox chkFiller;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnSimulate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblGameStatus;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbVibrator;
    }
}

