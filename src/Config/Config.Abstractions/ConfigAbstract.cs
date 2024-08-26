namespace OpenModular.Config.Abstractions;

public abstract class ConfigAbstract : IConfig
{
    protected ConfigAbstract(string moduleCode)
    {
        ModuleCode = moduleCode;
    }

    public string ModuleCode { get; }
}