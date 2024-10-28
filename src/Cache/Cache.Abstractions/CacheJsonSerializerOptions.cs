using System.Text.Json;

namespace OpenModular.Cache.Abstractions;

public sealed class CacheJsonSerializerOptions
{
    public static JsonSerializerOptions? Options { get; set; } = null;
}