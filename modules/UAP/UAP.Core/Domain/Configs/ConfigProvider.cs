using Microsoft.Extensions.Configuration;
using OpenModular.Config.Abstractions;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.Abstractions;
using OpenModular.Module.UAP.Core.Conventions;

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

    public async Task<TConfig> GetAsync<TConfig>() where TConfig : IConfig, new()
    {
        var module = _modules.FirstOrDefault(m => m.Config != null && m.Config.ConfigType == typeof(TConfig));
        if (module == null)
        {
            return default!;
        }

        var cacheKey = UAPCacheKeys.Config(module.Module.Code);

        var config = await _cache.GetOrSetAsync<TConfig>(cacheKey,);
    }

    public Task<IConfig> GetAsync(Type configType)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync<TConfig>(TConfig config) where TConfig : IConfig, new()
    {
        throw new NotImplementedException();
    }

    private async Task<TConfig> GetFromRepositoryAsync<TConfig>(IModuleDescriptor module) where TConfig : IConfig, new()
    {
        var config = new TConfig();

        var configItems = await _repository.GetListAsync(m => m.ModuleCode == module.Module.Code);

        if (!configItems.Any())
        {
            return config;
        }

        var builder = new ConfigurationBuilder().AddInMemoryCollection(configItems.Select(m => new KeyValuePair<string, string>(m.Key, m.Value)));
        
        builder.Build().Bind(config);

        return config;
    }
}