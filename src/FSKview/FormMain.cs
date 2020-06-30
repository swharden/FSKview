using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
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
        readonly string appPath = Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        readonly int verticalScaleWidth = 130;

        private string UtcTimeStamp { get { return $"{DateTime.UtcNow.Hour:D2}:{DateTime.UtcNow.Minute:D2}:{DateTime.UtcNow.Second:D2}"; } }
        private string UtcDateStamp { get { return $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month:D2}-{DateTime.UtcNow.Day:D2}"; } }

        public FormMain()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = $"FSKview {version.Major}.{version.Minor}.{version.Build}";

            // start in center of screen occupying 80% of its height
            Height = (int)(Screen.FromControl(this).Bounds.Height * .8);
            Location = new Point(
                x: Screen.FromControl(this).Bounds.Width / 2 - Width / 2,
                y: Screen.FromControl(this).Bounds.Height / 2 - Height / 2);

            // list window functions
            cbWindow.Items.AddRange(FftSharp.Window.GetWindowNames());
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            ResetSpectrogram();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioControl1.listener.Dispose();
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

            // reset the spectrogram based on where we are in the 10 minute block
            int secondsIntoTenMinute = (DateTime.UtcNow.Minute % 10) * 60 + DateTime.UtcNow.Second;
            double fracIntoTenMinute = secondsIntoTenMinute / 600.0;
            int nextIndex = (int)(fracIntoTenMinute * spec.Width);
            spec.RollReset(nextIndex);

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
            int pxPaddingAboveBandLimit = 10 + 13 * 8;
            panel1.AutoScrollPosition = new Point(0, wsprBandTopPx - pxPaddingAboveBandLimit);
        }

        private void timerUpdateSpectrogram_Tick(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            spec.Add(audioControl1.listener.GetNewAudio());
            var spotsToShow = spots.Where(x => x.ageSec < (10 * 60)).ToList();
            Annotate.Spectrogram(
                spec, band, spotsToShow,
                bmpSpectrogram, bmpVericalScale,
                (double)nudBrightness.Value, verticalReduction,
                drawBandLines: cbBands.Checked,
                roll: true);
            pictureBox1.Refresh();
            GC.Collect();
        }

        private void cbWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            double[] window = FftSharp.Window.GetWindowByName(cbWindow.Text, spec.FftSize);
            spec.SetWindow(window);
        }

        private void cbColormap_SelectedIndexChanged(object sender, EventArgs e)
        {
            spec?.SetColormap(cmaps[cbColormap.SelectedIndex]);
        }

        private void cbDialFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVerticalScale();
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
                    bool isRecent = spot.ageSec <= 30 * 60; // load spots within the last 15 min (apply a more tighter display rule later)
                    if (spot.isValid && isRecent)
                    {
                        spots.Add(spot);
                    }
                }
            }

            Status($"Loaded {spots.Count} WSPR spots");
        }

        string stampLast;
        private void timerWsprUpdate_Tick(object sender, EventArgs e)
        {
            string stampNow = $"{DateTime.UtcNow.Hour}{DateTime.UtcNow.Minute}{DateTime.UtcNow.Second}";
            if (stampNow == stampLast)
                return;

            stampLast = stampNow;
            lblTime.Text = $"{UtcTimeStamp} UTC";
            LoadWsprSpots();

            bool isTenMinute = DateTime.UtcNow.Minute % 10 == 0;
            bool isWsprHadTime = DateTime.UtcNow.Second == 2;
            if (isTenMinute && isWsprHadTime)
            {
                Debug.WriteLine($"RESETTING AT {DateTime.UtcNow}");
                spec.RollReset();
                if (cbSave.Checked)
                    SaveGrab();
            }
        }

        private void cbReduction_CheckedChanged(object sender, EventArgs e)
        {
            verticalReduction = (cbReduction.Checked) ? 2 : 1;
            ResetSpectrogram();
        }

        private void cbSave_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSave.Checked)
            {
                SaveGrab();
                System.Diagnostics.Process.Start("explorer.exe", $"\"{appPath}\"");
            }
        }

        int lastSavedMinute = -1;
        private void SaveGrab()
        {
            int pxPaddingAboveBandLimit = 10 + 13 * 8;
            int pxTop = spec.PixelY(band.upperFreq - band.dialFreq, verticalReduction) - pxPaddingAboveBandLimit;
            int pxBot = spec.PixelY(band.lowerFreq - 200 - band.dialFreq, verticalReduction) + 10;
            int height = pxBot - pxTop;
            int width = spec.Width + verticalScaleWidth;

            using (Bitmap bmpFull = new Bitmap(width, spec.Height, PixelFormat.Format32bppPArgb))
            using (Bitmap bmpCropped = new Bitmap(width, height, PixelFormat.Format32bppPArgb))
            using (Graphics gfx = Graphics.FromImage(bmpCropped))
            {
                // annotate a full-size spectrogram
                var spotsToShow = spots.Where(x => x.ageSec < (11 * 60)).ToList();
                Annotate.Spectrogram(spec, band, spotsToShow, bmpFull, bmpVericalScale,
                                    (double)nudBrightness.Value, verticalReduction,
                                    drawBandLines: false, roll: false);

                // draw the full-size spectrogram on the cropped Bitmap
                gfx.DrawImage(bmpFull, 0, -pxTop);

                // draw the scale
                gfx.DrawImage(bmpVericalScale, 0, spec.Width);

                // decorate the cropped bitmap
                string stationInformation = "(station.txt not found)";
                if (File.Exists("station.txt"))
                    stationInformation = File.ReadAllText("station.txt").Trim();

                string msg = $"FSKview: {stationInformation} {UtcDateStamp} {UtcTimeStamp} UTC";
                Annotate.Logo(gfx, msg, 3, height - 3);

                // ensure output folders exist
                string pathSaveWeb = $"{appPath}/grabs-web";
                string pathSaveAll = $"{appPath}/grabs-all";
                if (!Directory.Exists(pathSaveWeb))
                    Directory.CreateDirectory(pathSaveWeb);
                if (!Directory.Exists(pathSaveAll))
                    Directory.CreateDirectory(pathSaveAll);

                // save the cropped bitmap
                bmpCropped.Save($"{pathSaveWeb}/latest.png", ImageFormat.Png);
                bmpCropped.Save($"{pathSaveAll}/{UtcDateStamp.Replace("-", "")}-{UtcTimeStamp.Replace(":", "")}.png", ImageFormat.Png);
                Status("Saved spectrogram as image file");
                lastSavedMinute = DateTime.UtcNow.Minute;
            }
        }
    }
}
