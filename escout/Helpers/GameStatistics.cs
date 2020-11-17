using System;

namespace escout.Helpers
{
    public static class GameStatistics
    {
        public static double StdDev(int[] values)
        {
            double mean = 0.0;
            double sum = 0.0;
            double stdDev = 0.0;
            int n = 0;

            try
            {
                foreach (double val in values)
                {
                    n++;
                    double delta = val - mean;
                    mean += delta / n;
                    sum += delta * (val - mean);
                }
                if (1 < n)
                    stdDev = Math.Sqrt(sum / (n - 1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return stdDev;
        }

        public static double Median(int[] values)
        {
            double median = 0.0;

            try
            {
                if (values == null || values.Length == 0)
                    Console.WriteLine("Median of empty array not defined.");

                //make sure the list is sorted, but use a new array
                double[] sortedPNumbers = (double[])values.Clone();
                Array.Sort(sortedPNumbers);

                //get the median
                int size = sortedPNumbers.Length;
                int mid = size / 2;
                median = (size % 2 != 0) ? (double)sortedPNumbers[mid] : ((double)sortedPNumbers[mid] + (double)sortedPNumbers[mid - 1]) / 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return median;
        }
    }
}
