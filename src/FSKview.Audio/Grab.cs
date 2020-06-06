using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FSKview.Audio
{
    /// <summary>
    /// Represents a single grab (2 seconds short of 10 minutes)
    /// </summary>
    public class Grab
    {
        public readonly int SampleRate;
        public readonly int FftSize;
        public readonly int Width;
        public readonly int Height;
        public readonly double FftHzPerPixel;

        private readonly double[] window;
        private readonly double[] audio;
        private readonly byte[,] pixelValues;
        private readonly int fftStepSize;
        private readonly int fftIndex1;
        private readonly int fftIndex2;
        private readonly bool[] fftProcessed;

        public Grab(int sampleRate = 6000, int fftSize = 1 << 14,
            double minFreq = 0, double maxFreq = double.PositiveInfinity)
        {
            SampleRate = sampleRate;
            FftSize = fftSize;
            FftHzPerPixel = (double)sampleRate / FftSize;
            window = FftSharp.Window.Hanning(FftSize);

            int totalSeconds = 60 * 10 - 2;
            audio = new double[sampleRate * totalSeconds];

            // todo: replace with fftSize/4 and fix fft size?
            int targetWidth = 1000;
            fftStepSize = (audio.Length - fftSize) / targetWidth;
            Width = (audio.Length - fftSize) / fftStepSize;
            fftProcessed = new bool[Width];

            fftIndex1 = (minFreq == 0) ? 0 : (int)(minFreq / FftHzPerPixel);
            fftIndex2 = (maxFreq > FftSize / 2) ? FftSize / 2 : (int)(maxFreq / FftHzPerPixel);
            Height = fftIndex2 - fftIndex1;

            pixelValues = new byte[Width, Height];
        }

        public override string ToString()
        {
            return $"Grab ({(double)audio.Length / SampleRate / 60:N2} minutes), " +
                   $"FFT: {FftSize:N0} points, " +
                   $"Height: {Height} px ({FftHzPerPixel:N2} Hz / px), " +
                   $"Width: {Width} ({(double)fftStepSize / SampleRate:N2} sec / px), " +
                   $"View: {FftHzPerPixel * fftIndex1:N2} Hz to {FftHzPerPixel * fftIndex2:N2} Hz";
        }

        public bool isFull { get { return NextIndex >= audio.Length; } }
        public int NextIndex { get; private set; }
        public void AddAudio(double[] newAudio)
        {
            int valuesRemaining = audio.Length - NextIndex;
            int valuesToCopy = (valuesRemaining >= newAudio.Length) ? newAudio.Length : valuesRemaining;
            Array.Copy(newAudio, 0, audio, NextIndex, valuesToCopy);
            NextIndex += valuesToCopy;
        }

        public void Window(double[] newWindow)
        {
            if (newWindow.Length != FftSize)
                throw new ArgumentException("window length must equal FFT size");
            Array.Copy(newWindow, 0, window, 0, FftSize);
        }

        public void Process()
        {
            List<int> colsToProcess = new List<int>();
            for (int columnIndex = 0; columnIndex < Width; columnIndex++)
            {
                int colIndex2 = columnIndex * fftStepSize + FftSize;
                bool isFilled = colIndex2 <= NextIndex - 1;
                bool isNotAlreadyProcessed = !fftProcessed[columnIndex];

                if (isFilled && isNotAlreadyProcessed)
                    colsToProcess.Add(columnIndex);
            }
            Console.WriteLine($"cols needing processing: {colsToProcess.Count}");

            Parallel.ForEach(colsToProcess, x =>
            {
                int offset = x * fftStepSize;
                Complex[] com = new Complex[FftSize];
                for (int i = 0; i < FftSize; i++)
                    com[i] = new Complex(audio[offset + i] * window[i], 0);

                FftSharp.Transform.FFT(com);

                double mult = 1.0 / FftSize;
                for (int i = 0; i < Height; i++)
                {
                    double value = com[fftIndex1 + i].Magnitude * mult;
                    value = Math.Min(value, 255);
                    pixelValues[x, i] = (byte)value;
                }

                fftProcessed[x] = true;
            });
        }

        public Bitmap GetBitmap()
        {
            return Image.Create(pixelValues);
        }
    }
}
