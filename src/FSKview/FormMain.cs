using Spectrogram;
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
        readonly ProgramSettings settings;
        readonly Version version = Assembly.GetExecutingAssembly().GetName().Version;

        readonly Spectrogram.Colormap[] cmaps;
        WsprBand band;
        readonly List<WsprSpot> spots = new List<WsprSpot>();
        readonly string appPath = Path.GetFullPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        readonly int verticalScaleWidth = 130;

        private string UtcTimeStamp { get { return $"{DateTime.UtcNow.Hour:D2}:{DateTime.UtcNow.Minute:D2}:{DateTime.UtcNow.Second:D2}"; } }
        private string UtcTimeStampNoSec { get { return $"{DateTime.UtcNow.Hour:D2}:{DateTime.UtcNow.Minute:D2}"; } }
        private string UtcDateStamp { get { return $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month:D2}-{DateTime.UtcNow.Day:D2}"; } }

        public FormMain()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            settings = File.Exists("settings.xml") ? ProgramSettings.Load("settings.xml") : new ProgramSettings();
            lblVersion.Text = $"FSKview {version.Major}.{version.Minor}.{version.Build}";

            // start in center of screen occupying 80% of its height
            Height = (int)(Screen.FromControl(this).Bounds.Height * .8);
            Location = new Point(
                x: Screen.FromControl(this).Bounds.Width / 2 - Width / 2,
                y: Screen.FromControl(this).Bounds.Height / 2 - Height / 2);

            // pre-populate listboxes
            cbWindow.Items.AddRange(FftSharp.Window.GetWindowNames());
            cmaps = Spectrogram.Colormap.GetColormaps();
            cbColormap.Items.AddRange(cmaps.Select(x => x.Name).ToArray());
            cbDialFreq.Items.AddRange(WsprBands.GetBands().Select(x => $"{x.name}: {x.dialFreq:N0} Hz").ToArray());

            // pre-select items based on saved settings
            audioControl1.SelectDevice(settings.audioDeviceIndex);
            cbWindow.SelectedIndex = cbWindow.Items.IndexOf(settings.window);
            cbColormap.SelectedIndex = cbColormap.Items.IndexOf(settings.colormap);
            cbDialFreq.SelectedIndex = settings.wsprBandIndex;
            cbWspr.Checked = settings.isWsprEnabled;
            cbFTP.Checked = settings.isFtpEnabled;

            // select on load
            ActiveControl = cbColormap;
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
            settings.audioDeviceIndex = audioControl1.AudioDeviceIndex;
            settings.Save();
            ResetSpectrogram();
        }

        Spectrogram.Spectrogram spec;
        Bitmap bmpVericalScale;
        Bitmap bmpSpectrogram;

        private void ResetSpectrogram()
        {
            int sampleRate = audioControl1.SampleRate;
            int fftSize = 1 << 14;
            int samplesInTenMinutes = sampleRate * 60 * 10;
            int targetWidth = settings.targetWidth;
            int stepSize = samplesInTenMinutes / targetWidth;

            spec = new Spectrogram.Spectrogram(sampleRate, fftSize, stepSize, fixedWidth: targetWidth);
            spec.SetColormap(cmaps.Where(x => x.Name == settings.colormap).First());

            // reset the spectrogram based on where we are in the 10 minute block
            ResetSpectrogramPosition();

            // resize the image based on the spectrogram dimensions
            bmpSpectrogram = new Bitmap(
                width: spec.Width + verticalScaleWidth,
                height: spec.Height / settings.verticalReduction,
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
            if (spec != null && cbDialFreq.SelectedIndex >= 0)
            {
                bmpVericalScale = Ruler.GetVerticalScale(verticalScaleWidth, spec, band.dialFreq, settings.freqDisplayOffset, settings.verticalReduction);
                ScrollToUpperBandEdge();
            }
        }

        private void ScrollToUpperBandEdge()
        {
            int wsprBandTopPx = spec.PixelY(band.upperFreq - band.dialFreq, settings.verticalReduction);
            panel1.AutoScrollPosition = new Point(0, wsprBandTopPx - settings.grabSavePxAbove + 1);
        }

        private void timerUpdateSpectrogram_Tick(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            spec.Add(audioControl1.listener.GetNewAudio());
            var spotsToShow = spots.Where(x => x.ageSec < (11 * 60)).ToList();
            Annotate.Spectrogram(spec, band, spotsToShow, bmpSpectrogram, bmpVericalScale, cbBands.Checked, settings);
            pictureBox1.Refresh();
            GC.Collect();
        }

        private void cbWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            settings.window = cbWindow.Text;
            settings.Save();

            double[] window = FftSharp.Window.GetWindowByName(cbWindow.Text, spec.FftSize);
            spec.SetWindow(window);
        }

        private void cbColormap_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.colormap = cbColormap.Text;
            settings.Save();

            spec?.SetColormap(cmaps[cbColormap.SelectedIndex]);
        }

        private void cbDialFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            band = WsprBands.GetBands()[cbDialFreq.SelectedIndex];
            settings.wsprBandIndex = cbDialFreq.SelectedIndex;
            settings.Save();

            UpdateVerticalScale();
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            int oldTargetWidth = settings.targetWidth;
            int oldReduction = settings.verticalReduction;

            new FormSettings(settings).ShowDialog();

            bool targetWidthChanged = (oldTargetWidth != settings.targetWidth);
            bool reductionChanged = (oldReduction != settings.verticalReduction);
            if (targetWidthChanged || reductionChanged)
                ResetSpectrogram();

            UpdateVerticalScale();
            ScrollToUpperBandEdge();
        }

        DateTime wsprLogLastReadModifiedTime;
        private void LoadWsprSpots()
        {
            if (settings.wsprLogFilePath is null)
                return;

            if (!File.Exists(settings.wsprLogFilePath))
                return;

            var lastModifiedTime = File.GetLastWriteTime(settings.wsprLogFilePath);
            if (lastModifiedTime == wsprLogLastReadModifiedTime)
                return;
            else
                wsprLogLastReadModifiedTime = lastModifiedTime;

            spots.Clear();
            using (var stream = new FileStream(settings.wsprLogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
            int second = (DateTime.UtcNow.Minute % 10) * 60 + DateTime.UtcNow.Second;
            lblTime.Text = $"{UtcTimeStamp} UTC";
            lblStatusTime.Text = $"{UtcTimeStamp} UTC (second {second}/600)";
            pbTimeFrac.Value = second;
            LoadWsprSpots();

            bool isTenMinute = DateTime.UtcNow.Minute % 10 == 0;
            bool isWsprHadTime = DateTime.UtcNow.Second == 2;
            if (isTenMinute && isWsprHadTime)
            {
                spec.RollReset();
                if (cbSave.Checked && cbSave.Enabled)
                    SaveGrab(uploadToo: cbFTP.Checked);
            }
        }

        private void cbSave_CheckedChanged(object sender, EventArgs e)
        {
            settings.saveGrabs = cbSave.Checked;
            settings.Save();

            cbFTP.Enabled = cbSave.Checked;

            if (cbSave.Checked)
            {
                SaveGrab();
                Process.Start("explorer.exe", $"\"{appPath}\"");
            }
        }

        private void SaveGrab(bool uploadToo = false)
        {
            int pxTop = spec.PixelY(band.upperFreq - band.dialFreq, settings.verticalReduction) - settings.grabSavePxAbove;
            int pxBot = spec.PixelY(band.lowerFreq - 200 - band.dialFreq, settings.verticalReduction) + settings.grabSavePxBelow;
            int height = pxBot - pxTop;
            int widthWithScale = spec.Width + verticalScaleWidth;

            using (Bitmap bmpFull = new Bitmap(widthWithScale, spec.Height, PixelFormat.Format32bppPArgb))
            using (Bitmap bmpCropped = new Bitmap(widthWithScale, height, PixelFormat.Format32bppPArgb))
            using (Graphics gfx = Graphics.FromImage(bmpCropped))
            {
                // annotate a full-size spectrogram
                var spotsToShow = spots.Where(x => x.ageSec < (11 * 60)).ToList();
                Annotate.Spectrogram(spec, band, spotsToShow, bmpFull, bmpVericalScale, false, settings);

                // draw the full-size spectrogram on the cropped Bitmap
                gfx.DrawImage(bmpFull, 0, -pxTop);

                // decorate the cropped bitmap
                string msg = $"FSKview {version.Major}.{version.Minor}.{version.Build}: " +
                    $"{settings.stationInformation} {UtcDateStamp} {UtcTimeStampNoSec} UTC";
                Annotate.Logo(gfx, msg, 3, height - 3);

                // ensure output folders exist
                string pathSaveWeb = $"{appPath}/grabs-web";
                string pathSaveAll = $"{appPath}/grabs-all";
                if (!Directory.Exists(pathSaveWeb))
                    Directory.CreateDirectory(pathSaveWeb);
                if (!Directory.Exists(pathSaveAll))
                    Directory.CreateDirectory(pathSaveAll);

                // save the cropped image for the web
                ImageFormat imgFmt = ImageFormat.Bmp;
                if (settings.grabFileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    imgFmt = ImageFormat.Png;
                else if (settings.grabFileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                    imgFmt = ImageFormat.Jpeg;
                else if (settings.grabFileName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    imgFmt = ImageFormat.Gif;
                bmpCropped.Save($"{pathSaveWeb}/{settings.grabFileName}", imgFmt);

                // save the cropped bitmap for stitching
                string stitchFileName = $"{pathSaveAll}/{UtcDateStamp.Replace("-", "")}-{UtcTimeStamp.Replace(":", "")}.png";
                if (settings.showScaleOnAllGrabs)
                {
                    bmpCropped.Save(stitchFileName, ImageFormat.Png);
                }
                else
                {
                    using (Bitmap bmpCropped2 = new Bitmap(spec.Width, height, PixelFormat.Format32bppPArgb))
                    using (Graphics gfx2 = Graphics.FromImage(bmpCropped2))
                    {
                        gfx2.DrawImage(bmpCropped, 0, 0);
                        bmpCropped2.Save(stitchFileName, ImageFormat.Png);
                    }
                }

                Status($"Saved spectrogram as {settings.grabFileName}");

                if (uploadToo)
                {
                    Enabled = false;
                    Status($"Performing FTP upload...");
                    Application.DoEvents();
                    string result = FTP.Upload(settings.ftpServerAddress, settings.ftpRemoteSubfolder, settings.ftpUsername, 
                        settings.DeObfuscate(settings.ftpObfuscatedPassword), $"{pathSaveWeb}/{settings.grabFileName}");
                    if (result.Contains("Not logged in"))
                        result = "Incorrect username/password";
                    else if (result.Contains("File name not allowed"))
                        result = "Invalid path (does the target folder exist?)";
                    else if (result == "FTP upload success!")
                        result = "FTP upload successful";
                    else
                        result = "FTP ERROR (get full message in settings)";
                    Status(result);
                    Enabled = true;
                }
            }
        }

        private void nudBrightness_ValueChanged(object sender, EventArgs e)
        {
            settings.brightness = (double)nudBrightness.Value;
            settings.Save();
        }

        private void cbBands_CheckedChanged(object sender, EventArgs e)
        {
            settings.showBands = cbBands.Checked;
            settings.Save();
            if (cbBands.Checked)
                ScrollToUpperBandEdge();
        }

        private void cbWspr_CheckedChanged(object sender, EventArgs e)
        {
            settings.isWsprEnabled = cbWspr.Checked;
            settings.Save();
        }

        private void ResetSpectrogramPosition()
        {
            if (settings.roll)
            {
                int secondsIntoTenMinute = (DateTime.UtcNow.Minute % 10) * 60 + DateTime.UtcNow.Second;
                double fracIntoTenMinute = secondsIntoTenMinute / 600.0;
                int nextIndex = (int)(fracIntoTenMinute * spec.Width);
                spec.RollReset(nextIndex);
            }
            else
            {
                spec.RollReset(spec.Width);
            }
        }

        private void cbRoll_CheckedChanged(object sender, EventArgs e)
        {
            ResetSpectrogramPosition();
        }

        private void cbFTP_CheckedChanged(object sender, EventArgs e)
        {
            settings.isFtpEnabled = cbFTP.Checked;
            settings.Save();
        }
    }
}
