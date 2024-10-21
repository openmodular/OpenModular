using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Domain.Accounts.Events;

namespace OpenModular.Module.UAP.Core.Application.Accounts.Delete;

internal class AccountDeleteCommandHandler : CommandHandler<AccountDeleteCommand>
{
    private readonly IAccountRepository _repository;

    public AccountDeleteCommandHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public override async Task ExecuteAsync(AccountDeleteCommand request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetAsync(request.Id, cancellationToken);

        await _repository.DeleteAsync(account, cancellationToken);

        await Mediator.Publish(new AccountCreatedDomainEvent(account), cancellationToken);
    }
}