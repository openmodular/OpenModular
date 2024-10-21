using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Get;

internal class AccountGetQueryHandler : QueryHandler<AccountGetQuery, AccountDto>
{
    private readonly IAccountRepository _repository;

    public AccountGetQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public override async Task<AccountDto> ExecuteAsync(AccountGetQuery request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAsync(request.Id, cancellationToken);

        return ObjectMapper.Map<AccountDto>(account);
    }
}