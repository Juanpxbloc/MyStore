using System.Text.Json;

namespace MyStore.Utilities
{
    public static class SessionExtensions
    {

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}

// This static class provides extension methods for the ISession interface to store and retrieve complex objects using JSON serialization.