namespace OpenModular.Authorization.Annotations;

/// <summary>
/// ֻҪ�û��ѵ�¼���ܷ���
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public sealed class AuthenticatedOnlyAttribute : Attribute;