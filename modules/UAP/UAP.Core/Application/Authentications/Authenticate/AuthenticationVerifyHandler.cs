using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Module.UAP.Core.Application.Authentications.Authenticate.VerifyStageHandlers;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;

internal class AuthenticationVerifyHandler : IAuthenticationVerifyHandler<User>, ITransientDependency
{
    public static List<string> Stages = [UserStatusVerifyStageHandler.StageName];

    private readonly IEnumerable<IAuthenticationVerifyStageHandler<User>> _handlers;

    public AuthenticationVerifyHandler(IEnumerable<IAuthenticationVerifyStageHandler<User>> handlers)
    {
        _handlers = handlers;
    }

    public async Task HandleAsync(AuthenticationContext<User> context, CancellationToken cancellationToken)
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