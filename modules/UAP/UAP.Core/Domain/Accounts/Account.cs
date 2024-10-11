using System.Text.Json.Serialization;
using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Accounts.Events;
using OpenModular.Module.UAP.Core.Domain.Accounts.Rules;

namespace OpenModular.Module.UAP.Core.Domain.Accounts;

/// <summary>
/// 账户
/// </summary>
public class Account : AggregateRoot<AccountId>, ITenant
{
    /// <summary>
    /// 租户标识
    /// </summary>
    public TenantId TenantId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [JsonInclude]
    public string UserName { get; }

    /// <summary>
    /// 标准化用户名
    /// </summary>
    public string NormalizedUserName { get; }

    /// <summary>
    /// 密码哈希值
    /// </summary>
    [JsonInclude]
    public string PasswordHash { get; private set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// 标准化邮箱
    /// </summary>
    public string NormalizedEmail { get; private set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; private set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string Tel { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 性别，0、未知；1、男；2、女
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 账户状态
    /// </summary>
    [JsonInclude]
    public AccountStatus Status { get; private set; }

    /// <summary>
    /// 已锁定
    /// </summary>
    [JsonInclude]
    public bool Locked { get; private set; }

    /// <summary>
    /// 是否实名认证
    /// </summary>
    public bool IsRealNameVerified { get; set; }

    /// <summary>
    /// 激活时间
    /// </summary>
    [JsonInclude]
    public DateTimeOffset? ActivatedTime { get; private set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public AccountId CreatedBy { get; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonInclude]
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [JsonInclude]
    public DateTimeOffset? UpdatedAt { get; private set; }

    public Account()
    {
        //for ef
    }

    /// <summary>
    /// This constructor only for Json Serialize and Deserialize
    /// </summary>
    [JsonConstructor]
    public Account(AccountId id, string userName, string email, string phone, AccountId createdBy) : base(id)
    {
        UserName = userName;
        NormalizedUserName = userName.ToUpper();
        Email = email;
        NormalizedEmail = email.ToUpper();
        Phone = phone;
        CreatedBy = createdBy;
        Status = AccountStatus.Inactive;
    }

    private Account(string userName, string email, string phone, AccountStatus status, AccountId createdBy) : base(new AccountId())
    {
        Check.NotNull(userName, nameof(userName));

        CheckRule(new AccountEmailFormatNotValidRule(email));

        UserName = userName;
        Email = email;
        Phone = phone;

        Status = status;
        CreatedBy = createdBy;
        CreatedAt = DateTimeOffset.UtcNow;

        AddDomainEvent(new AccountCreatedDomainEvent(this));
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="status"></param>
    /// <param name="createdBy"></param>
    /// <returns></returns>
    public static Account Create(string username, string email, string phone, AccountStatus status, AccountId createdBy)
    {
        return new Account(username, email, phone, status, createdBy);
    }

    /// <summary>
    /// 激活
    /// </summary>
    public void Activate()
    {
        CheckRule(new AccountCannotActivateWhenAlreadyActivatedRule(Status));

        Status = AccountStatus.Enabled;
        ActivatedTime = DateTime.UtcNow;

        AddDomainEvent(new AccountActivatedDomainEvent(this));
    }

    /// <summary>
    /// 更改邮箱
    /// </summary>
    /// <param name="email"></param>
    public void SetEmail(string email)
    {
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// 设置密码
    /// </summary>
    /// <param name="passwordHash"></param>
    public void SetPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// 删除
    /// </summary>
    public void Delete()
    {
        Status = AccountStatus.Deleted;
        UpdatedAt = DateTimeOffset.Now;

        AddDomainEvent(new AccountDeletedDomainEvent(this));
    }
}