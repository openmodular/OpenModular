using System.Text.Json.Serialization;

namespace OpenModular.Common.Utils.Json;

[JsonSerializable(typeof(Guid))]
internal partial class CommonUtilsJsonSerializerContext : JsonSerializerContext;