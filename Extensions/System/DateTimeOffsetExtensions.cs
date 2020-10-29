using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System
{
    public static class DateTimeOffsetExtensions
    { 
        public static bool IsBetween(this DateTimeOffset value, DateTimeOffset from, DateTimeOffset to)
        {
            return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
        }
         
        public static IEnumerable<DateTimeOffset> GetDateRangeTo(this DateTimeOffset self, DateTimeOffset toDate)
        {
            var range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - self.Ticks).Days);

            return range.Select(x => self.AddDays(x)); 
        }

        public static DateTimeOffset ParseDateTimeOffset(this string dateTime)
        {
            if (!DateTimeOffset.TryParse(dateTime, out DateTimeOffset dt))
            {
                if (!DateTimeOffset.TryParseExact(dateTime, DateTimeCommon. Formats, CultureInfo.InvariantCulture, DateTimeCommon.Styles, out dt))
                {
                    throw new FormatException("dateTime is not a valid DateTime format.");
                }
            }

            return dt.ToUniversalTime();
        }
    }
}


        //private static readonly string[] DateFormats = {
        //    @"ddd, d MMM yyyy HH:mm:ss tt \C\S\T",
        //    @"ddd, d MMM yyyy HH:mm:ss \C\S\T",
        //    @"ddd, d MMM yyyy HH:mm \C\S\T",

        //    @"ddd, d MMM yyyy HH:mm:ss tt \E\S\T",
        //    @"ddd, d MMM yyyy HH:mm:ss \E\S\T",
        //    @"ddd, d MMM yyyy HH:mm \E\S\T",

        //    @"ddd, d MMM yyyy HH:mm:ss tt \E\D\T",
        //    @"ddd, d MMM yyyy HH:mm:ss \E\D\T",
        //    @"ddd, d MMM yyyy HH:mm \E\D\T",

        //    @"yyyy-MM-dd\Thh:mm:ss\Z"
        //};

        //private static readonly DateTimeStyles _styles = DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowWhiteSpaces;
