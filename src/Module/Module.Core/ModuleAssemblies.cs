using OpenModular.Module.Abstractions;
using System.Reflection;

namespace OpenModular.Module.Core;

internal class ModuleAssemblies: IModuleAssemblies
{
    public Assembly? Core { get; set; }
    public Assembly? Web { get; set; }
    public Assembly? Api { get; set; }
}