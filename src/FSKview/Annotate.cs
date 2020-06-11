using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    static class Annotate
    {
        public static void Spectrogram(
            Spectrogram.Spectrogram spec, WsprBand band, List<WsprSpot> spots,
            Bitmap bmpSpectrogram, Bitmap bmpVericalScale,
            double brightness, int verticalReduction,
            bool drawBandLines, bool roll)
        {
            using (Graphics gfx = Graphics.FromImage(bmpSpectrogram))
            using (Bitmap bmpIndexed = spec.GetBitmapMax(brightness, reduction: verticalReduction, roll: true))
            using (Pen bandEdgePen = new Pen(Color.White) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            using (Pen rollPen = new Pen(Color.White))
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

                if (roll)
                {
                    gfx.DrawLine(rollPen, spec.NextColumnIndex, 0, spec.NextColumnIndex, spec.Height);
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
    }
}
