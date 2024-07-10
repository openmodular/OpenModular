using Microsoft.CodeAnalysis;
using System.Text;

namespace OpenModular.SourceGenerator.Executors.ModuleCore;

internal class ModuleBusinessRuleExecutor : IExecutor
{
    public void Execute(string moduleCode, GeneratorExecutionContext context)
    {
        var sb = new StringBuilder();

        sb.AppendLine("using OpenModular.DDD.Core.Domain;");
        sb.AppendLine();
        sb.AppendLine($"namespace {context.Compilation.AssemblyName};");
        sb.AppendLine();
        sb.AppendLine($"internal abstract record {moduleCode}BusinessRule({moduleCode}ErrorCode errorCode) : IBusinessRule");
        sb.AppendLine("{");
        sb.AppendLine($"    public string ModuleCode => {moduleCode}Constants.ModuleCode;");
        sb.AppendLine("    public Enum ErrorCode { get; } = errorCode;");
        sb.AppendLine("    public abstract bool IsBroken();");
        sb.AppendLine("}");

        context.AddSource($"{moduleCode}BusinessRule.g.cs", sb.ToString());
    }
}