using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }


        public static string UrlDecode(this string str)
        {
            return str.IsNullOrEmpty() ? str : WebUtility.UrlDecode(str);
        }

        public static string UrlEncode(this string str)
        {
            return str.IsNullOrEmpty() ? str : WebUtility.UrlEncode(str);
        }

        public static string HtmlDecode(this string str)
        {
            return str.IsNullOrEmpty() ? str : WebUtility.HtmlDecode(str).Trim();
        }

        public static string HtmlEncode(this string str)
        {
            return str.IsNullOrEmpty() ? str : WebUtility.HtmlEncode(str);
        }


        public static string TrimUrl(this string str)
        {
            return str.Trim().TrimEnd('/');
        }

        public static string Reverse(this string str)
        {
            return new string(str.ToCharArray().Reverse().ToArray());
        }

        public static string SplitByTitleCase(this string str)
        {
            return Regex.Replace(str, "([a-z])([A-Z])", "$1 $2");
        }

        public static string RemoveHtmlTags(this string str)
        {
            return Regex.Replace(str, "<.*?>", string.Empty, RegexOptions.Multiline)?.Trim().HtmlDecode();
        }

        //public static string RemoveAccent(this string text)
        //{
        //    var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
        //    return Encoding.ASCII.GetString(bytes);
        //}

        public static string RemoveDiacritics(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            str = str.Normalize(NormalizationForm.FormD);
            var chars = str.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string GenerateSlug(this string str, int maxLength = -1)
        {
            str = str.RemoveDiacritics().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            str = Regex.Replace(str, @"-", " ");

            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 


            //if (maxLength > 0)
            //{
            //    str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
            //}

            str = str.MaxLength(maxLength);

            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string MaxLength(this string str, int maxLength = -1)
        { 
            if (maxLength > 0)
            {
                str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength);//.Trim();
            }   

            return str;
        }

        public static string ToTitleCase(this string str)
        {
            CultureInfo cultureInfo = Threading.Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(str);
        }


        /// <summary>
        /// Adds spaces to words based on capital characters.
        /// </summary>
        /// <param name="text">The CamelCase string you want spaces added to.</param>
        /// <returns>A clean string with spaces added between CamelCase words.</returns>
        public static string AddSpacesToWord(this string str)
        {
            if (str.IsNullOrEmpty()) return string.Empty;

            //var str = text;

            for (var i = 0; i < str.Length; i++)
            {
                if (!char.IsUpper(str[i]) || i == 0) continue;
                str = str.Insert(i, " ");

                //incremement the counter because we just added a space
                //in front of the upper case character
                i++;
            }

            return str;
        }

        /// <summary>
        /// Creates a string with all spaces removed.
        /// </summary>
        /// <param name="text">The string you want to remove spaces from.</param>
        /// <returns>A string with all words squished together.</returns>
        public static string RemoveSpacesFromWords(this string str)
        {
            return str.Replace(" ", "");
        }

        private readonly static Regex domainRegex = new Regex(@"(((?<scheme>http(s)?):\/\/)?([\w-]+?\.\w+)+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\,]*)?)", RegexOptions.Compiled | RegexOptions.Multiline);

        public static string Linkify(this string str, string target = "_self")
        {
            return domainRegex.Replace(str, match =>
            {
                var link = match.ToString();
                var scheme = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;
                var url = new UriBuilder(link) { Scheme = scheme }.Uri.ToString();

                return $@"<a href=""{url}"" target=""{target}"">{link}</a>";
            });
        }

        /// <summary>
        ///     Convert url query string to IDictionary value key pair
        /// </summary>
        /// <param name="str">query string value</param>
        /// <returns>IDictionary value key pair</returns>
        public static IDictionary<string, string> QueryStringToDictionary(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            if (!str.Contains("?"))
            {
                return null;
            }

            string query = str.Replace("?", "");
            if (!query.Contains("="))
            {
                return null;
            }

            return query.Split('&').Select(p => p.Split('=')).ToDictionary(key => key[0].ToLower().Trim(), value => value[1]);
        }

        /// <summary>
        ///     Check a String ends with another string ignoring the case.
        /// </summary>
        /// <param name="str">string</param>
        /// <param name="suffix">suffix</param>
        /// <returns>true or false</returns>
        public static bool EndsWithIgnoreCase(this string str, string suffix)
        {
            if (str == null)
            {
                throw new ArgumentNullException("val", "val parameter is null");
            }
            if (suffix == null)
            {
                throw new ArgumentNullException("suffix", "suffix parameter is null");
            }
            if (str.Length < suffix.Length)
            {
                return false;
            }
            return str.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Check a String starts with another string ignoring the case.
        /// </summary>
        /// <param name="str">string</param>
        /// <param name="prefix">prefix</param>
        /// <returns>true or false</returns>
        public static bool StartsWithIgnoreCase(this string str, string prefix)
        {
            if (str == null)
            {
                throw new ArgumentNullException("val", "val parameter is null");
            }
            if (prefix == null)
            {
                throw new ArgumentNullException("prefix", "prefix parameter is null");
            }
            if (str.Length < prefix.Length)
            {
                return false;
            }
            return str.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
        }


        /// <summary>
        ///     Appends the prefix to the start of the string if the string does not already start with prefix.
        /// </summary>
        /// <param name="str">string to append prefix</param>
        /// <param name="prefix">prefix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns></returns>
        public static string AppendPrefixIfMissing(this string str, string prefix, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(str) || (ignoreCase ? str.StartsWithIgnoreCase(prefix) : str.StartsWith(prefix)))
            {
                return str;
            }
            return prefix + str;
        }

        /// <summary>
        ///     Appends the suffix to the end of the string if the string does not already end in the suffix.
        /// </summary>
        /// <param name="str">string to append suffix</param>
        /// <param name="suffix">suffix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns></returns>
        public static string AppendSuffixIfMissing(this string str, string suffix, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(str) || (ignoreCase ? str.EndsWithIgnoreCase(suffix) : str.EndsWith(suffix)))
            {
                return str;
            }
            return str + suffix;
        }

        /// <summary>
        ///     Removes the first part of the string, if no match found return original string
        /// </summary>
        /// <param name="str">string to remove prefix</param>
        /// <param name="prefix">prefix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns>trimmed string with no prefix or original string</returns>
        public static string RemovePrefix(this string str, string prefix, bool ignoreCase = true)
        {
            if (!string.IsNullOrEmpty(str) && (ignoreCase ? str.StartsWithIgnoreCase(prefix) : str.StartsWith(prefix)))
            {
                return str.Substring(prefix.Length, str.Length - prefix.Length);
            }
            return str;
        }

        /// <summary>
        ///     Removes the end part of the string, if no match found return original string
        /// </summary>
        /// <param name="str">string to remove suffix</param>
        /// <param name="suffix">suffix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns>trimmed string with no suffix or original string</returns>
        public static string RemoveSuffix(this string str, string suffix, bool ignoreCase = true)
        {
            if (!string.IsNullOrEmpty(str) && (ignoreCase ? str.EndsWithIgnoreCase(suffix) : str.EndsWith(suffix)))
            {
                return str.Substring(0, str.Length - suffix.Length);
            }
            return null;
        }

        /// <summary>
        ///     Convert a string to its equivalent byte array
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>System.byte array</returns>
        public static byte[] ToBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
        const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
        const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />

        public static string HtmlToPlainText(this string str)
        {
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            //   var text = str;
            //Decode html specific characters
            str = WebUtility.HtmlDecode(str);
            //Remove tag whitespace/line breaks
            str = tagWhiteSpaceRegex.Replace(str, "><");
            //Replace <br /> with line breaks
            str = lineBreakRegex.Replace(str, Environment.NewLine);
            //Strip formatting
            str = stripFormattingRegex.Replace(str, string.Empty);

            return str;
        }

        public static string UrlToLink(this string str)
        {
            var patten = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[_.a-z0-9-]+\.[a-z0-9\/_:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
            var regex = new Regex(patten, RegexOptions.IgnoreCase);
            return regex.Replace(str, "<a href=\"$1\" title=\"Click here to open in a new window or tab\" target =\"_blank\">$1</a>").Replace("href=\"www", "href=\"http://www");

        }

        public static string SimpleTextToHtml(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            str = WebUtility.HtmlEncode(str);
            str = str.Replace("\r\n", "\r");
            str = str.Replace("\n", "\r");
            str = str.Replace("\r", "<br>\r\n");
            str = str.Replace("  ", " &nbsp;");

            return str;
        }

        public static string ToHtml(this string str)
        {
            return str.HtmlDecode()?.SimpleTextToHtml()?.UrlToLink();
        }



        public static string StripHtml(this string str)
        {
            return str.IsNullOrEmpty()
                ? str
                : Regex.Replace(str, "<.*?>", string.Empty, RegexOptions.Multiline).Trim().HtmlDecode();
        }

        public static string ToAbsoluteUri(this string str, string baseUrl)
        {
            var test = new Uri(str, UriKind.RelativeOrAbsolute);

            if (test.IsAbsoluteUri)
            {
                return str;
            }

            var uri = new Uri(baseUrl);

            var temp = $"{uri.Scheme}://{uri.Host}";

            return $"{temp}/{str}";
        }

    }
}
