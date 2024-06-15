using OpenModular.Module.Abstractions.Localization;
using Microsoft.Extensions.Localization;

namespace OpenModular.Module.UAP.Core;

internal class UAPModuleLocalizer(IStringLocalizerFactory localizerFactory) : ModuleLocalizerAbstract(localizerFactory);