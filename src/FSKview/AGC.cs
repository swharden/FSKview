using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSKview
{
    public static class AGC
    {
        private readonly static Random Rand = new Random();

        /// <summary>
        /// Return a copy of the input with the noise floor subtracted from it.
        /// Noise floor is calculated as the mean of the quietest 20% of values.
        /// </summary>
        public static double[] Method1_SortedFloorMean(double[] input, int windowSizePx = 10, double percentile = .2, double power = 1)
        {
            double[] normalized = new double[input.Length];

            double[] segment = new double[windowSizePx * 2 + 1];
            int valuesInFloor = (int)(percentile * segment.Length);

            for (int i = windowSizePx; i < input.Length - windowSizePx; i++)
            {
                for (int j = 0; j < segment.Length; j++)
                    segment[j] = input[i + j - windowSizePx];

                Array.Sort(segment);

                double meanFloor = segment.Take(valuesInFloor).Sum() / valuesInFloor;
                normalized[i] = Math.Max(input[i] - meanFloor * power, 0);
            }

            return normalized;
        }

        /// <summary>
        /// Return a copy of the input with the noise floor subtracted from it.
        /// Noise floor is calculated as the mean of the quietest 20% of values.
        /// </summary>
        public static double[] Method2_QuicksortFloor(double[] input, int windowSizePx = 10, double percentile = .2, double power = 1)
        {
            double[] normalized = new double[input.Length];

            double[] segment = new double[windowSizePx * 2 + 1];
            int valuesInFloor = (int)(percentile * segment.Length);

            for (int i = windowSizePx; i < input.Length - windowSizePx; i++)
            {
                for (int j = 0; j < segment.Length; j++)
                    segment[j] = input[i + j - windowSizePx];

                Array.Sort(segment);

                double meanFloor = segment.Take(valuesInFloor).Sum() / valuesInFloor;
                normalized[i] = Math.Max(input[i] - meanFloor * power, 0);
            }

            return normalized;
        }

        /// <summary>
        /// Return the percentile of the given array
        /// </summary>
        /// <param name="values"></param>
        /// <param name="percentile">number from 0 to 100</param>
        /// <returns></returns>
        private static double Percentile(double[] values, int percentile) => Quantile(values, percentile, 100);

        /// <summary>
        /// Return the Nth smallest value in the given array.
        /// </summary>
        private static double NthOrderStatistic(double[] values, int n)
        {
            if (n < 1 || n > values.Length)
                throw new ArgumentException("n must be a number from 1 to the length of the array");

            double[] valuesCopy = new double[values.Length];
            Array.Copy(values, valuesCopy, values.Length);
            return QuickSelect(valuesCopy, 0, values.Length - 1, n - 1);
        }

        /// <summary>
        /// Return the value of the Nth quantile.
        /// </summary>
        private static double Quantile(double[] values, int n, int quantileCount)
        {
            if (n == 0)
                return values.Min();
            else if (n == quantileCount)
                return values.Max();
            else
                return NthOrderStatistic(values, n * values.Length / quantileCount);
        }

        /// <summary>
        /// Return the kth smallest value from a range of the given array.
        /// WARNING: values will be mutated.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="leftIndex">inclusive lower bound</param>
        /// <param name="rightIndex">inclusive upper bound</param>
        /// <param name="k">number starting at 0</param>
        /// <returns></returns>
        private static double QuickSelect(double[] values, int leftIndex, int rightIndex, int k)
        {
            /*
             * QuickSelect (aka Hoare's Algorithm) is a selection algorithm 
             *  - Given an integer k it returns the kth smallest element in a sequence) with O(n) expected time.
             *  - In the worst case it is O(n^2), i.e. when the chosen pivot is always the max or min at each call.
             *  - The use of a random pivot virtually assures linear time performance.
             *  - https://en.wikipedia.org/wiki/Quickselect
             */

            if (leftIndex == rightIndex)
                return values[leftIndex];

            if (k == 0)
            {
                double min = values[leftIndex];
                for (int j = leftIndex; j <= rightIndex; j++)
                {
                    if (values[j] < min)
                    {
                        min = values[j];
                    }
                }

                return min;
            }

            if (k == rightIndex - leftIndex)
            {
                double max = values[leftIndex];
                for (int j = leftIndex; j <= rightIndex; j++)
                {
                    if (values[j] > max)
                    {
                        max = values[j];
                    }
                }

                return max;
            }

            int partitionIndex = Partition(values, leftIndex, rightIndex);
            int pivotIndex = partitionIndex - leftIndex;

            if (k == pivotIndex)
                return values[partitionIndex];
            else if (k < pivotIndex)
                return QuickSelect(values, leftIndex, partitionIndex - 1, k);
            else
                return QuickSelect(values, partitionIndex + 1, rightIndex, k - pivotIndex - 1);
        }

        /// <summary>
        /// Partition the array between the defined bounds according to elements above and below a randomly chosen pivot value
        /// </summary>
        /// <param name="values"></param>
        /// <param name="leftIndex"></param>
        /// <param name="rightIndex"></param>
        /// <returns>index of the pivot used</returns>
        private static int Partition(double[] values, int leftIndex, int rightIndex)
        {
            // Moving the pivot to the end is far easier than handling it where it is
            // This also allows you to turn this into the non-randomized Partition
            int initialPivotIndex = Rand.Next(leftIndex, rightIndex + 1);
            double swap = values[initialPivotIndex];
            values[initialPivotIndex] = values[rightIndex];
            values[rightIndex] = swap;

            double pivotValue = values[rightIndex];

            int pivotIndex = leftIndex - 1;
            for (int j = leftIndex; j < rightIndex; j++)
            {
                if (values[j] <= pivotValue)
                {
                    pivotIndex++;
                    double tmp1 = values[j];
                    values[j] = values[pivotIndex];
                    values[pivotIndex] = tmp1;
                }
            }

            pivotIndex++;
            double tmp2 = values[rightIndex];
            values[rightIndex] = values[pivotIndex];
            values[pivotIndex] = tmp2;

            return pivotIndex;
        }
    }
}
