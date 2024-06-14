using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace OpenModular.Common.Utils.Json;

public static class JsonSerializerOptionsExtensions
{
    public static void AddCommonUtilsJsonSerializerContext(this JsonSerializerOptions options)
    {
        options.TypeInfoResolverChain.Insert(0, CommonUtilsJsonSerializerContext.Default);
    }

    /// <summary>
    /// 从指定程序集中添加JsonTypeInfoResolver
    /// </summary>
    /// <param name="options"></param>
    /// <param name="assembly"></param>
    public static void AddJsonTypeInfoResolverFromAssembly(this JsonSerializerOptions options, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Where(t => typeof(JsonSerializerContext).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in types)
        {
            //反射获取type的静态属性Default
            var defaultProperty = type.GetProperty("Default", BindingFlags.Static | BindingFlags.Public);
            if (defaultProperty != null)
            {
                var defaultValue = defaultProperty.GetValue(null);
                if (defaultValue is IJsonTypeInfoResolver value)
                {
                    options.TypeInfoResolverChain.Add(value);
                }
            }
        }
    }
}