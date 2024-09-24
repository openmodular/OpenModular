using Microsoft.Extensions.Logging;
using OpenModular.Authentication.Abstractions;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Auth.Authenticate.VerifyStageHandlers;

internal class UserStatusVerifyStageHandler : IAuthenticationVerifyStageHandler<User>
{
    public const string StageName = "UserStatus";

    public string Name => StageName;

    private readonly ILogger<UserStatusVerifyStageHandler> _logger;

    public UserStatusVerifyStageHandler(ILogger<UserStatusVerifyStageHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(AuthenticationContext<User> context)
    {
        _logger.LogInformation("Start verify user status, the user is {@user}.", context.User);

        if (context.User.Status == UserStatus.Deleted)
        {
            context.Status = AuthenticationStatus.UserNotFound;
            _logger.LogInformation("Verify user status failed, the login user's status is disabled, can not login.");
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}