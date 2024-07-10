using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using OpenModular.SourceGenerator.Extensions;

namespace OpenModular.SourceGenerator.Executors.ModuleCore;

internal class DtoExecutor : IExecutor
{
    public void Execute(string moduleCode, GeneratorExecutionContext context)
    {
        var syntaxTrees = context.Compilation.SyntaxTrees.ToList();

        foreach (var syntaxTree in syntaxTrees)
        {
            var root = syntaxTree.GetRoot();

            // 查询所有继承了 IDto 接口并且是部分类的类
            var dtoClassDeclarations = root.DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Where(x => x.BaseList != null && x.BaseList.Types
                                .Any(baseType => baseType.Type.ToString() == "IDto") &&
                            x.Modifiers.Any(SyntaxKind.PartialKeyword))
                .ToList();

            foreach (var declaration in dtoClassDeclarations)
            {
                var dtoClassName = declaration.Identifier.Text;
                var entityClassName = dtoClassName.Replace("Dto", "");

                // 获取实体类声明
                var entityClassDeclaration = context.GetClassDeclaration(entityClassName);
                if (entityClassDeclaration == null)
                {
                    continue;
                }

                var entitySyntaxTree = entityClassDeclaration.SyntaxTree;
                var dtoClassCode = GenerateDtoClassCode(dtoClassName, declaration, entityClassDeclaration, entitySyntaxTree, context);

                context.AddSource($"{dtoClassName}.g.cs", dtoClassCode);
            }
        }
    }

    private string GenerateDtoClassCode(string dtoClassName, ClassDeclarationSyntax dtoClass, ClassDeclarationSyntax entityClass, SyntaxTree entitySyntaxTree, GeneratorExecutionContext context)
    {
        var properties = new List<string>();
        var usings = new HashSet<string>();

        // 获取类的所有属性，包括继承的父类中的属性
        var semanticModel = context.Compilation.GetSemanticModel(entityClass.SyntaxTree);
        var entitySymbol = semanticModel.GetDeclaredSymbol(entityClass) as INamedTypeSymbol;

        if (entitySymbol != null)
        {
            var allProperties = GetAllProperties(entitySymbol);
            foreach (var propertySymbol in allProperties)
            {
                // 检查属性是否包含 DtoPropertyIgnoreAttribute 特性
                var hasIgnoreAttribute = propertySymbol.GetAttributes().Any(attr => attr.AttributeClass?.Name == "DtoPropertyIgnoreAttribute");
                if (hasIgnoreAttribute || propertySymbol.Name == "DomainEvents")
                {
                    continue;
                }

                var propertyType = propertySymbol.Type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
                var propertyName = propertySymbol.Name;

                properties.Add($"public {propertyType} {propertyName} {{ get; set; }}");

                // 添加命名空间到 using 指令
                var namespaceName = propertySymbol.Type.ContainingNamespace.ToDisplayString();
                if (!string.IsNullOrEmpty(namespaceName))
                {
                    usings.Add($"using {namespaceName};");
                }
            }
        }

        var sb = new StringBuilder();

        // 获取实体类文件的所有 using 指令
        var root = entitySyntaxTree.GetRoot();
        var existingUsings = root.DescendantNodes().OfType<UsingDirectiveSyntax>();
        foreach (var usingDirective in existingUsings)
        {
            var usingStr = usingDirective.ToString();
            if (usingStr.Contains("Annotations.SourceGenerator"))
            {
                continue;
            }

            sb.AppendLine(usingDirective.ToString());
        }

        // 添加收集到的 using 指令
        foreach (var usingDirective in usings)
        {
            sb.AppendLine(usingDirective);
        }

        sb.AppendLine($"using {entityClass.GetNamespaceName()};");

        sb.AppendLine();
        sb.AppendLine($"namespace {dtoClass.GetNamespaceName()}");
        sb.AppendLine("{");
        sb.AppendLine($"    public partial class {dtoClassName}");
        sb.AppendLine("    {");

        foreach (var property in properties)
        {
            sb.AppendLine($"        {property}");
        }

        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }

    private IEnumerable<IPropertySymbol> GetAllProperties(INamedTypeSymbol typeSymbol)
    {
        var properties = new List<IPropertySymbol>();

        while (typeSymbol != null)
        {
            properties.AddRange(typeSymbol.GetMembers().OfType<IPropertySymbol>());
            typeSymbol = typeSymbol.BaseType;
        }

        return properties;
    }
}