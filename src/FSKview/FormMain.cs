using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FSKview
{
    public partial class FormMain : Form
    {
        readonly Spectrogram.Colormap[] cmaps;
        WsprBand band;
        string wsprLogFilePath = null;
        readonly List<WsprSpot> spots = new List<WsprSpot>();

        readonly int verticalScaleWidth = 130;

        private string UtcTimeStamp { get { return $"{DateTime.UtcNow.Hour:D2}:{DateTime.UtcNow.Minute:D2}:{DateTime.UtcNow.Second:D2}"; } }
        private string UtcDateStamp { get { return $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month:D2}-{DateTime.UtcNow.Day:D2}"; } }

        public FormMain()
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

            // predict default WSPR log file path and use it if it exists
            string predictedWsprFilePath = Path.GetFullPath(Application.LocalUserAppDataPath + "../../../../WSJT-X/ALL_WSPR.TXT");
            if (File.Exists(predictedWsprFilePath))
            {
                wsprLogFilePath = predictedWsprFilePath;
                cbWspr.Enabled = true;
                cbWspr.Checked = true;
            }
            else
            {
                cbWspr.Enabled = false;
                cbWspr.Checked = false;
            }
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
        int verticalReduction = 2;

        private void ResetSpectrogram()
        {
            int sampleRate = audioControl1.SampleRate;
            int fftSize = 1 << 14;
            int samplesInTenMinutes = sampleRate * 60 * 10;
            int targetWidth = 800;
            int stepSize = samplesInTenMinutes / targetWidth;

            spec = new Spectrogram.Spectrogram(sampleRate, fftSize, stepSize, fixedWidth: targetWidth);

            // resize the image based on the spectrogram dimensions
            bmpSpectrogram = new Bitmap(
                width: spec.Width + verticalScaleWidth,
                height: spec.Height / verticalReduction,
                format: PixelFormat.Format32bppPArgb);
            pictureBox1.Image = bmpSpectrogram;
            pictureBox1.Size = pictureBox1.Image.Size;
            Width = pictureBox1.Width + 33;
            UpdateVerticalScale();

            Status($"Spectrogram reset");
        }

        private void Status(string message)
        {
            lblStatus.Text = $"{UtcTimeStamp} UTC: {message}";
        }

        private void UpdateVerticalScale()
        {
            if (spec is null)
                return;

            band = WsprBands.GetBands()[cbDialFreq.SelectedIndex];
            bmpVericalScale = spec.GetVerticalScale(verticalScaleWidth, band.dialFreq, reduction: verticalReduction);
            int wsprBandTopPx = spec.PixelY(band.upperFreq - band.dialFreq, verticalReduction);
            panel1.AutoScrollPosition = new Point(0, wsprBandTopPx - 10);
        }

        private void timerUpdateSpectrogram_Tick(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            spec.Add(audioControl1.listener.GetNewAudio());
            AnnotateSpectrogram(
                spec, band, spots,
                bmpSpectrogram, bmpVericalScale,
                (double)nudBrightness.Value, verticalReduction,
                drawBandLines: true,
                partialTenMinute: true);
            pictureBox1.Refresh();
        }

        private static void AnnotateSpectrogram(
            Spectrogram.Spectrogram spec, WsprBand band, List<WsprSpot> spots,
            Bitmap bmpSpectrogram, Bitmap bmpVericalScale,
            double brightness, int verticalReduction,
            bool drawBandLines, bool partialTenMinute)
        {
            int secondsIntoTenMinute = (DateTime.UtcNow.Minute % 10) * 60 + DateTime.UtcNow.Second;
            double fracIntoTenMinute = secondsIntoTenMinute / 600.0;
            int nextIndex = (int)(fracIntoTenMinute * spec.Width);
            if (partialTenMinute == false)
                nextIndex = 0;

            using (Graphics gfx = Graphics.FromImage(bmpSpectrogram))
            using (Bitmap bmpIndexed = spec.GetBitmapMax(brightness, reduction: verticalReduction, firstColumnIndex: nextIndex))
            using (Pen bandEdgePen = new Pen(Color.White) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            using (var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold))
            using (var sfMiddleCenter = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center })
            using (var sfUpperLeft = new StringFormat { LineAlignment = StringAlignment.Near, Alignment = StringAlignment.Near })
            {
                // copy source bitmaps onto this display bitmap
                gfx.DrawImage(bmpIndexed, 0, 0);
                gfx.DrawImage(bmpVericalScale, spec.Width, 0);

                int wsprBandTopPx = spec.PixelY(band.upperFreq - band.dialFreq, verticalReduction);
                int wsprBandBottomPx = spec.PixelY(band.lowerFreq - band.dialFreq, verticalReduction);
                int qrssBandBottomPx = spec.PixelY(band.lowerFreq - band.dialFreq - 200, verticalReduction);
                if (drawBandLines)
                {
                    gfx.DrawLine(bandEdgePen, 0, wsprBandTopPx, spec.Width, wsprBandTopPx);
                    gfx.DrawLine(bandEdgePen, 0, wsprBandBottomPx, spec.Width, wsprBandBottomPx);
                    gfx.DrawLine(bandEdgePen, 0, qrssBandBottomPx, spec.Width, qrssBandBottomPx);
                }

                if (partialTenMinute)
                {
                    gfx.DrawLine(bandEdgePen, nextIndex, 0, nextIndex, spec.Height);
                }

                int[] seenMinutes = spots.Select(x => x.dt.Minute).Distinct().ToArray();

                int columnsPerTwoMinutes = (int)(60 * 2 / spec.SecPerPx);
                // TODO: this produces an error, as columns with no spots get shifted left

                for (int j = 0; j < seenMinutes.Length; j++)
                {
                    int minute = seenMinutes[j];
                    WsprSpot[] spotsThisMinute = spots.Where(x => x.dt.Minute == minute).ToArray();

                    for (int i = 0; i < spotsThisMinute.Length; i++)
                    {
                        WsprSpot spot = spotsThisMinute[i];

                        int r = 7;
                        int y = spec.PixelY(spot.frequencyHz - band.dialFreq, verticalReduction);
                        //int x = spec.Width - (int)(spot.ageSec / spec.SecPerPx);
                        int x = columnsPerTwoMinutes * j;

                        int xSpot = x + r * 2 * (i + 1);
                        gfx.FillEllipse(Brushes.Black, xSpot - r, y - r, r * 2, r * 2);
                        gfx.DrawString($"{i + 1}", font, Brushes.White, xSpot, y, sfMiddleCenter);

                        gfx.DrawString($"{i + 1}: {spot.callsign} ({spot.strength}) ", font, Brushes.White, x,
                            y: wsprBandBottomPx + 13 * i, sfUpperLeft);
                    }

                }
            }
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
            UpdateVerticalScale();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            MessageBox.Show(spec.ToString());
        }

        private void btnConfigureWspr_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "WSPR Log Files (*.txt)|*.txt";
            diag.FileName = "ALL_WSPR.TXT";
            diag.Title = "Locate WSPR Log File";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                wsprLogFilePath = diag.FileName;
                cbWspr.Enabled = true;
                cbWspr.Checked = true;
            }
            else
            {
                wsprLogFilePath = null;
                cbWspr.Enabled = false;
            }
            wsprLogLastReadModifiedTime = new DateTime(0);
        }

        DateTime wsprLogLastReadModifiedTime;
        private void LoadWsprSpots()
        {
            if (wsprLogFilePath is null)
                return;

            var lastModifiedTime = File.GetLastWriteTime(wsprLogFilePath);
            if (lastModifiedTime == wsprLogLastReadModifiedTime)
                return;
            else
                wsprLogLastReadModifiedTime = lastModifiedTime;

            spots.Clear();
            using (var stream = new FileStream(wsprLogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    var spot = new WsprSpot(streamReader.ReadLine());
                    bool isRecent = spot.ageSec <= 10 * 60;
                    //isRecent = true; // for development
                    if (spot.isValid && isRecent)
                        spots.Add(spot);
                }
            }

            Status($"Loaded {spots.Count} WSPR spots");
        }

        private void timerWsprUpdate_Tick(object sender, EventArgs e)
        {
            lblTime.Text = $"{UtcTimeStamp} UTC";
            LoadWsprSpots();

            bool isTenMinute = DateTime.UtcNow.Minute % 10 == 0;
            bool isWsprHadTime = DateTime.UtcNow.Second == 5; // gives WSPR time to analyze and save
            bool isLastSaveOld = lastSavedMinute != DateTime.UtcNow.Minute;
            if (cbSave.Checked && isTenMinute && isWsprHadTime && isLastSaveOld)
                SaveGrab();
        }

        private void cbReduction_CheckedChanged(object sender, EventArgs e)
        {
            verticalReduction = (cbReduction.Checked) ? 2 : 1;
            ResetSpectrogram();
        }

        private void cbSave_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSave.Checked)
                SaveGrab();
        }

        int lastSavedMinute = -1;
        private void SaveGrab()
        {
            // create a full-size custom annotated spectrogram
            Bitmap bmpSpec = spec.GetBitmap((double)nudBrightness.Value);
            AnnotateSpectrogram(spec, band, spots, bmpSpectrogram, bmpVericalScale,
                (double)nudBrightness.Value, verticalReduction,
                drawBandLines: false, partialTenMinute: false);

            // render both onto a smaller image
            int pxTop = spec.PixelY(band.upperFreq - band.dialFreq, verticalReduction) - 10;
            int pxBot = spec.PixelY(band.lowerFreq - 200 - band.dialFreq, verticalReduction) + 10;

            using (var bmp2 = new Bitmap(bmpSpec.Width + bmpVericalScale.Width, pxBot - pxTop))
            using (var gfx = Graphics.FromImage(bmp2))
            using (var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold))
            using (var sfMiddleCenter = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center })
            using (var sfLowerLeft = new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near })
            {
                gfx.DrawImage(bmpSpec, 0, -pxTop);
                gfx.DrawImage(bmpVericalScale, bmpSpec.Width, -pxTop);

                gfx.DrawString($"FSKview: Station AJ4VD (Gainesville, Florida, USA) {UtcDateStamp} {UtcTimeStamp} UTC",
                    font, Brushes.White, 3, bmp2.Height - 3, sfLowerLeft);
                bmp2.Save("aj4vd-latest.png", ImageFormat.Png);
                bmp2.Save($"aj4vd-{UtcDateStamp.Replace("-", "")}-{UtcTimeStamp.Replace(":", "")}.png", ImageFormat.Png);
                Status("Saved spectrogram as image file");
                lastSavedMinute = DateTime.UtcNow.Minute;
            }
        }
    }
}
