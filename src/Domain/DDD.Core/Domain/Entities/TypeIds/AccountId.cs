using System.Text.Json.Serialization;
using System.Text.Json;

namespace OpenModular.DDD.Core.Domain.Entities.TypeIds;

/// <summary>
/// 账户标识
/// </summary>
public sealed class AccountId : TypedIdValueBase
{
    public AccountId()
    {

    }

    public AccountId(string id) : base(id)
    {

    }

    public AccountId(Guid id) : base(id)
    {

    }
}

public class AccountIdJsonConverter : JsonConverter<AccountId>
{
    public override AccountId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value != null ? new AccountId(value) : null;
    }

    public override void Write(Utf8JsonWriter writer, AccountId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}