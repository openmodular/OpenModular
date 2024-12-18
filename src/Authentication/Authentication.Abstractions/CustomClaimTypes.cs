﻿namespace OpenModular.Authentication.Abstractions;

public class CustomClaimTypes
{
    /// <summary>
    /// 租户编号
    /// </summary>
    public const string TENANT_ID = "td";

    /// <summary>
    /// 账户标识
    /// </summary>
    public const string ACCOUNT_ID = "id";

    /// <summary>
    /// 平台类型
    /// </summary>
    public const string PLATFORM = "pf";

    /// <summary>
    /// 登录时间
    /// </summary>
    public const string LOGIN_TIME = "lt";

    /// <summary>
    /// 登录IP
    /// </summary>
    public const string LOGIN_IP = "ip";
}