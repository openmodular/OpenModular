using Microsoft.CodeAnalysis;

namespace OpenModular.SourceGenerator.Executors;

internal interface IExecutor
{
    void Execute(string moduleCode, GeneratorExecutionContext context);
}