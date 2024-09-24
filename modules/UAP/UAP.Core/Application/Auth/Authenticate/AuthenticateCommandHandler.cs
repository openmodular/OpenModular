using AutoMapper;
using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Auth.Authenticate;

internal class AuthenticateCommandHandler(IEnumerable<IAuthenticationIdentityHandler<User>> identityHandlers, IAuthenticationVerifyHandler<User> verifyHandler, IMapper mapper) : CommandHandler<AuthenticateCommand, AuthenticateDto>
{
    public override async Task<AuthenticateDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var identityHandler = identityHandlers.FirstOrDefault(x => x.Mode == request.Mode && x.Source == request.Source);
        if (identityHandler == null)
        {
            throw new UAPBusinessException(UAPErrorCode.Auth_NotSupportMode);
        }

        var context = new AuthenticationContext<User>
        {
            Mode = request.Mode,
            Source = request.Source,
            IPv4 = request.IPv4,
            IPv6 = request.IPv6,
            Mac = request.Mac,
            Terminal = request.Terminal
        };

        await identityHandler.HandleAsync(request.Payload, context);

        if (context.Success)
        {
            await verifyHandler.HandleAsync(context);
        }

        return mapper.Map<AuthenticateDto>(context);
    }
}