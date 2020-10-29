using System.Text.Json;

namespace Microsoft.AspNetCore.Http
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] data);

            return data == null ? default : JsonSerializer.Deserialize<T>(data);
        }
    }
}