using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeSeconds(this DateTime target)
        {
            return (long)(target - UnixEpoch).TotalSeconds;
        }

        public static long ToUnixTimeMilliseconds(this DateTime target)
        {
            return (long)(target - UnixEpoch).TotalMilliseconds;
        }

        public static long ToUnixTotalMinutes(this DateTime target)
        {
            return (long)(target - UnixEpoch).TotalMinutes;
        }

        public static bool IsBetween(this DateTime value, DateTime from, DateTime to)
        {
            return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
        }

        public static IEnumerable<DateTime> GetDateRangeTo(this DateTime self, DateTime toDate)
        {
            var range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - self.Ticks).Days);

            return range.Select(x => self.AddDays(x)); 
        }

        public static DateTime ParseDateTime(this string dateTime)
        {
            if (!DateTime.TryParse(dateTime, out DateTime dt))
            {
                if (!DateTime.TryParseExact(dateTime, DateTimeCommon.Formats, CultureInfo.InvariantCulture, DateTimeCommon.Styles, out dt))
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
