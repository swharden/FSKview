using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    public static class AGC
    {
        public static double[] SubtractMovingWindowFloor(double[] input, int windowSizePx = 10, double percentile = .2, double power = 1)
        {
            // return a copy of the input with the noise floor subtracted
            // where the noise floor is calculated from a moving window
            // this is good but very slow

            double[] normalized = new double[input.Length];

            double[] segment = new double[windowSizePx * 2 + 1];
            for (int i = windowSizePx; i < input.Length - windowSizePx; i++)
            {
                for (int j = 0; j < segment.Length; j++)
                    segment[j] = input[i + j - windowSizePx];

                Array.Sort(segment);

                int floorIndex = (int)(percentile * windowSizePx);
                double noiseFloor = segment[floorIndex];
                normalized[i] = Math.Max(input[i] - noiseFloor * power, 0);
            }

            return normalized;
        }
    }
}
