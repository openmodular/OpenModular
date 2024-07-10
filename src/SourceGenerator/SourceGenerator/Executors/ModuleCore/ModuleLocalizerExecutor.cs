using Microsoft.CodeAnalysis;
using System.Text;

namespace OpenModular.SourceGenerator.Executors.ModuleCore;

internal class ModuleLocalizerExecutor : IExecutor
{
    public void Execute(string moduleCode, GeneratorExecutionContext context)
    {
        var sb = new StringBuilder();
        sb.AppendLine("using OpenModular.Module.Abstractions.Localization;");
        sb.AppendLine("using Microsoft.Extensions.Localization;");

        sb.AppendLine();

        sb.AppendLine($"namespace {context.Compilation.AssemblyName};");

        sb.AppendLine();

        sb.AppendLine($"public partial class {moduleCode}ModuleLocalizer(IStringLocalizerFactory localizerFactory) : ModuleLocalizerAbstract(localizerFactory);");

        context.AddSource($"{moduleCode}ModuleLocalizer.g.cs", sb.ToString());
    }
}