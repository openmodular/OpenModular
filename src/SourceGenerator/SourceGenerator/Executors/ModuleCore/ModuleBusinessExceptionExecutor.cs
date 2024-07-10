using Microsoft.CodeAnalysis;
using System.Text;

namespace OpenModular.SourceGenerator.Executors.ModuleCore;

internal class ModuleBusinessExceptionExecutor : IExecutor
{
    public void Execute(string moduleCode, GeneratorExecutionContext context)
    {
        var sb = new StringBuilder();
        sb.AppendLine("using OpenModular.Module.Abstractions.Exceptions;");

        sb.AppendLine();

        sb.AppendLine($"namespace {context.Compilation.AssemblyName};");

        sb.AppendLine();

        sb.AppendLine($"public partial class {moduleCode}BusinessException({moduleCode}ErrorCode errorCode, string message = null) : ModuleBusinessException({moduleCode}Constants.ModuleCode, errorCode, message);");

        context.AddSource($"{moduleCode}BusinessException.g.cs", sb.ToString());
    }
}