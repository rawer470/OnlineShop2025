using System;
using System.Text.Json;

namespace OnlineShop.Utility;

public static class SessionsExtensions
{

    public static void Set<T>(this ISession session, string key, T value)  // T - generic type
    {
        session.SetString(key, JsonSerializer.Serialize<T>(value));
    }

  public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }


}
