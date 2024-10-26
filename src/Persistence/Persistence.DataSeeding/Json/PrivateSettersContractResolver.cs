using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenModular.Persistence.DataSeeding.Json;

public class PrivateSettersContractResolver : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Length > 0;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(PrivateSettersConverter<>).MakeGenericType(typeToConvert);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private class PrivateSettersConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert.IsPrimitive || typeToConvert == typeof(string) || typeToConvert == typeof(decimal))
            {
                return (T)Convert.ChangeType(reader.GetString(), typeToConvert)!;
            }

            if (typeToConvert == typeof(DateTimeOffset))
            {
                return (T)(object)reader.GetDateTimeOffset();
            }

            if (typeToConvert == typeof(Guid))
            {
                return (T)(object)reader.GetGuid();
            }

            var instance = Activator.CreateInstance(typeToConvert, true);
            var jsonDocument = JsonDocument.ParseValue(ref reader);
            foreach (var property in jsonDocument.RootElement.EnumerateObject())
            {
                var propertyInfo = typeToConvert.GetProperty(property.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (propertyInfo == null)
                    continue;

                var value = JsonSerializer.Deserialize(property.Value.GetRawText(), propertyInfo.PropertyType, options);
                if (propertyInfo.SetMethod != null)
                {
                    propertyInfo.SetValue(instance, value);
                }
                else
                {
                    var fieldInfo = GetFieldInfo(typeToConvert, property.Name);
                    if (fieldInfo != null)
                    {
                        fieldInfo.SetValue(instance, value);
                    }
                }
            }
            return (T)instance!;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }

        private FieldInfo? GetFieldInfo(Type type, string propertyName)
        {
            var fieldInfo = type.GetField($"<{propertyName}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo == null && type.BaseType != null)
            {
                return GetFieldInfo(type.BaseType, propertyName);
            }

            return fieldInfo;
        }
    }
}