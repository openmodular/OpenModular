using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

internal class GetAccountQueryHandler : QueryHandler<GetAccountQuery, AccountDto?>
{
    private readonly IAccountRepository _repository;

    public GetAccountQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public override async Task<AccountDto?> ExecuteAsync(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await _repository.FindAsync(m => (request.Id != null && m.Id == request.Id)
                                    || (request.UserName.NotNullOrWhiteSpace() && m.NormalizedUserName == request.UserName.ToUpper())
                                    || (request.Email.NotNullOrWhiteSpace() && m.NormalizedEmail == request.Email.ToUpper())
                                        || (request.Phone.NotNullOrWhiteSpace() && m.Phone == request.Phone.ToUpper()), cancellationToken);

        if (account == null || account.Status == AccountStatus.Deleted)
        {
            return null;
        }

        return ObjectMapper.Map<AccountDto>(account);
    }
}