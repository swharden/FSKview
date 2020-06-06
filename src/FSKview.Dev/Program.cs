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

        }

        static void SimulatedColormaps()
        {
            var grab = new Grab(minFreq: 1370, maxFreq: 1435);
            Console.WriteLine(grab.ToString().Replace(", ", "\n"));
            double[] audio = Simulate.TestSignals(grab.SampleRate);
            grab.AddAudio(audio);
            grab.Process();
            Bitmap bmp = grab.GetBitmap();
            Colormap.Grayscale(bmp);
            bmp.Save("colormap-grayscale.png", ImageFormat.Png);
            Colormap.Viridis(bmp);
            bmp.Save("colormap-viridis.png", ImageFormat.Png);
        }

        static void RealQRSS()
        {
            // isolated QRSS region
            double[] audio = Read.WavInt16mono(@"C:\Users\Scott\Documents\important\data\200605-6k-b.wav");
            var grab = new Grab(minFreq: 1220, maxFreq: 1260);
            Console.WriteLine(grab.ToString().Replace(", ", "\n"));
            grab.AddAudio(audio);
            grab.Process();
            Bitmap bmp = grab.GetBitmap();
            Colormap.Viridis(bmp);
            bmp.Save("200605-03.png", ImageFormat.Png);
        }
    }
}
