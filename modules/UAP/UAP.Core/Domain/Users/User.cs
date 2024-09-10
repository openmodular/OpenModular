using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.Module.UAP.Core.Domain.Users.Events;
using OpenModular.Module.UAP.Core.Domain.Users.Rules;
using System.Text.Json.Serialization;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Domain.Users;

/// <summary>
/// 用户
/// </summary>
public class User : AggregateRoot<UserId>
{
    /// <summary>
    /// 用户名
    /// </summary>
    [JsonInclude]
    public string UserName { get; }

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
    /// 用户状态
    /// </summary>
    [JsonInclude]
    public UserStatus Status { get; private set; }

    /// <summary>
    /// 已锁定
    /// </summary>
    [JsonInclude]
    public bool Locked { get; private set; }

    /// <summary>
    /// 激活时间
    /// </summary>
    [JsonInclude]
    public DateTimeOffset? ActivatedTime { get; private set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public UserId CreatedBy { get; }

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

    public User()
    {
        //for ef
    }

    /// <summary>
    /// This constructor only for Json Serialize and Deserialize
    /// </summary>
    [JsonConstructor]
    public User(UserId id, string username, string email, string phone, UserId createdBy) : base(id)
    {
        UserName = username;
        Email = email;
        Phone = phone;
        CreatedBy = createdBy;
    }

    private User(string username, string email, string phone, UserStatus status, UserId createdBy) : base(new UserId())
    {
        Check.NotNull(username, nameof(username));
        Check.NotNull(phone, nameof(phone));

        CheckRule(new UserEmailFormatNotValidRule(email));

        UserName = username;
        Email = email;
        Phone = phone;

        Status = status;
        CreatedBy = createdBy;
        CreatedAt = DateTimeOffset.UtcNow;

        AddDomainEvent(new UserCreatedDomainEvent(this));
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="username"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="createdBy"></param>
    /// <returns></returns>
    public static User Create(string username, string email, string phone, UserId createdBy)
    {
        return new User(username, email, phone, UserStatus.Inactive, createdBy);
    }

    public static User Register(string username, string email, string phone, UserId createdBy)
    {
        return new User(username, email, phone, UserStatus.Unverified, createdBy);
    }

    /// <summary>
    /// 激活
    /// </summary>
    public void Activate()
    {
        CheckRule(new UserCannotActivateWhenAlreadyActivatedRule(Status));

        Status = UserStatus.Enabled;
        ActivatedTime = DateTime.UtcNow;

        AddDomainEvent(new UserActivatedDomainEvent(this));
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
        Status = UserStatus.Deleted;
        UpdatedAt = DateTimeOffset.Now;

        AddDomainEvent(new UserDeletedDomainEvent(this));
    }
}