using System.Text.Json;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.Abstractions;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Configuration.Abstractions;
using OpenModular.Cache.Abstractions;

namespace OpenModular.Module.UAP.Core.Domain.Configs;

internal class ConfigProvider : IConfigProvider, ITransientDependency
{
    private readonly UAPCache _cache;
    private readonly IConfigRepository _repository;
    private readonly IModuleCollection _modules;
    private const int Duration = 30;//30 day

    public ConfigProvider(UAPCache cache, IConfigRepository repository, IModuleCollection modules)
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

        return _cache.GetOrSetAsync<TConfig>(cacheKey, token => GetFromRepositoryAsync<TConfig>(module, token), TimeSpan.FromDays(Duration));
    }

    public async ValueTask<object?> GetAsync(Type configType)
    {
        var module = _modules.FirstOrDefault(m => m.Config != null && m.Config.ConfigType == configType);
        if (module == null)
        {
            return default!;
        }

        var cacheKey = UAPCacheKeys.Config(module.Module.Code);

        var cacheValue = await _cache.GetOrSetAsync(cacheKey,
            token => GetFromRepositoryAsync(module, configType, token), TimeSpan.FromDays(Duration));

        if (cacheValue != null && cacheValue is JsonElement jsonElement)
        {
            return JsonSerializer.Deserialize(jsonElement.GetRawText(), configType, CacheJsonSerializerOptions.Options);
        }

        return cacheValue;
    }

    public async ValueTask SetAsync<TConfig>(TConfig config) where TConfig : IConfig, new()
    {
        var module = _modules.FirstOrDefault(m => m.Config != null && m.Config.ConfigType == typeof(TConfig));
        if (module == null)
        {
            return;
        }

        var configItem = await _repository.FindAsync(m => m.ModuleCode == module.Module.Code);
        if (configItem != null)
        {
            configItem.Value = config.ToJson();

            await _repository.UpdateAsync(configItem);
        }
        else
        {
            configItem = Config.Create(module.Module.Code, config.ToJson());
            await _repository.InsertAsync(configItem);
        }

        var cacheKey = UAPCacheKeys.Config(module.Module.Code);

        await _cache.SetAsync(cacheKey, config, TimeSpan.FromDays(Duration));
    }

    private async Task<TConfig> GetFromRepositoryAsync<TConfig>(IModuleDescriptor module, CancellationToken cancellationToken) where TConfig : IConfig, new()
    {
        var config = await _repository.FindAsync(m => m.ModuleCode == module.Module.Code, cancellationToken);

        if (config == null)
        {
            return new TConfig();
        }

        return config.Value.ToModel<TConfig>()!;
    }

    private async Task<object?> GetFromRepositoryAsync(IModuleDescriptor module, Type configType, CancellationToken cancellationToken)
    {
        var config = await _repository.FindAsync(m => m.ModuleCode == module.Module.Code, cancellationToken);

        if (config == null)
        {
            return Activator.CreateInstance(configType);
        }

        return config.Value.ToModel(configType)!;
    }
}