namespace OpenModular.Module.Blazor.Skins;

/// <summary>
/// Interface for skin.
/// </summary>
public interface ISkin
{
    /// <summary>
    /// Skin name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Skin unique code.
    /// </summary>
    string Code { get; }

    /// <summary>
    /// Skin version.
    /// </summary>
    string Version { get; }

    /// <summary>
    /// Skin component type.
    /// </summary>
    Type ComponentType { get; set; }
}