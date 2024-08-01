using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Users.CreateUser;

internal class CreateUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher) : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<UserId> Handle(CreateUserCommand commond, CancellationToken cancellationToken)
    {
        var exists = await repository.FindAsync(m => m.UserName == commond.Username || m.Email == commond.Email || m.Phone == commond.Phone, cancellationToken);
        if (exists != null)
        {
            if (exists.UserName == commond.Username)
                throw new UAPBusinessException(UAPErrorCode.User_UsernameExists);
            if (exists.Email == commond.Email)
                throw new UAPBusinessException(UAPErrorCode.User_UsernameExists);
            if (exists.Phone == commond.Phone)
                throw new UAPBusinessException(UAPErrorCode.User_PhoneExists);
        }

        var user = User.Create(commond.Username, commond.Email, commond.Phone, commond.CreatedBy);

        user.SetPasswordHash(passwordHasher.HashPassword(user, commond.Password));

        await repository.InsertAsync(user, cancellationToken: cancellationToken);

        return user.Id!;
    }
}