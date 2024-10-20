﻿using OpenModular.DDD.Core.Application.Command;
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

        var exists = await repository.FindAsync(m => m.UserName == command.Username, cancellationToken);
        if (exists != null)
        {
            throw new UAPBusinessException(UAPErrorCode.Account_UsernameExists);
        }

        if (command.Phone.IsNotNullOrWhiteSpace())
        {
            exists = await repository.FindAsync(m => m.Phone == command.Phone, cancellationToken);
            if (exists != null)
            {
                throw new UAPBusinessException(UAPErrorCode.Account_PhoneExists);
            }
        }

        if (command.Email.IsNotNullOrWhiteSpace())
        {
            exists = await repository.FindAsync(m => m.Email == command.Email, cancellationToken);
            if (exists != null)
            {
                throw new UAPBusinessException(UAPErrorCode.Account_EmailExists);
            }
        }

        var user = Account.Create(new AccountId(), command.Type, command.Username, command.Email, command.Phone, AccountStatus.Inactive, command.OperatorId!);

        if (command.Password.IsNotNullOrWhiteSpace())
            user.PasswordHash = passwordHasher.HashPassword(user, command.Password);

        await repository.InsertAsync(user, cancellationToken: cancellationToken);

        return user.Id!;
    }
}