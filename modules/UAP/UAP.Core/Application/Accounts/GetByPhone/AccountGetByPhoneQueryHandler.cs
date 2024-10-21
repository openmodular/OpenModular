using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Application.Accounts.GetByPhone;

internal class AccountGetByPhoneQueryHandler : QueryHandler<AccountGetByPhoneQuery, AccountDto>
{
    private readonly IAccountRepository _repository;

    public AccountGetByPhoneQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public override async Task<AccountDto> ExecuteAsync(AccountGetByPhoneQuery request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAsync(m => m.Phone == request.Phone, cancellationToken);
        return ObjectMapper.Map<AccountDto>(account);
    }
}