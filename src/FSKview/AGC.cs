using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    public static class AGC
    {
        public static double[] SubtractMovingWindowFloor(double[] input, int windowSizePx = 20, double percentile = .2)
        {
            // return a copy of the input with the noise floor subtracted
            // where the noise floor is calculated from a moving window
            // this is good but very slow

            double[] normalized = new double[input.Length];
            int floorIndex = (int)(percentile * windowSizePx);

            double[] segment = new double[windowSizePx];
            for (int i = 0; i < input.Length - windowSizePx; i++)
            {
                for (int j = 0; j < windowSizePx; j++)
                    segment[j] = input[i + j];
                Array.Sort(segment);
                normalized[i] = Math.Max(input[i] - segment[floorIndex], 0);
            }

            return normalized;
        }
    }
}
