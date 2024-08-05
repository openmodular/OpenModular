namespace OpenModular.Cache.Abstractions;

public abstract class CacheKeysAbstract : ICacheKeys
{
    protected CacheKeysAbstract(string moduleCode)
    {
        ModuleCode = moduleCode;
    }

    public string ModuleCode { get; }
}