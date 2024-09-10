using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Auth.Authenticate;

internal class AuthenticateCommandHandler(IEnumerable<IAuthenticationIdentityHandler<User>> identityHandlers) : CommandHandler<AuthenticateCommand, AuthenticateDto>
{
    public override async Task<AuthenticateDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        Check.NotNull(request.IdentityJson, nameof(request.IdentityJson));

        var identityHandler = identityHandlers.FirstOrDefault(x => x.Mode == request.Mode && x.Source == request.Source);
        if (identityHandler == null)
        {
            throw new UAPBusinessException(UAPErrorCode.Auth_InvalidAuthenticationMode);
        }

        switch (user.Status)
        {
            case UserStatus.Deleted:
                throw new UAPBusinessException(UAPErrorCode.Auth_UserDeleted);
            case UserStatus.Disabled:
                throw new UAPBusinessException(UAPErrorCode.Auth_UserDisabled);
        }

        return new AuthenticateDto();
    }
}