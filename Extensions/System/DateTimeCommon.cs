using System.Globalization;

namespace System
{
    internal static class DateTimeCommon
    {
        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        internal static readonly string[] Formats = {
            @"ddd, d MMM yyyy HH:mm:ss tt \C\S\T",
            @"ddd, d MMM yyyy HH:mm:ss \C\S\T",
            @"ddd, d MMM yyyy HH:mm \C\S\T",

            @"ddd, d MMM yyyy HH:mm:ss tt \E\S\T",
            @"ddd, d MMM yyyy HH:mm:ss \E\S\T",
            @"ddd, d MMM yyyy HH:mm \E\S\T",

            @"ddd, d MMM yyyy HH:mm:ss tt \E\D\T",
            @"ddd, d MMM yyyy HH:mm:ss \E\D\T",
            @"ddd, d MMM yyyy HH:mm \E\D\T",

            // Wed, 16 Sep 2020 20:48:21 PDT
            
            @"ddd, d MMM yyyy HH:mm \P\D\T",

            @"yyyy-MM-dd\Thh:mm:ss\Z"
        };

        internal static readonly DateTimeStyles Styles = DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowWhiteSpaces;
    }
}