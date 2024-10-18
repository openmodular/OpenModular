using System.Text.Json;

namespace OpenModular.Common.Utils.Extensions;

public static class JsonExtensions
{
    public static JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// 将对象转为JSON字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string? ToJson<T>(this T obj)
    {
        if (obj == null) return null;

        return JsonSerializer.Serialize(obj, Options);
    }

    /// <summary>
    /// 将一个JSON字符串转为对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T? ToModel<T>(this string? obj)
    {
        if (obj == null)
            return default;

        return JsonSerializer.Deserialize<T>(obj, Options);
    }
}