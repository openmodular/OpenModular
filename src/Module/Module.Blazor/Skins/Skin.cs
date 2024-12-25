namespace OpenModular.Module.Blazor.Skins;

public abstract class Skin : ISkin
{
    protected Skin(string name, string code, Type componentType)
    {
        Name = name;
        Code = code;
        Version = componentType.Assembly.GetName().Version?.ToString() ?? "1.0.0";
        ComponentType = componentType;
    }

    public string Name { get; }

    public string Code { get; }

    public string Version { get; }

    public Type ComponentType { get; set; }
}