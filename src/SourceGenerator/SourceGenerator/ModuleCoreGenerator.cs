using System.Reflection;
using Microsoft.CodeAnalysis;
using OpenModular.SourceGenerator.Executors;
using OpenModular.SourceGenerator.Extensions;

namespace OpenModular.SourceGenerator;

[Generator]
internal class ModuleCoreGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var assemblyName = context.Compilation.AssemblyName;
        if (assemblyName == null)
            return;

        var moduleCode = context.GetModuleCode();
        if (!assemblyName.EndsWith($"{moduleCode}.Core"))
        {
            return;
        }

        var executorTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(m => typeof(IExecutor).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract && m.FullName!.Contains("ModuleCore")).ToList();

        foreach (var executorType in executorTypes)
        {
            (Activator.CreateInstance(executorType) as IExecutor)!.Execute(moduleCode, context);
        }
    }
}