using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Infrastructure;

public class PasswordHasher : IPasswordHasher, ISingletonDependency
{
    public string HashPassword(User user, string password)
    {
        throw new System.NotImplementedException();
    }

    public bool VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
    {
        throw new System.NotImplementedException();
    }
}