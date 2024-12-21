using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Create;

internal class CreateAccountCommandHandler(IAccountRepository repository, IPasswordHasher passwordHasher) : CommandHandler<CreateAccountCommand, AccountId>
{
    public override async Task<AccountId> ExecuteAsync(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        Check.NotNull(command.OperatorId, nameof(command.OperatorId));

        var account = Account.Create(new AccountId(), command.UserName, command.Email, command.Phone, command.Status);

        Account? existsAccount;
        if (account.UserName.NotNullOrWhiteSpace())
        {
            existsAccount = await repository.FindAsync(m => m.UserName == command.UserName, cancellationToken);
            if (existsAccount != null)
            {
                throw new UAPBusinessException(UAPErrorCode.Account_UsernameExists);
            }
        }

        if (command.Phone.NotNullOrWhiteSpace())
        {
            existsAccount = await repository.FindAsync(m => m.Phone == command.Phone, cancellationToken);
            if (existsAccount != null)
            {
                throw new UAPBusinessException(UAPErrorCode.Account_PhoneExists);
            }
        }

        if (command.Email.NotNullOrWhiteSpace())
        {
            existsAccount = await repository.FindAsync(m => m.Email == command.Email, cancellationToken);
            if (existsAccount != null)
            {
                throw new UAPBusinessException(UAPErrorCode.Account_EmailExists);
            }
        }

        if (command.Password.NotNullOrWhiteSpace())
            account.PasswordHash = passwordHasher.HashPassword(account, command.Password);

        await repository.InsertAsync(account, cancellationToken: cancellationToken);

        return account.Id!;
    }
}