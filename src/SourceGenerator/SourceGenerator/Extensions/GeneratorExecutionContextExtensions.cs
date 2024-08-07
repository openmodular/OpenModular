using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OpenModular.SourceGenerator.Extensions;

internal static class GeneratorExecutionContextExtensions
{
    /// <summary>
    /// Gets the module code from the module constants class.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetModuleCode(this GeneratorExecutionContext context)
    {
        var assemblyName = context.Compilation.AssemblyName;
        if (string.IsNullOrWhiteSpace(assemblyName))
            throw new Exception("Assembly name is null or empty.");

        return assemblyName!.Split('.')[2];
    }

    /// <summary>
    /// 获取实体类列表
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static List<ClassDeclarationSyntax> GetEntityClasses(this GeneratorExecutionContext context)
    {
        var entityClasses = new List<ClassDeclarationSyntax>();
        var iEntityType = context.Compilation.GetTypeByMetadataName("OpenModular.DDD.Core.Domain.Entities.IEntity");

        foreach (var syntaxTree in context.Compilation.SyntaxTrees)
        {
            var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);
            var root = syntaxTree.GetRoot();

            var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            foreach (var classDeclaration in classDeclarations)
            {
                var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;

                if (classSymbol != null && classSymbol.AllInterfaces.Contains(iEntityType) && !classSymbol.IsAbstract)
                {
                    entityClasses.Add(classDeclaration);
                }
            }
        }

        return entityClasses;
    }

    /// <summary>
    /// 查询指定类定义
    /// </summary>
    /// <param name="context"></param>
    /// <param name="className"></param>
    /// <returns></returns>
    public static ClassDeclarationSyntax GetClassDeclaration(this GeneratorExecutionContext context, string className)
    {
        foreach (var syntaxTree in context.Compilation.SyntaxTrees)
        {
            var root = syntaxTree.GetRoot();
            var classDeclaration = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault(x => x.Identifier.Text == className);
            if (classDeclaration != null)
            {
                return classDeclaration;
            }
        }
        return null;
    }
}