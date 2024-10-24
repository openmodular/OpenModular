using System.Text.Json.Serialization;
using System.Text.Json;

namespace OpenModular.DDD.Core.Domain.Entities.TypeIds;

/// <summary>
/// 租户标识
/// </summary>
public class TenantId : TypedIdValueBase
{
    public TenantId()
    {

    }

    public TenantId(string id) : base(id)
    {

    }

    public TenantId(Guid id) : base(id)
    {

    }
}

public class TenantIdJsonConverter : JsonConverter<TenantId>
{
    public override TenantId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null ? new TenantId(value) : null;
    }

    public override void Write(Utf8JsonWriter writer, TenantId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}