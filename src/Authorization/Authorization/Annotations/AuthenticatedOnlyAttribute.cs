namespace OpenModular.Authorization.Annotations;

/// <summary>
/// 只要用户已登录就能访问
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public sealed class AuthenticatedOnlyAttribute : Attribute;