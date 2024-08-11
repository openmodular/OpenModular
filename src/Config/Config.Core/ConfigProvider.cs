using OpenModular.Config.Abstractions;

namespace OpenModular.Config.Core;

internal class ConfigProvider : IConfigProvider
{
    private readonly IConfigStorage _storage;
    private readonly 

    public ConfigProvider(IConfigStorage storage)
    {
        _storage = storage;
    }

    public Task<TConfig> GetAsync<TConfig>() where TConfig : IConfig
    {
        throw new NotImplementedException();
    }

    public Task SetAsync<TConfig>(TConfig config) where TConfig : IConfig
    {
        throw new NotImplementedException();
    }
}