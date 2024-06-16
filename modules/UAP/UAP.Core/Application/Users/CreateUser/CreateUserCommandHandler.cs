using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;

namespace OpenModular.Module.UAP.Core.Application.Users.CreateUser;

internal class CreateUserCommandHandler(IUserRepository repository, UAPDbContext dbContext) : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<UserId> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await repository.FindAsync(m => m.Username == request.Username || m.Email == request.Email || m.Phone == request.Phone, false, cancellationToken);
        if (exists != null)
        {
            if (exists.Username == request.Username)
                throw new UAPBusinessException(UAPErrorCode.User_UsernameExists);
            if (exists.Email == request.Email)
                throw new UAPBusinessException(UAPErrorCode.User_UsernameExists);
            if (exists.Phone == request.Phone)
                throw new UAPBusinessException(UAPErrorCode.User_PhoneExists);
        }

        //处理密码

        var user = User.Create(request.Username, request.Password, request.Email, request.Phone, null);

        await repository.InsertAsync(user, cancellationToken: cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return user.Id!;
    }
}