using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    public static class EnumExtensions
    {
        public static string[] GetNames(this Enum @enum)
        {
            return Enum.GetNames(@enum.GetType());
        }

        public static int[] GetValues(this Enum @enum)
        {
            return Enum.GetValues(@enum.GetType()) as int[];
        }

        public static List<string> ToList(this Enum @enum)
        {
            return new List<string>(Enum.GetNames(@enum.GetType()));
        }

        public static IDictionary<string, int> ToDictionary(this Enum @enum)
        {
            //var names = Enum.GetNames(@enum.GetType());
            //var values = Enum.GetValues(@enum.GetType()); 

            var names = @enum.GetNames();
            var values = @enum.GetValues();
            var data = from i in Enumerable.Range(0, names.Length) select (Key: names[i], Value: values[i]);

            return data.ToDictionary(k => k.Key, k => k.Value);
        }


        public static string Description(this Enum @enum)
        {
            var memInfo = @enum.GetType().GetMember(@enum.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return @enum.ToString();
        }

        public static bool HasDescription(this Enum @enum)
        {
            return !string.IsNullOrWhiteSpace(@enum.Description());
        }

        public static bool HasDescription(this Enum @enum, string expectedDescription)
        {
            return @enum.Description().Equals(expectedDescription);
        }

        public static bool HasDescription(this Enum @enum, string expectedDescription, StringComparison comparisionType)
        {
            return @enum.Description().Equals(expectedDescription, comparisionType);
        }

        private static Regex UpperCamelCaseRegex = new Regex(@"(?<!^)((?<!\d)\d|(?(?<=[A-Z])[A-Z](?=[a-z])|[A-Z]))", RegexOptions.Compiled);

        public static string AsUpperCamelCaseName(this Enum @enum)
        {
            return UpperCamelCaseRegex.Replace(@enum.ToString(), " $1");
        }
    }
}
