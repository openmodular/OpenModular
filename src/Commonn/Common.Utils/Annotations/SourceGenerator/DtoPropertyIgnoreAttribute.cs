namespace OpenModular.Common.Utils.Annotations.SourceGenerator;

/// <summary>
/// Ignore property when generating DTO
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DtoPropertyIgnoreAttribute : Attribute;