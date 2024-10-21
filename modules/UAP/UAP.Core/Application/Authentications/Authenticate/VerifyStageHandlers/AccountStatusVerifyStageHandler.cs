using Microsoft.Extensions.Logging;
using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Application.Authentications.Authenticate.VerifyStageHandlers;

internal class AccountStatusVerifyStageHandler : IAuthenticationVerifyStageHandler<Account>, ITransientDependency
{
    public const string StageName = "AccountStatus";

    public string Name => StageName;

    private readonly ILogger<AccountStatusVerifyStageHandler> _logger;

    public AccountStatusVerifyStageHandler(ILogger<AccountStatusVerifyStageHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(AuthenticationContext<Account> context, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start verify user status, the user is {@account}.", context.Account);

        if (context.Account!.Status == AccountStatus.Inactive)
        {
            context.Status = AuthenticationStatus.AccountInactive;
            context.Message = "Authentication failed, account is inactive";
            _logger.LogInformation("Verify account status failed, the login account's status is disabled, can not login.");
            return Task.CompletedTask;
        }

        if (context.Account!.Status == AccountStatus.Disabled)
        {
            context.Status = AuthenticationStatus.AccountDisabled;
            context.Message = "Authentication failed, account is inactive";
            _logger.LogInformation("Verify account status failed, the login account's status is disabled, can not login.");
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}