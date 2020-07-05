using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    public static class Ruler
    {
        public static Bitmap GetVerticalScale(int width, Spectrogram.Spectrogram spec, int offsetHz = 0, int reduction = 1)
        {
            int majorTickHz = 50;
            int majorTickLength = 4;
            int minorTickHz = 10;
            int minorTickLength = 1;

            Bitmap bmp = new Bitmap(width, spec.Height / reduction);

            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font(FontFamily.GenericMonospace, 10))
            using (var sf = new StringFormat() { LineAlignment = StringAlignment.Center })
            {
                gfx.Clear(Color.White);

                List<double> freqsMajor = new List<double>();
                for (double f = spec.FreqMin; f <= spec.FreqMax; f += majorTickHz)
                    freqsMajor.Add(f);

                List<double> freqsMinor = new List<double>();
                for (double f = spec.FreqMin; f <= spec.FreqMax; f += minorTickHz)
                    freqsMinor.Add(f);

                // don't show first or last major tick
                if (freqsMajor.Count >= 2)
                {
                    freqsMajor.RemoveAt(0);
                    freqsMajor.RemoveAt(freqsMajor.Count - 1);
                }

                foreach (var freq in freqsMajor)
                {
                    int y = spec.PixelY(freq) / reduction;
                    gfx.DrawLine(pen, 0, y, majorTickLength, y);
                    gfx.DrawString($"{freq + offsetHz:N0} Hz", font, brush, majorTickLength, y, sf);
                }

                foreach (var freq in freqsMinor)
                {
                    int y = spec.PixelY(freq) / reduction;
                    gfx.DrawLine(pen, 0, y, minorTickLength, y);
                }
            }

            return bmp;
        }
    }
}
