using System;
using System.Collections.Generic;
using System.Text;

namespace FSKview.Dev
{
    public static class Simulate
    {
        public static double[] TestSignals(int sampleRate = 6000, int lengthMinutes = 10,
            int freqHz = 1400, int shiftHz = 5, double maxSNR = .5)
        {
            Random rand = new Random(0);
            double[] data = new double[sampleRate * lengthMinutes * 60];
            for (int i = 0; i < data.Length; i++)
            {
                // start with noise noise
                data[i] += RandomNormalValue(rand, 0, .2) * (1 << 14);

                // add signals
                double thisSNR = maxSNR * i / data.Length;
                double mult = thisSNR * thisSNR * (1 << 14);
                int second = i / sampleRate;

                // 5 second square waves
                double qrss5freq = (second % 10 < 5) ? freqHz : freqHz + shiftHz;
                data[i] += Math.Sin(i * qrss5freq / sampleRate * 2 * Math.PI) * mult;

                // 15 second square waves
                double qrss15freq = (second % 30 < 15) ? freqHz : freqHz + shiftHz;
                qrss15freq += 20;
                data[i] += Math.Sin(i * qrss15freq / sampleRate * 2 * Math.PI) * mult;

                // 15 second triangle waves
                int triangleSampleCount = 15 * sampleRate;
                double triangleFrac = (double)(i % triangleSampleCount) / triangleSampleCount / 2;
                double triangleFreq = triangleFrac * 5 + freqHz - 20;
                data[i] += Math.Sin((i % triangleSampleCount) * triangleFreq / sampleRate * 2 * Math.PI) * mult;
            }

            return data;
        }

        private static double RandomNormalValue(Random rand, double mean, double stdDev, double maxSdMultiple = 10)
        {
            while (true)
            {
                double u1 = 1.0 - rand.NextDouble();
                double u2 = 1.0 - rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
                if (Math.Abs(randStdNormal) < maxSdMultiple)
                    return mean + stdDev * randStdNormal;
            }
        }
    }
}
