using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using OpenModular.SourceGenerator.Extensions;

namespace OpenModular.SourceGenerator;

[Generator]
public class TypeIdSourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // 注册一个语法接收器，用于在语法树中查找目标类
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // 获取语法接收器
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
            return;

        // 获取所有的目标类
        var targetClasses = receiver.TargetClasses;

        foreach (var classDeclaration in targetClasses)
        {
            var namespaceName = classDeclaration.GetNamespaceName();
            var className = classDeclaration.Identifier.Text;

            // 生成代码
            var source = $@"
namespace {namespaceName}
{{
    public sealed partial class {className}
    {{
        public {className}()
        {{
        }}

        public {className}(string id) : base(id)
        {{
        }}

        public {className}(Guid id) : base(id)
        {{
        }}
    }}
}}
";
            // 添加生成的代码到编译器
            context.AddSource($"{className}.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }

    private class SyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> TargetClasses { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // 查找目标类
            if (syntaxNode is ClassDeclarationSyntax classDeclaration &&
                classDeclaration.Modifiers.Any(SyntaxKind.PublicKeyword) &&
                classDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword) &&
                classDeclaration.Modifiers.Any(SyntaxKind.SealedKeyword) &&
                classDeclaration.BaseList?.Types.Any(t => t.ToString() == "TypedIdValueBase") == true)
            {
                TargetClasses.Add(classDeclaration);
            }
        }
    }
}