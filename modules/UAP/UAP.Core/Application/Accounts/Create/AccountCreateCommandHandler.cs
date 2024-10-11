using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Create;

internal class AccountCreateCommandHandler(IAccountRepository repository, IPasswordHasher passwordHasher) : ICommandHandler<AccountCreateCommand, AccountId>
{
    public async Task<AccountId> Handle(AccountCreateCommand commond, CancellationToken cancellationToken)
    {
        var exists = await repository.FindAsync(m => m.UserName == commond.Username || m.Email == commond.Email || m.Phone == commond.Phone, cancellationToken);
        if (exists != null)
        {
            if (exists.UserName == commond.Username)
                throw new UAPBusinessException(UAPErrorCode.Account_UsernameExists);
            if (exists.Email == commond.Email)
                throw new UAPBusinessException(UAPErrorCode.Account_UsernameExists);
            if (exists.Phone == commond.Phone)
                throw new UAPBusinessException(UAPErrorCode.Account_PhoneExists);
        }

        var user = Account.Create(commond.Username, commond.Email, commond.Phone, AccountStatus.Inactive, commond.CreatedBy);

        user.SetPasswordHash(passwordHasher.HashPassword(user, commond.Password));

        await repository.InsertAsync(user, cancellationToken: cancellationToken);

        return user.Id!;
    }
}