using System.Linq;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static string ToQueryString(this Dictionary<string, object> source)
        {
            return string.Join("&", source
                .Where(p => !string.IsNullOrEmpty(p.Key) && !string.IsNullOrEmpty(p.Value?.ToString()))
                .Select(p => $"{p.Key}={p.Value}"));
        }

        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePair(this Dictionary<string, object> source)
        {
            foreach (var x in source)
            {
                if (x.Value != null)
                {
                    yield return new KeyValuePair<string, string>(x.Key, x.Value.ToString());
                }
            }
        }

        public static string GetStringValue(this IDictionary<string, string> source, string key, bool throwEx = false)
        {
            try
            {
                source.TryGetValue(key, out var str);
                return str;
            }
            catch (Exception)
            {
                if (throwEx) throw;
                //ignored
                return null;
            }
        }
    }
}
