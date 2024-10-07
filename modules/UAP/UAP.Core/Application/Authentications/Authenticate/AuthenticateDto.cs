﻿using OpenModular.Authentication.Abstractions;
using OpenModular.Module.UAP.Core.Application.Users.Get;

namespace OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;

public class AuthenticateDto
{
    /// <summary>
    /// 认证模式
    /// </summary>
    public AuthenticationMode Mode { get; set; }

    /// <summary>
    /// 认证源
    /// </summary>
    public AuthenticationSource Source { get; set; }

    /// <summary>
    /// 认证成功
    /// </summary>
    public bool Success => Status == AuthenticationStatus.Success;

    /// <summary>
    /// 用户信息
    /// </summary>
    public UserDto User { get; set; }

    /// <summary>
    /// 客户端
    /// </summary>
    public AuthenticationClient Client { get; set; }

    /// <summary>
    /// 认证时间
    /// </summary>
    public DateTimeOffset AuthenticateTime { get; set; }

    /// <summary>
    /// 认证状态
    /// </summary>
    public AuthenticationStatus Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string Message { get; set; }
}