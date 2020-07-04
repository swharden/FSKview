﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSKview
{
    public partial class FormSettings : Form
    {
        readonly ProgramSettings settings;
        public FormSettings(ProgramSettings settings)
        {
            InitializeComponent();
            this.settings = settings;

            tbServer.Text = settings.ftpServerAddress;
            tbRemotePath.Text = settings.ftpRemoteSubfolder;
            tbUsername.Text = settings.ftpUsername;
            tbPassword.Text = settings.DeObfuscate(settings.ftpObfuscatedPassword);
            tbWsprPath.Text = settings.wsprLogFilePath;
            nudTargetWidth.Value = settings.targetWidth;
            cbVerticalReduction.Checked = settings.verticalReduction == 2;
            nudFreqDisplayOffset.Value = settings.freqDisplayOffset;
            tbImageFileName.Text = settings.grabFileName;
            cbShowFreqScale.Checked = settings.showScaleOnAllGrabs;
            nudPxAbove.Value = settings.grabSavePxAbove;
            nudPxBelow.Value = settings.grabSavePxBelow;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            settings.ftpServerAddress = tbServer.Text;
            settings.ftpRemoteSubfolder = tbRemotePath.Text;
            settings.ftpUsername = tbUsername.Text;
            settings.ftpObfuscatedPassword = settings.Obfuscate(tbPassword.Text);
            settings.wsprLogFilePath = tbWsprPath.Text;
            settings.targetWidth = (int)nudTargetWidth.Value;
            settings.verticalReduction = cbVerticalReduction.Checked ? 2 : 1;
            settings.freqDisplayOffset = (int)nudFreqDisplayOffset.Value;
            settings.grabFileName = tbImageFileName.Text;
            settings.showScaleOnAllGrabs = cbShowFreqScale.Checked;
            settings.grabSavePxAbove = (int)nudPxAbove.Value;
            settings.grabSavePxBelow = (int)nudPxBelow.Value;
            settings.Save();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSetWsprPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "WSPR Log Files (*.txt)|*.txt";
            diag.FileName = "ALL_WSPR.TXT";
            diag.Title = "Locate WSPR Log File";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                //wsprLogFilePath = diag.FileName;
                //wsprLogLastReadModifiedTime = new DateTime(0);
            }
        }
    }
}
