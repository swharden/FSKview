using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;

namespace FSKview
{
    public partial class AudioControl : UserControl
    {
        public Listener listener;
        public event EventHandler InputDeviceChanged;

        [
        Category("Audio"),
        Description("Audio device sample rate (Hz)")
        ]
        public int SampleRate { get; set; } = 6000;
        public int AudioDeviceIndex { get { return comboBox1.SelectedIndex; } }

        public AudioControl()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            pictureBox2.Width = 0;

            comboBox1.Items.AddRange(Enumerable
                .Range(0, NAudio.Wave.WaveIn.DeviceCount)
                .Select(x => NAudio.Wave.WaveIn.GetCapabilities(x).ProductName)
                .ToArray());

            if (comboBox1.Items.Count == 0)
                MessageBox.Show("No audio input devices were found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void SelectDevice(int deviceIndex)
        {
            if (deviceIndex > comboBox1.Items.Count - 1)
                deviceIndex = comboBox1.Items.Count - 1;
            if (deviceIndex < 0)
                deviceIndex = 0;
            comboBox1.SelectedIndex = deviceIndex;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listener?.Dispose();
            listener = new Listener(comboBox1.SelectedIndex, SampleRate);
            InputDeviceChanged?.Invoke(this, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listener is null)
                return;
            int targetWidth = (int)(listener.AmplitudeFrac * panel1.Width);
            pictureBox2.Width = (targetWidth >= pictureBox2.Width) ? targetWidth : pictureBox2.Width - 1;
            pictureBox2.BackColor = (listener.AmplitudeFrac < .9) ? Color.LightSeaGreen : Color.Magenta;
        }
    }
}
