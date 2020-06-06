using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace FSKview.Tests
{
    public class Tests
    {
        [Test]
        public void Test_Analyze_InSmallChuncks()
        {
            var grab = new Audio.Grab(minFreq: 1370, maxFreq: 1435);

            double[] audio = Audio.Simulate.TestSignals(grab.SampleRate, lengthMinutes: 10);
            double[] oneSecChunk = new double[grab.SampleRate];

            grab.Process();
            for (int i = 0; i < audio.Length / oneSecChunk.Length; i++)
            {
                Array.Copy(audio, i * oneSecChunk.Length, oneSecChunk, 0, oneSecChunk.Length);
                grab.AddAudio(oneSecChunk);
                grab.Process();
            }
            grab.Process();

            Bitmap bmp = grab.GetBitmap();
            Audio.Colormap.Viridis(bmp);
            bmp.Save("test-chunks.png", ImageFormat.Png);
            Console.WriteLine("Saved output in: " + System.IO.Path.GetFullPath("./"));
        }

        [Test]
        public void Test_Analyze_AllAtOnce()
        {
            var grab = new Audio.Grab(minFreq: 1370, maxFreq: 1435);

            double[] audio = Audio.Simulate.TestSignals(grab.SampleRate, lengthMinutes: 10);
            grab.AddAudio(audio);
            grab.Process();

            Bitmap bmp = grab.GetBitmap();
            Audio.Colormap.Viridis(bmp);
            bmp.Save("test-all.png", ImageFormat.Png);
            Console.WriteLine("Saved output in: " + System.IO.Path.GetFullPath("./"));
        }
    }
}