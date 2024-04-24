using System;
using System.Collections.Generic;
using System.Text;

namespace KSS.Patterns.Extensions
{
    public static class RandomExtensions
    {
        private const string CHARS = "0123465789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string HEX_CHARS = "abcdefABCDEF";

        public static string NextString(this Random random, int length)
        {
            return random.NextString(length, true);
        }

        public static string NextString(this Random random, int length, bool includeNumber)
        {
            int sourceLength = CHARS.Length;

            StringBuilder destination = new StringBuilder();

            int start = includeNumber ? 0 : 10;

            for (int i = 0; i < length; i++)
            {
                destination.Append(CHARS[random.Next(start, sourceLength - 1)]);
            }

            return destination.ToString();
        }

        public static string AdvancedNextString(this Random random, int length)
        {
            return AdvancedNextString(random, length, false);
        }

        public static string AdvancedNextNumber(this Random random, int length)
        {
            if (length <= 0)
                throw new ArgumentException("Length param must be greater than 0", "length");

            StringBuilder numbers = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                numbers.Append(AdvancedNext(random, 0, 9).ToString());
            }

            return numbers.ToString();
        }

        public static string AdvancedNextHexString(this Random random, int length, bool includeNumber)
        {
            int sourceLength = HEX_CHARS.Length;

            StringBuilder destination = new StringBuilder();

            int start = includeNumber ? 0 : 10;

            for (int i = 0; i < length; i++)
            {
                destination.Append(CHARS[random.AdvancedNext(start, sourceLength - 1)]);
            }

            return destination.ToString();
        }

        public static string AdvancedNextString(this Random random, int length, bool includeNumber)
        {
            int sourceLength = CHARS.Length;

            StringBuilder destination = new StringBuilder();

            int start = includeNumber ? 0 : 10;

            for (int i = 0; i < length; i++)
            {
                destination.Append(CHARS[random.AdvancedNext(start, sourceLength - 1)]);
            }

            return destination.ToString();
        }

        public static bool NextBoolean(this Random random)
        {
            return random.Next(0, 1) == 0 ? false : true;
        }

        public static int AdvancedNext(this Random random, int maxValue)
        {
            return AdvancedNext(random, 0, maxValue);
        }

        public static int AdvancedNext(this Random random, int minValue, int maxValue)
        {
            minValue = Math.Abs(minValue);
            maxValue = Math.Abs(maxValue);
            if (minValue >= maxValue)
                throw new Exception("Invalid parameter");

            byte[] buffer = new byte[4];
            random.AdvancedNextBytes(buffer);
            int value = Math.Abs(BitConverter.ToInt32(buffer, 0));
            if (value > maxValue - minValue)
                value %= Math.Abs(maxValue - minValue);

            return value + minValue;
        }

        public static bool AdvancedNextBoolean(this Random random)
        {
            byte[] buffer = new byte[1];
            random.AdvancedNextBytes(buffer);
            return buffer[0] % 2 == 0;
        }

        public static double AdvancedNextDouble(this Random random)
        {
            byte[] buffer = new byte[8];
            random.AdvancedNextBytes(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        public static DateTime AdvancedNextDate(this Random random, DateTime startDate, DateTime endDate)
        {
            int days = AdvancedNext(random, (int) (endDate - startDate).TotalDays);
            return startDate.AddDays(days);
        }

        private static void AdvancedNextBytes(this Random random, byte[] buffer)
        {
            if (buffer == null | buffer.Length <= 0)
                return;

            int count = 0;
            int length = buffer.Length;
            List<byte> data = new List<byte>();
            while (count < length)
            {
                byte[] genBytes = Guid.NewGuid().ToByteArray();
                foreach (byte t in genBytes)
                {
                    data.Add(t);
                    count++;
                    if (count >= length)
                        break;
                }
            }

            data.CopyTo(buffer);
        }
    }
}