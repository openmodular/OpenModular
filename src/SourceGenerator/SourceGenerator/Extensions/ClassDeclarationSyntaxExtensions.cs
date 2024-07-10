using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OpenModular.SourceGenerator.Extensions;

public static class ClassDeclarationSyntaxExtensions
{
    /// <summary>
    /// 获取类的命名空间名称
    /// </summary>
    /// <param name="classDeclaration"></param>
    /// <returns></returns>
    public static string GetNamespaceName(this ClassDeclarationSyntax classDeclaration)
    {
        var namespaceDeclaration = classDeclaration.Ancestors().OfType<NamespaceDeclarationSyntax>().FirstOrDefault();
        if (namespaceDeclaration != null)
        {
            return namespaceDeclaration!.Name.ToString();
        }
        var fileScopedNamespaceDeclaration = classDeclaration.Ancestors().OfType<FileScopedNamespaceDeclarationSyntax>().FirstOrDefault();
        return fileScopedNamespaceDeclaration!.Name.ToString();
    }
}