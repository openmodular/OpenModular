﻿using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Accounts.Events;
using OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

namespace OpenModular.Module.UAP.Core.Domain.Accounts;

/// <summary>
/// 账户
/// </summary>
public class Account : AggregateRoot<AccountId>
{
    private string? _userName;
    private string? _email;
    private string? _phone;

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName
    {
        get => _userName;
        set
        {
            Check.NullOrWhiteSpace(value, nameof(UserName));
            _userName = value;
        }
    }

    /// <summary>
    /// 标准化用户名
    /// </summary>
    public string NormalizedUserName { get; set; }

    /// <summary>
    /// 密码哈希值
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email
    {
        get => _email;
        set
        {
            CheckRule(new AccountEmailFormatNotValidRule(value));
            _email = value;
        }
    }

    /// <summary>
    /// 标准化邮箱
    /// </summary>
    public string? NormalizedEmail { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone
    {
        get => _phone;
        set
        {
            CheckRule(new AccountPhoneFormatNotValidRule(value));
            _phone = value;
        }
    }

    /// <summary>
    /// 账户状态
    /// </summary>
    public AccountStatus Status { get; set; }

    public Account()
    {
        //for ef
    }

    private Account(AccountId accountId, string userName, string? email, string? phone, AccountStatus status) : base(accountId)
    {
        UserName = userName;
        NormalizedUserName = userName.ToUpper();
        Email = email;
        NormalizedEmail = email ?? email.ToUpper();
        Phone = phone;
        Status = status;

        PublishDomainEvent(new AccountCreatedDomainEvent(this));
    }

    /// <summary>
    /// 创建账户
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public static Account Create(AccountId accountId, string username, string? email, string? phone, AccountStatus status)
    {
        return new Account(accountId, username, email, phone, status);
    }
}