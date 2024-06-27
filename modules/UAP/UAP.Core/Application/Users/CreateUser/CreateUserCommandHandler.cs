using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Users.CreateUser;

internal class CreateUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher) : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<UserId> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await repository.FindAsync(m => m.Username == request.Username || m.Email == request.Email || m.Phone == request.Phone, cancellationToken);
        if (exists != null)
        {
            if (exists.Username == request.Username)
                throw new UAPBusinessException(UAPErrorCode.User_UsernameExists);
            if (exists.Email == request.Email)
                throw new UAPBusinessException(UAPErrorCode.User_UsernameExists);
            if (exists.Phone == request.Phone)
                throw new UAPBusinessException(UAPErrorCode.User_PhoneExists);
        }

        var user = User.Create(request.Username, request.Email, request.Phone, null);

        user.SetPasswordHash(passwordHasher.HashPassword(user, request.Password));

        await repository.InsertAsync(user, cancellationToken: cancellationToken);

        return user.Id!;
    }
}