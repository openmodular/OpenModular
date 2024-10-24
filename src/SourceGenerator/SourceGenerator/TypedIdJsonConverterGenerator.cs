using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;
using OpenModular.SourceGenerator.Extensions;

[Generator]
public class TypedIdJsonConverterGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new TypedIdSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not TypedIdSyntaxReceiver receiver) return;

        var converters = new List<string>();
        var sb = new StringBuilder();

        // 遍历所有强类型 ID，生成各自的 JsonConverter
        foreach (var type in receiver.Candidates)
        {
            var namespaceName = type.GetNamespaceName();
            var typeName = type.Identifier.Text;

            // 生成对应的 JsonConverter 代码
            var generatedCode = $@"
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using {namespaceName};

namespace {namespaceName}
{{
    public class {typeName}JsonConverter : JsonConverter<{typeName}>
    {{
        public override {typeName} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {{
            var value = reader.GetString();
            return value != null ? new {typeName}(Guid.Parse(value)) : null;
        }}

        public override void Write(Utf8JsonWriter writer, {typeName} value, JsonSerializerOptions options)
        {{
            writer.WriteStringValue(value.ToString());
        }}
    }}
}}
";

            // 将生成的代码添加到编译上下文中
            context.AddSource($"{typeName}JsonConverter.g.cs", generatedCode);

            // 记录生成的转换器名称，便于后续汇总
            converters.Add($"{namespaceName}.{typeName}JsonConverter");
        }

        // 生成汇聚所有 JsonConverter 的扩展方法
        GenerateConvertersRegistration(context, converters);
    }

    private static void GenerateConvertersRegistration(GeneratorExecutionContext context, List<string> converters)
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class JsonConverterExtensions
    {
        public static void AddTypedIdJsonConverters(this IServiceCollection services)
        {
            services.Configure<JsonOptions>(options =>
            {
");

        // 自动注册所有生成的 JsonConverter
        foreach (var converter in converters)
        {
            sb.AppendLine($"                options.JsonSerializerOptions.Converters.Add(new {converter}());");
        }

        sb.AppendLine(@"
            });
        }
    }
}
");

        // 将汇总的扩展方法添加到生成的代码中
        context.AddSource("TypedIdJsonConvertersExtensions.g.cs", sb.ToString());
    }

    class TypedIdSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> Candidates { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax cds &&
                cds.BaseList?.Types.Any(bt => bt.ToString().Contains("TypedIdValueBase")) == true)
            {
                Candidates.Add(cds);
            }
        }
    }
}
