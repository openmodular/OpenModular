using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure.Persistence;

namespace OpenModular.Module.UAP.Core.Application.Users.CreateUser;

internal class CreateUserCommandHandler(IUserRepository _repository,UAPDbContext dbContext) : ICommandHandler<CreateUserCommand, UserId>
{
    public async Task<UserId> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repository.FindAsync(m => m.Username == request.Username || m.Email == request.Email || m.Phone == request.Phone, false, cancellationToken);
        if (exists != null)
        {
            if (exists.Username == request.Username)
                throw new InvalidOperationException("The username is exist");
            if (exists.Email == request.Email)
                throw new InvalidOperationException("The user email is exist");
            if (exists.Phone == request.Phone)
                throw new InvalidOperationException("The user phone is exist");
        }

        //处理密码

        var user = User.Create(request.Username, request.Password, request.Email, request.Phone, null);

        await _repository.InsertAsync(user, cancellationToken: cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return user.Id!;
    }
} 