using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.UAP.Core.Application.Authentications.Authenticate.VerifyStageHandlers;
using OpenModular.Module.UAP.Core.Domain.Accounts;

namespace OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;

internal class AuthenticationVerifyHandler : IAuthenticationVerifyHandler<Account>, ITransientDependency
{
    public static List<string> Stages = [AccountStatusVerifyStageHandler.StageName];

    private readonly IEnumerable<IAuthenticationVerifyStageHandler<Account>> _handlers;

    public AuthenticationVerifyHandler(IEnumerable<IAuthenticationVerifyStageHandler<Account>> handlers)
    {
        _handlers = handlers;
    }

    public async Task HandleAsync(AuthenticationContext<Account> context, CancellationToken cancellationToken)
    {
        foreach (var stage in Stages)
        {
            var handle = _handlers.FirstOrDefault(m => m.Name == stage);
            if (handle == null)
            {
                context.Status = AuthenticationStatus.VerifyStageNotFound;
                return;
            }

            await handle.HandleAsync(context, cancellationToken);

            if (context.Status != AuthenticationStatus.Success)
            {
                return;
            }
        }
    }
}