namespace OpenModular.Module.Blazor;

public abstract class ModuleBlazorAbstract : IModuleBlazor
{
    protected ModuleBlazorAbstract(int id, string code)
    {
        Id = id;
        Code = code;
        Version = GetType().Assembly.GetName().Version?.ToString() ?? "1.0.0";
    }

    public int Id { get; }

    public string Code { get; }

    public string Version { get; }
}