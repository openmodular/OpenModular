namespace OpenModular.DDD.Core.Domain.Annotations;

/// <summary>
/// Ignore property or field for value objects
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class ValueObjectIgnoreMemberAttribute : Attribute;