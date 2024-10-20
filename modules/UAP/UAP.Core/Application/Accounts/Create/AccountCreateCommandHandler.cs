using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Create;

internal class AccountCreateCommandHandler(IAccountRepository repository, IPasswordHasher passwordHasher) : CommandHandler<AccountCreateCommand, AccountId>
{
    public override async Task<AccountId> ExecuteAsync(AccountCreateCommand command, CancellationToken cancellationToken)
    {
        Check.NotNull(command.OperatorId, nameof(command.OperatorId));

        var exists = await repository.FindAsync(m => m.UserName == command.Username || m.Email == command.Email || m.Phone == command.Phone, cancellationToken);
        if (exists != null)
        {
            if (exists.UserName == command.Username)
                throw new UAPBusinessException(UAPErrorCode.Account_UsernameExists);
            if (exists.Email == command.Email)
                throw new UAPBusinessException(UAPErrorCode.Account_UsernameExists);
            if (exists.Phone == command.Phone)
                throw new UAPBusinessException(UAPErrorCode.Account_PhoneExists);
        }

        var user = Account.Create(new AccountId(), command.Username, command.Email, command.Phone, AccountStatus.Inactive, command.OperatorId!);

        user.PasswordHash = passwordHasher.HashPassword(user, command.Password);

        await repository.InsertAsync(user, cancellationToken: cancellationToken);

        return user.Id!;
    }
}