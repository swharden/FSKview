namespace FSKview
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.nudBrightness = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cbColormap = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbWindow = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDialFreq = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnConfigure = new System.Windows.Forms.Button();
            this.cbWspr = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pbTimeFrac = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatusTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerUpdateSpectrogram = new System.Windows.Forms.Timer(this.components);
            this.cbSave = new System.Windows.Forms.CheckBox();
            this.timerWsprUpdate = new System.Windows.Forms.Timer(this.components);
            this.cbBands = new System.Windows.Forms.CheckBox();
            this.audioControl1 = new FSKview.AudioControl();
            this.cbFTP = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudBrightness
            // 
            this.nudBrightness.DecimalPlaces = 1;
            this.nudBrightness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudBrightness.Location = new System.Drawing.Point(614, 26);
            this.nudBrightness.Name = "nudBrightness";
            this.nudBrightness.Size = new System.Drawing.Size(53, 20);
            this.nudBrightness.TabIndex = 5;
            this.nudBrightness.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudBrightness.ValueChanged += new System.EventHandler(this.nudBrightness_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(611, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Brightness";
            // 
            // cbColormap
            // 
            this.cbColormap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColormap.FormattingEnabled = true;
            this.cbColormap.Location = new System.Drawing.Point(300, 25);
            this.cbColormap.Name = "cbColormap";
            this.cbColormap.Size = new System.Drawing.Size(121, 21);
            this.cbColormap.TabIndex = 7;
            this.cbColormap.SelectedIndexChanged += new System.EventHandler(this.cbColormap_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Colormap";
            // 
            // cbWindow
            // 
            this.cbWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWindow.FormattingEnabled = true;
            this.cbWindow.Location = new System.Drawing.Point(173, 25);
            this.cbWindow.Name = "cbWindow";
            this.cbWindow.Size = new System.Drawing.Size(121, 21);
            this.cbWindow.TabIndex = 9;
            this.cbWindow.SelectedIndexChanged += new System.EventHandler(this.cbWindow_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Window";
            // 
            // cbDialFreq
            // 
            this.cbDialFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDialFreq.FormattingEnabled = true;
            this.cbDialFreq.Location = new System.Drawing.Point(427, 25);
            this.cbDialFreq.Name = "cbDialFreq";
            this.cbDialFreq.Size = new System.Drawing.Size(181, 21);
            this.cbDialFreq.TabIndex = 11;
            this.cbDialFreq.SelectedIndexChanged += new System.EventHandler(this.cbDialFreq_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Dial Frequency";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(899, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 26);
            this.label5.TabIndex = 13;
            this.label5.Text = "FSKview";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblTime.Location = new System.Drawing.Point(899, 30);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(100, 26);
            this.lblTime.TabIndex = 14;
            this.lblTime.Text = "23:17:00 UTC";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnConfigure
            // 
            this.btnConfigure.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfigure.Location = new System.Drawing.Point(810, 12);
            this.btnConfigure.Name = "btnConfigure";
            this.btnConfigure.Size = new System.Drawing.Size(86, 32);
            this.btnConfigure.TabIndex = 15;
            this.btnConfigure.Text = "Settings";
            this.btnConfigure.UseVisualStyleBackColor = true;
            this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // cbWspr
            // 
            this.cbWspr.AutoSize = true;
            this.cbWspr.Checked = true;
            this.cbWspr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWspr.Location = new System.Drawing.Point(745, 6);
            this.cbWspr.Name = "cbWspr";
            this.cbWspr.Size = new System.Drawing.Size(59, 17);
            this.cbWspr.TabIndex = 17;
            this.cbWspr.Text = "WSPR";
            this.cbWspr.UseVisualStyleBackColor = true;
            this.cbWspr.CheckedChanged += new System.EventHandler(this.cbWspr_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(999, 413);
            this.panel1.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Purple;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(630, 313);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbTimeFrac,
            this.lblStatusTime,
            this.lblStatus,
            this.lblVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 466);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(999, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pbTimeFrac
            // 
            this.pbTimeFrac.Maximum = 600;
            this.pbTimeFrac.Name = "pbTimeFrac";
            this.pbTimeFrac.Size = new System.Drawing.Size(100, 16);
            this.pbTimeFrac.Value = 123;
            // 
            // lblStatusTime
            // 
            this.lblStatusTime.Name = "lblStatusTime";
            this.lblStatusTime.Size = new System.Drawing.Size(31, 17);
            this.lblStatusTime.Text = "time";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(806, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "status";
            // 
            // lblVersion
            // 
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 17);
            this.lblVersion.Text = "version";
            // 
            // timerUpdateSpectrogram
            // 
            this.timerUpdateSpectrogram.Enabled = true;
            this.timerUpdateSpectrogram.Interval = 1000;
            this.timerUpdateSpectrogram.Tick += new System.EventHandler(this.timerUpdateSpectrogram_Tick);
            // 
            // cbSave
            // 
            this.cbSave.AutoSize = true;
            this.cbSave.Checked = true;
            this.cbSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSave.Location = new System.Drawing.Point(673, 27);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(71, 17);
            this.cbSave.TabIndex = 21;
            this.cbSave.Text = "Autosave";
            this.cbSave.UseVisualStyleBackColor = true;
            this.cbSave.CheckedChanged += new System.EventHandler(this.cbSave_CheckedChanged);
            // 
            // timerWsprUpdate
            // 
            this.timerWsprUpdate.Enabled = true;
            this.timerWsprUpdate.Interval = 500;
            this.timerWsprUpdate.Tick += new System.EventHandler(this.timerWsprUpdate_Tick);
            // 
            // cbBands
            // 
            this.cbBands.AutoSize = true;
            this.cbBands.Checked = true;
            this.cbBands.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBands.Location = new System.Drawing.Point(673, 6);
            this.cbBands.Name = "cbBands";
            this.cbBands.Size = new System.Drawing.Size(56, 17);
            this.cbBands.TabIndex = 23;
            this.cbBands.Text = "Bands";
            this.cbBands.UseVisualStyleBackColor = true;
            this.cbBands.CheckedChanged += new System.EventHandler(this.cbBands_CheckedChanged);
            // 
            // audioControl1
            // 
            this.audioControl1.Location = new System.Drawing.Point(9, 9);
            this.audioControl1.Margin = new System.Windows.Forms.Padding(0);
            this.audioControl1.Name = "audioControl1";
            this.audioControl1.SampleRate = 6000;
            this.audioControl1.Size = new System.Drawing.Size(161, 41);
            this.audioControl1.TabIndex = 3;
            this.audioControl1.InputDeviceChanged += new System.EventHandler(this.audioControl1_InputDeviceChanged);
            // 
            // cbFTP
            // 
            this.cbFTP.AutoSize = true;
            this.cbFTP.Location = new System.Drawing.Point(745, 27);
            this.cbFTP.Name = "cbFTP";
            this.cbFTP.Size = new System.Drawing.Size(46, 17);
            this.cbFTP.TabIndex = 24;
            this.cbFTP.Text = "FTP";
            this.cbFTP.UseVisualStyleBackColor = true;
            this.cbFTP.CheckedChanged += new System.EventHandler(this.cbFTP_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 488);
            this.Controls.Add(this.cbFTP);
            this.Controls.Add(this.cbSave);
            this.Controls.Add(this.cbBands);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbWspr);
            this.Controls.Add(this.btnConfigure);
            this.Controls.Add(this.cbDialFreq);
            this.Controls.Add(this.cbWindow);
            this.Controls.Add(this.cbColormap);
            this.Controls.Add(this.nudBrightness);
            this.Controls.Add(this.audioControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FSKview";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private AudioControl audioControl1;
        private System.Windows.Forms.NumericUpDown nudBrightness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbColormap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbWindow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbDialFreq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnConfigure;
        private System.Windows.Forms.CheckBox cbWspr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Timer timerUpdateSpectrogram;
        private System.Windows.Forms.CheckBox cbSave;
        private System.Windows.Forms.Timer timerWsprUpdate;
        private System.Windows.Forms.CheckBox cbBands;
        private System.Windows.Forms.ToolStripStatusLabel lblVersion;
        private System.Windows.Forms.ToolStripProgressBar pbTimeFrac;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusTime;
        private System.Windows.Forms.CheckBox cbFTP;
    }
}

