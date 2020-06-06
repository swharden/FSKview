using FSKview.Audio;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace FSKview.Dev
{
    class Program
    {
        static void Main(string[] args)
        {
            Comparison2();
        }

        static void Comparison2()
        {
            // QRSS and WSPR band
            var grab = new Grab(minFreq: 1370, maxFreq: 1435);
            Console.WriteLine(grab.ToString().Replace(", ", "\n"));
            double[] audio = Simulate.TestSignals(grab.SampleRate);
            grab.AddAudio(audio);
            grab.ProcessAll();
            Bitmap bmp = grab.GetBitmap();
            Colormap.Viridis(bmp);
            bmp.Save("200605-03.png", ImageFormat.Png);
        }

        static void Comparison()
        {
            // isolated QRSS region
            double[] audio = Read.WavInt16mono(@"C:\Users\Scott\Documents\important\data\200605-6k-b.wav");
            var grab = new Grab(minFreq: 1220, maxFreq: 1260);
            Console.WriteLine(grab.ToString().Replace(", ", "\n"));
            grab.AddAudio(audio);
            grab.ProcessAll();
            Bitmap bmp = grab.GetBitmap();
            Colormap.Viridis(bmp);
            bmp.Save("200605-03.png", ImageFormat.Png);
        }
    }
}
