using Microsoft.CodeAnalysis;
using System.Text;

namespace OpenModular.SourceGenerator.Executors.ModuleCore;

internal class ModuleExecutor : IExecutor
{
    public void Execute(string moduleCode, GeneratorExecutionContext context)
    {
        var sb = new StringBuilder();
        sb.AppendLine("using OpenModular.Module.Abstractions;");

        sb.AppendLine();

        sb.AppendLine($"namespace {context.Compilation.AssemblyName};");

        sb.AppendLine();

        sb.AppendLine($"public partial class {moduleCode}Module() : ModuleAbstract({moduleCode}Constants.ModuleId, {moduleCode}Constants.ModuleCode);");

        context.AddSource($"{moduleCode}Module.g.cs", sb.ToString());
    }
}