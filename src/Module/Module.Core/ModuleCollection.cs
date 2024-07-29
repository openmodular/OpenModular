using OpenModular.Module.Abstractions;

namespace OpenModular.Module.Core;

internal class ModuleCollection : List<IModuleDescriptor>, IModuleCollection;