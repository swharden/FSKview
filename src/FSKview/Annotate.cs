﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1416 // Ignore System.Drawing platform compatibility

namespace FSKview
{
    static class Annotate
    {
        public static void Spectrogram(
            Spectrogram.Spectrogram spec, WsprBand band, List<WsprSpot> spots,
            Bitmap bmpSpectrogram, Bitmap bmpVericalScale,
            bool drawBandLines, bool drawVerticalLine, ProgramSettings settings)
        {
            using (Graphics gfx = Graphics.FromImage(bmpSpectrogram))
            using (Bitmap bmpIndexed = spec.GetBitmapMax(settings.brightness, reduction: settings.verticalReduction, roll: settings.roll))
            using (Pen bandEdgePen = new Pen(Color.White) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            using (Pen grabEdgePen = new Pen(Color.Yellow) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            using (Pen rollPen = new Pen(Color.White))
            using (var font = new Font("consolas", 10, FontStyle.Bold))
            using (var sfMiddleCenter = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center })
            using (var sfUpperLeft = new StringFormat { LineAlignment = StringAlignment.Near, Alignment = StringAlignment.Near })
            {
                // copy source bitmaps onto this display bitmap
                gfx.DrawImage(bmpIndexed, 0, 0);
                gfx.DrawImage(bmpVericalScale, spec.Width, 0);

                int wsprBandTopPx = spec.PixelY(band.upperFreq - band.dialFreq, settings.verticalReduction) + 1;
                int wsprBandBottomPx = spec.PixelY(band.lowerFreq - band.dialFreq, settings.verticalReduction) + 1;
                int qrssBandBottomPx = spec.PixelY(band.lowerFreq - band.dialFreq - 200, settings.verticalReduction) + 1;
                int grabTopPx = wsprBandTopPx - settings.grabSavePxAbove;
                int grabBotPx = qrssBandBottomPx + settings.grabSavePxBelow;

                if (drawBandLines)
                {
                    gfx.DrawLine(bandEdgePen, 0, wsprBandTopPx, spec.Width, wsprBandTopPx);
                    gfx.DrawLine(bandEdgePen, 0, wsprBandBottomPx, spec.Width, wsprBandBottomPx);
                    gfx.DrawLine(bandEdgePen, 0, qrssBandBottomPx, spec.Width, qrssBandBottomPx);
                    gfx.DrawLine(grabEdgePen, 0, grabTopPx - 1, spec.Width, grabTopPx - 1);
                    gfx.DrawLine(grabEdgePen, 0, grabBotPx, spec.Width, grabBotPx);
                }

                if (settings.showTimeLines)
                {
                    using Pen pen = new(Color.FromArgb(100, Color.White));
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    for (int i = 1; i < 10; i++)
                    {
                        int pxFromLeft = (int)(i * 60 / spec.SecPerPx);
                        gfx.DrawLine(pen, pxFromLeft, 0, pxFromLeft, spec.Height);
                    }
                }

                if (settings.roll && drawVerticalLine)
                    gfx.DrawLine(rollPen, spec.NextColumnIndex, 0, spec.NextColumnIndex, spec.Height);

                if (settings.isWsprEnabled == false)
                    return;

                // plot spots in a single segment (the 2m block within a 10m time frame)
                for (int segment = 0; segment < 5; segment++)
                {
                    WsprSpot[] segmentSpots = spots
                                                .Where(x => x.segment == segment) // only this segment
                                                .Where(x => Math.Abs(x.frequencyHz - band.dialFreq) < 1e5) // only this band
                                                .OrderBy(x => x.strength).GroupBy(x => x.callsign).Select(x => x.Last()) // only strongest
                                                .OrderBy(x => x.frequencyHz).Reverse().ToArray(); // top to bottom

                    int segmentX = spec.Width * segment / 5;
                    if (settings.roll == false && segmentSpots.Length > 0)
                        segmentX = (int)(spec.Width - segmentSpots[0].ageSec / spec.SecPerPx) + 10;

                    for (int spotIndex = 0; spotIndex < segmentSpots.Length; spotIndex++)
                    {
                        WsprSpot spot = segmentSpots[spotIndex];

                        int r = 7;
                        int y = spec.PixelY(spot.frequencyHz - band.dialFreq, settings.verticalReduction);

                        // draw the marker
                        if (spot.isWspr)
                        {
                            int xSpot = segmentX + r * 2 * (spotIndex % 8 + 1);

                            // determine where to place the marker
                            gfx.FillEllipse(Brushes.Black, xSpot - r, y - r, r * 2, r * 2);
                            gfx.DrawString($"{spotIndex + 1}", font, Brushes.White, xSpot + 0.5f, y + 1, sfMiddleCenter);

                            // draw the key label at the top of the band edge
                            DrawStringWithShadow(gfx, $"{spotIndex + 1}: {spot.callsign} ({spot.strength} dB) ",
                                segmentX, wsprBandTopPx - settings.grabSavePxAbove + 13 * spotIndex,
                                font, sfUpperLeft, Brushes.White, Brushes.Black);
                        }
                        else
                        {
                            // draw the callsign exactly where the spot is on the spectrogram
                            int xSpot = (int)(segmentX + (spot.dt.Minute % 2 * 60 + spot.dt.Second) / spec.SecPerPx);
                            DrawStringWithShadow(gfx, spot.callsign, xSpot, y, font, sfUpperLeft, Brushes.White, Brushes.Black);
                        }
                    }
                }
            }
        }

        public static void Logo(Graphics gfx, string message, float x, float y)
        {
            using (var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold))
            using (var sfLowerLeft = new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near })
            {
                DrawStringWithShadow(gfx, message, x, y, font, sfLowerLeft, Brushes.White, Brushes.Black);
            }
        }

        public static void TimeTicks(Graphics gfx, float height, double secPerPx, int tickLength = 3)
        {
            using (var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold))
            using (var sfLowerLeft = new StringFormat { LineAlignment = StringAlignment.Far, Alignment = StringAlignment.Near })
            {
                for (int i = 1; i < 10; i++)
                {
                    int seconds = i * 60;
                    int pxFromLeft = (int)(seconds / secPerPx);
                    gfx.DrawLine(Pens.Black, pxFromLeft - 1, height, pxFromLeft - 1, height - tickLength);
                    gfx.DrawLine(Pens.Black, pxFromLeft + 1, height, pxFromLeft + 1, height - tickLength);
                    gfx.DrawLine(Pens.White, pxFromLeft, height, pxFromLeft, height - tickLength);
                }
            }
        }

        public static void DrawStringWithShadow(Graphics gfx, String str, float x, float y,
            Font font, StringFormat sf, Brush foreground, Brush background)
        {
            for (int dx = -1; dx < 2; dx++)
                for (int dy = -1; dy < 2; dy++)
                    if (dx != 0 || dy != 0)
                        gfx.DrawString(str, font, background, x + dx, y + dy, sf);

            gfx.DrawString(str, font, foreground, x, y, sf);
        }
    }
}
