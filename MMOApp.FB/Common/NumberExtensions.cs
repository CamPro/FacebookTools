using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSS.Patterns.Extensions
{
    public static class NumberExtensions
    {
        public static bool InRange(this int value, int min, int max)
        {
            if (min > max)   
                throw new ArgumentException("Min value must less than max value", "min");

            return min <= value && value <= max;
        }

        public static bool InRange(this long value, long min, long max)
        {
            if (min > max)
                throw new ArgumentException("Min value must less than max value", "min");

            return min <= value && value <= max;
        }
    }
}
