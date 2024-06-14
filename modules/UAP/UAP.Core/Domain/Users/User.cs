using OpenModular.DDD.Core.Domain.Entities;
using OpenModular.Module.UAP.Core.Domain.Users.Events;
using OpenModular.Module.UAP.Core.Domain.Users.Rules;

namespace OpenModular.Module.UAP.Core.Domain.Users;

/// <summary>
/// 用户
/// </summary>
public class User : AggregateRoot<UserId>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; }

    /// <summary>
    /// 用户状态
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// 已锁定
    /// </summary>
    public bool Locked { get; set; }

    /// <summary>
    /// 激活时间
    /// </summary>
    public DateTime? ActivatedTime { get; private set; }

    /// <summary>
    /// 创建人标识
    /// </summary>
    public UserId CreatedBy { get; private set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    public User()
    {
        
    }

    private User(string username, string password, string email, string phone, UserId createdBy) : base(new UserId())
    {
        Check.NotNullOrWhiteSpace(username, nameof(username));
        Check.NotNullOrWhiteSpace(password, nameof(password));
        Check.NotNullOrWhiteSpace(phone, nameof(phone));

        CheckRule(new UserEmailFormatNotValidRule(email));

        Username = username;
        Password = password;
        Email = email;
        Phone = phone;

        Status = UserStatus.Inactive;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new UserCreatedDomainEven(Id!, createdBy, CreatedAt));
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="createdBy"></param>
    /// <returns></returns>
    public static User Create(string username, string password, string email, string phone, UserId createdBy)
    {
        return new User(username, password, email, phone, createdBy);
    }

    /// <summary>
    /// 激活
    /// </summary>
    public void Activate()
    {
        CheckRule(new UserCannotActivateWhenAlreadyActivatedRule(Status));

        Status = UserStatus.Enabled;
        ActivatedTime = DateTime.UtcNow;

        AddDomainEvent(new UserActivatedDomainEvent(Id!, ActivatedTime.Value));
    }

    /// <summary>
    /// 更改邮箱
    /// </summary>
    /// <param name="email"></param>
    public void ChangeEmail(string email)
    {
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }
}