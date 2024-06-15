using Microsoft.Extensions.Localization;

namespace OpenModular.Module.Abstractions.Localization;

public abstract class ModuleLocalizerAbstract : IModuleLocalizer
{
    private readonly IStringLocalizer _localizer;

    protected ModuleLocalizerAbstract(IStringLocalizerFactory localizerFactory)
    {
        _localizer = localizerFactory.Create("Default", GetType().Assembly.GetName().Name!);
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _localizer.GetAllStrings(includeParentCultures);
    }

    public LocalizedString this[string name] => _localizer[name];

    public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];
}