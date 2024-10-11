using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Infrastructure;

public interface IPasswordHasher
{
    /// <summary>
    /// 计算密码哈希值
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    string HashPassword(Account user, string password);

    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="user"></param>
    /// <param name="hashedPassword"></param>
    /// <param name="providedPassword"></param>
    /// <returns></returns>
    bool VerifyHashedPassword(Account user, string hashedPassword, string providedPassword);
}