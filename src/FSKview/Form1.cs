using System;
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
    public partial class Form1 : Form
    {
        readonly Spectrogram.Colormap[] cmaps;
        WsprBand band;

        public Form1()
        {
            InitializeComponent();

            // start in center of screen occupying 80% of its height
            Height = (int)(Screen.FromControl(this).Bounds.Height * .8);
            Location = new Point(
                x: Screen.FromControl(this).Bounds.Width / 2 - Width / 2,
                y: Screen.FromControl(this).Bounds.Height / 2 - Height / 2);

            // list window functions
            cbWindow.Items.AddRange
            (
                typeof(FftSharp.Window)
                    .GetMethods()
                    .Select(x => x)
                    .Where(x => x.GetParameters().Length == 1)
                    .Where(x => x.ReturnType == typeof(double[]))
                    .OrderBy(x => x.Name)
                    .Select(x => x.Name)
                    .ToArray()
            );
            cbWindow.SelectedIndex = cbWindow.Items.IndexOf("Cosine");

            // list colormaps
            cmaps = Spectrogram.Colormap.GetColormaps();
            cbColormap.Items.AddRange(cmaps.Select(x => x.Name).ToArray());
            cbColormap.SelectedIndex = cbColormap.Items.IndexOf("Viridis");

            // list WSPR bands
            cbDialFreq.Items.AddRange(WsprBands.GetBands().Select(x => $"{x.name}: {x.dialFreq:N0} Hz").ToArray());
            cbDialFreq.SelectedIndex = 5;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetSpectrogram();
        }

        private void audioControl1_InputDeviceChanged(object sender, EventArgs e)
        {
            ResetSpectrogram();
        }

        Spectrogram.Spectrogram spec;
        Bitmap bmpVericalScale;
        Bitmap bmpSpectrogram;
        private void ResetSpectrogram()
        {
            int sampleRate = audioControl1.SampleRate;
            int fftSize = 1 << 15;
            int samplesInTenMinutes = sampleRate * 60 * 10;
            int targetWidth = 800;
            int stepSize = samplesInTenMinutes / targetWidth;

            spec = new Spectrogram.Spectrogram(sampleRate, fftSize, stepSize, fixedWidth: targetWidth);

            bmpVericalScale = spec.GetVerticalScale(120, band.dialFreq);
            bmpSpectrogram = new Bitmap(spec.Width + bmpVericalScale.Width, spec.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            pictureBox1.Image = bmpSpectrogram;
            pictureBox1.Size = pictureBox1.Image.Size;
            Width = pictureBox1.Width + 33;
        }

        private void timerUpdateSpectrogram_Tick(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            spec.Add(audioControl1.listener.GetNewAudio());

            using (Bitmap bmpIndexed = spec.GetBitmap((double)nudBrightness.Value))
            using (Graphics gfx = Graphics.FromImage(bmpSpectrogram))
            {
                gfx.DrawImage(bmpIndexed, 0, 0);
                gfx.DrawImage(bmpVericalScale, spec.Width, 0);
            }
            pictureBox1.Refresh();
        }

        private void cbWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: use reflection
        }

        private void cbColormap_SelectedIndexChanged(object sender, EventArgs e)
        {
            spec?.SetColormap(cmaps[cbColormap.SelectedIndex]);
        }

        private void cbDialFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            band = WsprBands.GetBands()[cbDialFreq.SelectedIndex];
        }
    }
}
