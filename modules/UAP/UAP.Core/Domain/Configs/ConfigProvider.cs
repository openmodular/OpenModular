using Microsoft.Extensions.Configuration;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.Abstractions;
using OpenModular.Module.UAP.Core.Conventions;
using System.Reflection;
using OpenModular.Configuration.Abstractions;

namespace OpenModular.Module.UAP.Core.Domain.Configs;

internal class ConfigProvider : IConfigProvider, ITransientDependency
{
    private readonly UAPCacheProvider _cache;
    private readonly IConfigRepository _repository;
    private readonly IModuleCollection _modules;

    public ConfigProvider(UAPCacheProvider cache, IConfigRepository repository, IModuleCollection modules)
    {
        _cache = cache;
        _repository = repository;
        _modules = modules;
    }

    public ValueTask<TConfig> GetAsync<TConfig>() where TConfig : IConfig, new()
    {
        var module = _modules.FirstOrDefault(m => m.Config != null && m.Config.ConfigType == typeof(TConfig));
        if (module == null)
        {
            return default!;
        }

        var cacheKey = UAPCacheKeys.Config(module.Module.Code);

        return _cache.GetOrSetAsync(cacheKey, token => GetFromRepositoryAsync<TConfig>(module, token), TimeSpan.FromDays(7));
    }

    public ValueTask<object?> GetAsync(Type configType)
    {
        var module = _modules.FirstOrDefault(m => m.Config != null && m.Config.ConfigType == configType);
        if (module == null)
        {
            return default!;
        }

        var cacheKey = UAPCacheKeys.Config(module.Module.Code);

        return _cache.GetOrSetAsync(cacheKey, token => GetFromRepositoryAsync(module, configType, token), TimeSpan.FromDays(7));
    }

    public async ValueTask SetAsync<TConfig>(TConfig config) where TConfig : IConfig, new()
    {
        var module = _modules.FirstOrDefault(m => m.Config != null && m.Config.ConfigType == typeof(TConfig));
        if (module == null)
        {
            return;
        }

        var dic = new Dictionary<string, string>();
        Object2Dictionary(config, dic, string.Empty);

        foreach (var d in dic)
        {
            var configItem = await _repository.FindAsync(m => m.ModuleCode == module.Module.Code && m.Key == d.Key);
            if (configItem != null)
            {
                configItem.Value = d.Value;

                await _repository.UpdateAsync(configItem);
            }
            else
            {
                configItem = Config.Create(module.Module.Code, d.Key, d.Value);
                await _repository.InsertAsync(configItem);
            }
        }

        var cacheKey = UAPCacheKeys.Config(module.Module.Code);

        await _cache.SetAsync(cacheKey, config, TimeSpan.FromDays(7));
    }

    private async Task<TConfig> GetFromRepositoryAsync<TConfig>(IModuleDescriptor module, CancellationToken cancellationToken) where TConfig : IConfig, new()
    {
        var config = new TConfig();

        var configItems = await _repository.GetListAsync(m => m.ModuleCode == module.Module.Code, cancellationToken);

        if (!configItems.Any())
        {
            return config;
        }

        var builder = new ConfigurationBuilder().AddInMemoryCollection(configItems.Select(m => new KeyValuePair<string, string?>(m.Key, m.Value)));

        builder.Build().Bind(config);

        return config;
    }

    private async Task<object?> GetFromRepositoryAsync(IModuleDescriptor module, Type configType, CancellationToken cancellationToken)
    {
        var config = Activator.CreateInstance(configType);

        var configItems = await _repository.GetListAsync(m => m.ModuleCode == module.Module.Code, cancellationToken);

        if (!configItems.Any())
        {
            return config;
        }

        var builder = new ConfigurationBuilder().AddInMemoryCollection(configItems.Select(m => new KeyValuePair<string, string?>(m.Key, m.Value)));

        builder.Build().Bind(config);

        return config;
    }

    private void Object2Dictionary(object obj, Dictionary<string, string> configDictionary, string parentKey)
    {
        var objType = obj.GetType();
        foreach (var property in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var key = parentKey.IsNullOrWhiteSpace() ? property.Name : $"{parentKey}:{property.Name}";
            var value = property.GetValue(obj);

            if (value != null)
            {
                if (IsSimpleType(property.PropertyType))
                {

                    configDictionary[key] = value.ToString() ?? string.Empty;
                }
                else
                {
                    Object2Dictionary(value, configDictionary, key);
                }
            }
        }
    }

    private bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal);
    }
}