
namespace Konvolucio.MTOO180701.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;


    public class SuffixFormatter : IFormatProvider, ICustomFormatter
    { 
        static Dictionary<int, string> SuffixesDic = new Dictionary<int, string>
        { /*Key Value */
           { -4, "p" },
           { -3, "n" },
           { -2, "u" },
           { -1 ,"m" },
           { 0 ,"" },
           { 1 ,"K" },
           { 2 ,"M" },
           { 3 ,"G" },
           { 4 ,"T" },
        };

        public static string Suffix(double value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + Suffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "}", 0); }

            // mag is 0 for Ω, 1 for KΩ, 2, for MΩ, etc.
            var mag = Math.Round( Math.Log(value, 1000));

            double adjustedSize = 0;
            if (mag > 0)
            {
                /*5000 => 5*/
                /*50000000 =>5*/
                adjustedSize = value / Math.Pow(1000, mag);
            }
            else if (mag == 0)
            {
                ///*5 = 5*/
                adjustedSize = value;
                if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
                {
                    mag += 1;
                    adjustedSize /= 1000;
                }
            }
            else
            {
                /*0.001 => 1 m*/
                adjustedSize = value * Math.Pow(1000, -1 * mag);
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SuffixesDic.FirstOrDefault(n=>n.Key== mag).Value);
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            if (arg == null) return string.Empty;
            if (fmt == "sfx")
                return Suffix(double.Parse(arg.ToString()), 2);
            else if (arg is IFormattable)
                return ((IFormattable)arg).ToString(fmt, CultureInfo.CurrentCulture);
            return arg.ToString();
        }
    }
}
