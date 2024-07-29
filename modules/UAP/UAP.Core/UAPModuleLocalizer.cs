using Microsoft.Extensions.Localization;
using OpenModular.Module.Abstractions.Localization;

namespace OpenModular.Module.UAP.Core;

public class UAPModuleLocalizer(IStringLocalizerFactory localizerFactory) : ModuleLocalizerAbstract(localizerFactory);