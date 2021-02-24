using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace escout.Helpers
{
    public static class Utils
    {
        public static string StringGenerator()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateSha256String(string inputString)
        {
            var sb = new StringBuilder();
            using var hash = SHA256.Create();
            var enc = Encoding.UTF8;
            var result = hash.ComputeHash(enc.GetBytes(inputString));

            foreach (var b in result)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }

        public static string GetDateTime()
        {
            return DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");
        }

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

        public static double Median(int[] numbers)
        {
            double median = 0.0;

            try
            {
                int numberCount = numbers.Count();
                int halfIndex = numbers.Count() / 2;
                var sortedNumbers = numbers.OrderBy(n => n);

                if ((numberCount % 2) == 0)
                {
                    median = ((sortedNumbers.ElementAt(halfIndex) +
                        sortedNumbers.ElementAt((halfIndex - 1))) / 2);
                }
                else
                {
                    median = sortedNumbers.ElementAt(halfIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return median;
        }
    }
}
