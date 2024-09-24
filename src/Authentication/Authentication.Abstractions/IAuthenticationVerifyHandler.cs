﻿namespace OpenModular.Authentication.Abstractions;

/// <summary>
/// 认证校验处理器
/// </summary>
public interface IAuthenticationVerifyHandler<TUser> where TUser : class
{
    /// <summary>
    /// 处理
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    Task HandleAsync(AuthenticationContext<TUser> context);
}