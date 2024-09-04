using System.Reflection;
using OpenModular.Configuration.Abstractions;

namespace OpenModular.Configuration.Core;

internal class ConfigDescriptor : IConfigDescriptor
{
    public string ModuleCode { get; }

    public Type ConfigType { get; }

    public List<ConfigKey> Keys { get; }

    public ConfigDescriptor(string moduleCode, Type configType)
    {
        ModuleCode = moduleCode;
        ConfigType = configType;
        Keys = new List<ConfigKey>();

        LoadKeys(configType, moduleCode);
    }

    private void LoadKeys(Type type, string parentName)
    {
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            var keyName = parentName == null ? property.Name : $"{parentName}.{property.Name}";
            var key = new ConfigKey
            {
                Name = keyName,
                Property = property
            };

            Keys.Add(key);

            var propertyType = property.PropertyType;

            if (propertyType.IsClass && propertyType != typeof(string) && !propertyType.Namespace!.StartsWith("System"))
            {
                LoadKeys(property.PropertyType, keyName);
            }
        }
    }
}