
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
            this.cmbScripts = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkFiller = new System.Windows.Forms.CheckBox();
            this.chkSexScenes = new System.Windows.Forms.CheckBox();
            this.chkAttack = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnGenerateGallery = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.optInputJoystick = new System.Windows.Forms.RadioButton();
            this.optInputKeyboard = new System.Windows.Forms.RadioButton();
            this.chkForceFucking = new System.Windows.Forms.CheckBox();
            this.chkInvincibility = new System.Windows.Forms.CheckBox();
            this.DeviceSelector = new System.Windows.Forms.TabControl();
            this.Handy = new System.Windows.Forms.TabPage();
            this.lblHandyStatus = new System.Windows.Forms.Label();
            this.btnHandyConnect = new System.Windows.Forms.Button();
            this.txtHandyKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Buttplug = new System.Windows.Forms.TabPage();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtButtplugUrl = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.DeviceSelector.SuspendLayout();
            this.Handy.SuspendLayout();
            this.Buttplug.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbScripts
            // 
            this.cmbScripts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScripts.FormattingEnabled = true;
            this.cmbScripts.Items.AddRange(new object[] {
            "simple",
            "detailed",
            "vibrator"});
            this.cmbScripts.Location = new System.Drawing.Point(140, 137);
            this.cmbScripts.Margin = new System.Windows.Forms.Padding(2);
            this.cmbScripts.Name = "cmbScripts";
            this.cmbScripts.Size = new System.Drawing.Size(183, 33);
            this.cmbScripts.TabIndex = 10;
            this.cmbScripts.SelectedIndexChanged += new System.EventHandler(this.cmbScripts_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkFiller);
            this.groupBox3.Controls.Add(this.chkSexScenes);
            this.groupBox3.Controls.Add(this.chkAttack);
            this.groupBox3.Location = new System.Drawing.Point(14, 305);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(568, 78);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "React To";
            // 
            // chkFiller
            // 
            this.chkFiller.AutoSize = true;
            this.chkFiller.Checked = true;
            this.chkFiller.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFiller.Location = new System.Drawing.Point(241, 30);
            this.chkFiller.Margin = new System.Windows.Forms.Padding(2);
            this.chkFiller.Name = "chkFiller";
            this.chkFiller.Size = new System.Drawing.Size(74, 29);
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
            this.chkSexScenes.Location = new System.Drawing.Point(111, 30);
            this.chkSexScenes.Margin = new System.Windows.Forms.Padding(2);
            this.chkSexScenes.Name = "chkSexScenes";
            this.chkSexScenes.Size = new System.Drawing.Size(124, 29);
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
            this.chkAttack.Location = new System.Drawing.Point(9, 30);
            this.chkAttack.Margin = new System.Windows.Forms.Padding(2);
            this.chkAttack.Name = "chkAttack";
            this.chkAttack.Size = new System.Drawing.Size(96, 29);
            this.chkAttack.TabIndex = 1;
            this.chkAttack.Text = "Attacks";
            this.chkAttack.UseVisualStyleBackColor = true;
            this.chkAttack.CheckedChanged += new System.EventHandler(this.chkAttack_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 139);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "Gallery Variant:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnLaunch
            // 
            this.btnLaunch.Enabled = false;
            this.btnLaunch.Location = new System.Drawing.Point(653, 354);
            this.btnLaunch.Margin = new System.Windows.Forms.Padding(2);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(111, 32);
            this.btnLaunch.TabIndex = 7;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::FallenAngelHandy.Properties.Resource.Marielle;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(586, 9);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(181, 164);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(9, 440);
            this.txtLog.Margin = new System.Windows.Forms.Padding(2);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(819, 332);
            this.txtLog.TabIndex = 14;
            this.txtLog.Text = "";
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            this.txtLog.DoubleClick += new System.EventHandler(this.txtLog_DoubleClick);
            // 
            // btnGenerateGallery
            // 
            this.btnGenerateGallery.Location = new System.Drawing.Point(406, 133);
            this.btnGenerateGallery.Margin = new System.Windows.Forms.Padding(2);
            this.btnGenerateGallery.Name = "btnGenerateGallery";
            this.btnGenerateGallery.Size = new System.Drawing.Size(158, 34);
            this.btnGenerateGallery.TabIndex = 15;
            this.btnGenerateGallery.Text = "Export Gallery";
            this.btnGenerateGallery.UseVisualStyleBackColor = true;
            this.btnGenerateGallery.Click += new System.EventHandler(this.btnGenerateGallery_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.optInputJoystick);
            this.groupBox1.Controls.Add(this.optInputKeyboard);
            this.groupBox1.Controls.Add(this.btnGenerateGallery);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkForceFucking);
            this.groupBox1.Controls.Add(this.chkInvincibility);
            this.groupBox1.Controls.Add(this.cmbScripts);
            this.groupBox1.Location = new System.Drawing.Point(14, 128);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(568, 173);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mods";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 35);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 25);
            this.label4.TabIndex = 18;
            this.label4.Text = "Input Metod:";
            // 
            // optInputJoystick
            // 
            this.optInputJoystick.AutoSize = true;
            this.optInputJoystick.Checked = true;
            this.optInputJoystick.Location = new System.Drawing.Point(131, 32);
            this.optInputJoystick.Name = "optInputJoystick";
            this.optInputJoystick.Size = new System.Drawing.Size(93, 29);
            this.optInputJoystick.TabIndex = 17;
            this.optInputJoystick.TabStop = true;
            this.optInputJoystick.Text = "Joistick";
            this.optInputJoystick.UseVisualStyleBackColor = true;
            // 
            // optInputKeyboard
            // 
            this.optInputKeyboard.AutoSize = true;
            this.optInputKeyboard.Location = new System.Drawing.Point(230, 32);
            this.optInputKeyboard.Name = "optInputKeyboard";
            this.optInputKeyboard.Size = new System.Drawing.Size(107, 29);
            this.optInputKeyboard.TabIndex = 16;
            this.optInputKeyboard.Text = "Keyboad";
            this.optInputKeyboard.UseVisualStyleBackColor = true;
            this.optInputKeyboard.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // chkForceFucking
            // 
            this.chkForceFucking.AutoSize = true;
            this.chkForceFucking.Location = new System.Drawing.Point(8, 102);
            this.chkForceFucking.Margin = new System.Windows.Forms.Padding(2);
            this.chkForceFucking.Name = "chkForceFucking";
            this.chkForceFucking.Size = new System.Drawing.Size(128, 29);
            this.chkForceFucking.TabIndex = 3;
            this.chkForceFucking.Text = "Restric Skip";
            this.chkForceFucking.UseVisualStyleBackColor = true;
            this.chkForceFucking.CheckedChanged += new System.EventHandler(this.chkForceFucking_CheckedChanged);
            // 
            // chkInvincibility
            // 
            this.chkInvincibility.AutoSize = true;
            this.chkInvincibility.Checked = true;
            this.chkInvincibility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInvincibility.Location = new System.Drawing.Point(8, 69);
            this.chkInvincibility.Margin = new System.Windows.Forms.Padding(2);
            this.chkInvincibility.Name = "chkInvincibility";
            this.chkInvincibility.Size = new System.Drawing.Size(126, 29);
            this.chkInvincibility.TabIndex = 1;
            this.chkInvincibility.Text = "Invincibility";
            this.chkInvincibility.UseVisualStyleBackColor = true;
            this.chkInvincibility.CheckedChanged += new System.EventHandler(this.chkInvincibility_CheckedChanged);
            // 
            // DeviceSelector
            // 
            this.DeviceSelector.Controls.Add(this.Handy);
            this.DeviceSelector.Controls.Add(this.Buttplug);
            this.DeviceSelector.Location = new System.Drawing.Point(11, 3);
            this.DeviceSelector.Name = "DeviceSelector";
            this.DeviceSelector.SelectedIndex = 0;
            this.DeviceSelector.Size = new System.Drawing.Size(565, 120);
            this.DeviceSelector.TabIndex = 15;
            // 
            // Handy
            // 
            this.Handy.BackColor = System.Drawing.SystemColors.Control;
            this.Handy.Controls.Add(this.lblHandyStatus);
            this.Handy.Controls.Add(this.btnHandyConnect);
            this.Handy.Controls.Add(this.txtHandyKey);
            this.Handy.Controls.Add(this.label3);
            this.Handy.Location = new System.Drawing.Point(4, 34);
            this.Handy.Name = "Handy";
            this.Handy.Padding = new System.Windows.Forms.Padding(3);
            this.Handy.Size = new System.Drawing.Size(557, 82);
            this.Handy.TabIndex = 0;
            this.Handy.Text = "Handy";
            // 
            // lblHandyStatus
            // 
            this.lblHandyStatus.AutoSize = true;
            this.lblHandyStatus.Location = new System.Drawing.Point(6, 54);
            this.lblHandyStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHandyStatus.Name = "lblHandyStatus";
            this.lblHandyStatus.Size = new System.Drawing.Size(64, 25);
            this.lblHandyStatus.TabIndex = 16;
            this.lblHandyStatus.Text = "Status:";
            // 
            // btnHandyConnect
            // 
            this.btnHandyConnect.Location = new System.Drawing.Point(366, 10);
            this.btnHandyConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnHandyConnect.Name = "btnHandyConnect";
            this.btnHandyConnect.Size = new System.Drawing.Size(143, 32);
            this.btnHandyConnect.TabIndex = 15;
            this.btnHandyConnect.Text = "Connect";
            this.btnHandyConnect.UseVisualStyleBackColor = true;
            this.btnHandyConnect.Click += new System.EventHandler(this.btnHandyConnect_Click);
            // 
            // txtHandyKey
            // 
            this.txtHandyKey.Location = new System.Drawing.Point(106, 11);
            this.txtHandyKey.Margin = new System.Windows.Forms.Padding(2);
            this.txtHandyKey.Name = "txtHandyKey";
            this.txtHandyKey.Size = new System.Drawing.Size(256, 31);
            this.txtHandyKey.TabIndex = 14;
            this.txtHandyKey.TextChanged += new System.EventHandler(this.txtHandyKey_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Handy Key";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Buttplug
            // 
            this.Buttplug.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Buttplug.Controls.Add(this.lblStatus);
            this.Buttplug.Controls.Add(this.btnConnect);
            this.Buttplug.Controls.Add(this.txtButtplugUrl);
            this.Buttplug.Controls.Add(this.lblUrl);
            this.Buttplug.Location = new System.Drawing.Point(4, 34);
            this.Buttplug.Name = "Buttplug";
            this.Buttplug.Padding = new System.Windows.Forms.Padding(3);
            this.Buttplug.Size = new System.Drawing.Size(557, 82);
            this.Buttplug.TabIndex = 1;
            this.Buttplug.Text = "Buttplug.io";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(7, 54);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(64, 25);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(398, 12);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(144, 32);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click_1);
            // 
            // txtButtplugUrl
            // 
            this.txtButtplugUrl.Location = new System.Drawing.Point(148, 13);
            this.txtButtplugUrl.Margin = new System.Windows.Forms.Padding(2);
            this.txtButtplugUrl.Name = "txtButtplugUrl";
            this.txtButtplugUrl.Size = new System.Drawing.Size(247, 31);
            this.txtButtplugUrl.TabIndex = 11;
            this.txtButtplugUrl.Text = "ws://localhost:12345/buttplug";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(7, 15);
            this.lblUrl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(142, 25);
            this.lblUrl.TabIndex = 10;
            this.lblUrl.Text = "Web Socket URL";
            // 
            // FrmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 395);
            this.Controls.Add(this.DeviceSelector);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmGame";
            this.Text = "Fallen Angel Buttplug Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DeviceSelector.ResumeLayout(false);
            this.Handy.ResumeLayout(false);
            this.Handy.PerformLayout();
            this.Buttplug.ResumeLayout(false);
            this.Buttplug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ComboBox cmbScripts;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkSexScenes;
        private System.Windows.Forms.CheckBox chkAttack;
        private System.Windows.Forms.CheckBox chkFiller;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerateGallery;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkForceFucking;
        private System.Windows.Forms.CheckBox chkInvincibility;
        private System.Windows.Forms.TabControl DeviceSelector;
        private System.Windows.Forms.TabPage Handy;
        private System.Windows.Forms.TabPage Buttplug;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtButtplugUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnHandyConnect;
        private System.Windows.Forms.TextBox txtHandyKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHandyStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.RadioButton optInputKeyboard;
        private System.Windows.Forms.RadioButton optInputJoystick;
        private System.Windows.Forms.Label label4;
    }
}

