namespace FSKview
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUploadNow = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbRemotePath = new System.Windows.Forms.TextBox();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.nudFreqDisplayOffset = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.tbImageFileName = new System.Windows.Forms.TextBox();
            this.btnSetWsprPath = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tbWsprPath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.nudTargetWidth = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.nudPxAbove = new System.Windows.Forms.NumericUpDown();
            this.cbShowFreqScale = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.nudPxBelow = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStationInfo = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cbRoll = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.nudVerticalReduction = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAgcMode = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqDisplayOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTargetWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPxAbove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPxBelow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalReduction)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUploadNow);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbPassword);
            this.groupBox2.Controls.Add(this.tbUsername);
            this.groupBox2.Controls.Add(this.tbRemotePath);
            this.groupBox2.Controls.Add(this.tbServer);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 275);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FTP Settings";
            // 
            // btnUploadNow
            // 
            this.btnUploadNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUploadNow.Location = new System.Drawing.Point(120, 246);
            this.btnUploadNow.Name = "btnUploadNow";
            this.btnUploadNow.Size = new System.Drawing.Size(75, 23);
            this.btnUploadNow.TabIndex = 28;
            this.btnUploadNow.Text = "Upload Now";
            this.btnUploadNow.UseVisualStyleBackColor = true;
            this.btnUploadNow.Click += new System.EventHandler(this.btnUploadNow_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(6, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 45);
            this.label1.TabIndex = 3;
            this.label1.Text = "stored as obfuscated (not encrypted) text in the settings file";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Remote Path";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Server";
            // 
            // tbPassword
            // 
            this.tbPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPassword.Location = new System.Drawing.Point(9, 188);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(181, 20);
            this.tbPassword.TabIndex = 27;
            // 
            // tbUsername
            // 
            this.tbUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUsername.Location = new System.Drawing.Point(9, 136);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(181, 20);
            this.tbUsername.TabIndex = 25;
            // 
            // tbRemotePath
            // 
            this.tbRemotePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRemotePath.Location = new System.Drawing.Point(9, 84);
            this.tbRemotePath.Name = "tbRemotePath";
            this.tbRemotePath.Size = new System.Drawing.Size(181, 20);
            this.tbRemotePath.TabIndex = 23;
            // 
            // tbServer
            // 
            this.tbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServer.Location = new System.Drawing.Point(9, 32);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(181, 20);
            this.tbServer.TabIndex = 21;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(753, 339);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 27);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(686, 339);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 27);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(637, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 48);
            this.label9.TabIndex = 33;
            this.label9.Text = "The frequency scale will be shifted verticaly by this amount";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(637, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Frequency Offset (Hz)";
            // 
            // nudFreqDisplayOffset
            // 
            this.nudFreqDisplayOffset.Location = new System.Drawing.Point(640, 96);
            this.nudFreqDisplayOffset.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudFreqDisplayOffset.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.nudFreqDisplayOffset.Name = "nudFreqDisplayOffset";
            this.nudFreqDisplayOffset.Size = new System.Drawing.Size(64, 20);
            this.nudFreqDisplayOffset.TabIndex = 32;
            this.nudFreqDisplayOffset.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(227, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Latest grab filename";
            // 
            // tbImageFileName
            // 
            this.tbImageFileName.Location = new System.Drawing.Point(230, 200);
            this.tbImageFileName.Name = "tbImageFileName";
            this.tbImageFileName.Size = new System.Drawing.Size(201, 20);
            this.tbImageFileName.TabIndex = 42;
            // 
            // btnSetWsprPath
            // 
            this.btnSetWsprPath.Location = new System.Drawing.Point(759, 40);
            this.btnSetWsprPath.Name = "btnSetWsprPath";
            this.btnSetWsprPath.Size = new System.Drawing.Size(55, 27);
            this.btnSetWsprPath.TabIndex = 30;
            this.btnSetWsprPath.Text = "Browse";
            this.btnSetWsprPath.UseVisualStyleBackColor = true;
            this.btnSetWsprPath.Click += new System.EventHandler(this.btnSetWsprPath_Click);
            // 
            // label12
            // 
            this.label12.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label12.Location = new System.Drawing.Point(227, 223);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(204, 63);
            this.label12.TabIndex = 44;
            this.label12.Text = "PNG, JPG, GIF, and BMP extensions are supported. PNG files are recommended (super" +
    "ior quality to JPG and GIF, smaller file size than BMP)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(227, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "WSPR Log File";
            // 
            // tbWsprPath
            // 
            this.tbWsprPath.Location = new System.Drawing.Point(230, 44);
            this.tbWsprPath.Name = "tbWsprPath";
            this.tbWsprPath.Size = new System.Drawing.Size(517, 20);
            this.tbWsprPath.TabIndex = 46;
            // 
            // label14
            // 
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label14.Location = new System.Drawing.Point(227, 119);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 58);
            this.label14.TabIndex = 48;
            this.label14.Text = "Approximate spectrogram width (not including scale)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(227, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 13);
            this.label15.TabIndex = 49;
            this.label15.Text = "Target Width";
            // 
            // nudTargetWidth
            // 
            this.nudTargetWidth.Location = new System.Drawing.Point(230, 96);
            this.nudTargetWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudTargetWidth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudTargetWidth.Name = "nudTargetWidth";
            this.nudTargetWidth.Size = new System.Drawing.Size(64, 20);
            this.nudTargetWidth.TabIndex = 47;
            this.nudTargetWidth.ThousandsSeparator = true;
            this.nudTargetWidth.Value = new decimal(new int[] {
            111,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(605, 187);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 13);
            this.label16.TabIndex = 50;
            this.label16.Text = "Px Above WSPR";
            // 
            // nudPxAbove
            // 
            this.nudPxAbove.Location = new System.Drawing.Point(608, 203);
            this.nudPxAbove.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudPxAbove.Name = "nudPxAbove";
            this.nudPxAbove.Size = new System.Drawing.Size(64, 20);
            this.nudPxAbove.TabIndex = 51;
            this.nudPxAbove.Value = new decimal(new int[] {
            111,
            0,
            0,
            0});
            // 
            // cbShowFreqScale
            // 
            this.cbShowFreqScale.AutoSize = true;
            this.cbShowFreqScale.Location = new System.Drawing.Point(469, 203);
            this.cbShowFreqScale.Name = "cbShowFreqScale";
            this.cbShowFreqScale.Size = new System.Drawing.Size(67, 17);
            this.cbShowFreqScale.TabIndex = 52;
            this.cbShowFreqScale.Text = "all saves";
            this.cbShowFreqScale.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(466, 187);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(87, 13);
            this.label17.TabIndex = 53;
            this.label17.Text = "Frequency Scale";
            // 
            // label18
            // 
            this.label18.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label18.Location = new System.Drawing.Point(466, 223);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(111, 63);
            this.label18.TabIndex = 54;
            this.label18.Text = "leave this off so images in \"grabs-all\" are suitable for stitching";
            // 
            // nudPxBelow
            // 
            this.nudPxBelow.Location = new System.Drawing.Point(717, 203);
            this.nudPxBelow.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudPxBelow.Name = "nudPxBelow";
            this.nudPxBelow.Size = new System.Drawing.Size(64, 20);
            this.nudPxBelow.TabIndex = 55;
            this.nudPxBelow.Value = new decimal(new int[] {
            111,
            0,
            0,
            0});
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(714, 187);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 13);
            this.label19.TabIndex = 56;
            this.label19.Text = "Px Below QRSS";
            // 
            // label20
            // 
            this.label20.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label20.Location = new System.Drawing.Point(605, 227);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(202, 58);
            this.label20.TabIndex = 57;
            this.label20.Text = "Values describe distance above and below band edges";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(346, 80);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 13);
            this.label21.TabIndex = 59;
            this.label21.Text = "Vertical Reduction";
            // 
            // label22
            // 
            this.label22.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label22.Location = new System.Drawing.Point(346, 119);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 60);
            this.label22.TabIndex = 60;
            this.label22.Text = "Shrinks output image vertically (applying a local max filter)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 300);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Station Information";
            // 
            // tbStationInfo
            // 
            this.tbStationInfo.Location = new System.Drawing.Point(21, 316);
            this.tbStationInfo.Name = "tbStationInfo";
            this.tbStationInfo.Size = new System.Drawing.Size(410, 20);
            this.tbStationInfo.TabIndex = 62;
            // 
            // label23
            // 
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(18, 352);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(378, 17);
            this.label23.TabIndex = 63;
            this.label23.Text = "To reset all settings, close FSKview and delete settings.xml";
            // 
            // cbRoll
            // 
            this.cbRoll.AutoSize = true;
            this.cbRoll.Checked = true;
            this.cbRoll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRoll.Location = new System.Drawing.Point(489, 96);
            this.cbRoll.Name = "cbRoll";
            this.cbRoll.Size = new System.Drawing.Size(44, 17);
            this.cbRoll.TabIndex = 64;
            this.cbRoll.Text = "Roll";
            this.cbRoll.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(486, 80);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 13);
            this.label24.TabIndex = 65;
            this.label24.Text = "Display Mode";
            // 
            // label25
            // 
            this.label25.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label25.Location = new System.Drawing.Point(486, 116);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(94, 60);
            this.label25.TabIndex = 66;
            this.label25.Text = "Roll is like \"wipe\". Off is like scroll.";
            // 
            // nudVerticalReduction
            // 
            this.nudVerticalReduction.Location = new System.Drawing.Point(349, 97);
            this.nudVerticalReduction.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudVerticalReduction.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVerticalReduction.Name = "nudVerticalReduction";
            this.nudVerticalReduction.Size = new System.Drawing.Size(64, 20);
            this.nudVerticalReduction.TabIndex = 67;
            this.nudVerticalReduction.ThousandsSeparator = true;
            this.nudVerticalReduction.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label10.Location = new System.Drawing.Point(466, 340);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 31);
            this.label10.TabIndex = 70;
            this.label10.Text = "Auto-adjust gain to achieve a stable noise floor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(465, 300);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Spectral AGC";
            // 
            // cbAgcMode
            // 
            this.cbAgcMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAgcMode.FormattingEnabled = true;
            this.cbAgcMode.Items.AddRange(new object[] {
            "AGC Off",
            "Windowed Noise Floor"});
            this.cbAgcMode.Location = new System.Drawing.Point(469, 316);
            this.cbAgcMode.Name = "cbAgcMode";
            this.cbAgcMode.Size = new System.Drawing.Size(135, 21);
            this.cbAgcMode.TabIndex = 71;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 378);
            this.Controls.Add(this.cbAgcMode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudVerticalReduction);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cbRoll);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.tbStationInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.nudPxBelow);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbShowFreqScale);
            this.Controls.Add(this.nudPxAbove);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.nudTargetWidth);
            this.Controls.Add(this.tbWsprPath);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbImageFileName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nudFreqDisplayOffset);
            this.Controls.Add(this.btnSetWsprPath);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FSKview Settings";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqDisplayOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTargetWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPxAbove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPxBelow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVerticalReduction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnUploadNow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbRemotePath;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudFreqDisplayOffset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbImageFileName;
        private System.Windows.Forms.Button btnSetWsprPath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbWsprPath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nudTargetWidth;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown nudPxAbove;
        private System.Windows.Forms.CheckBox cbShowFreqScale;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudPxBelow;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStationInfo;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox cbRoll;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown nudVerticalReduction;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbAgcMode;
    }
}