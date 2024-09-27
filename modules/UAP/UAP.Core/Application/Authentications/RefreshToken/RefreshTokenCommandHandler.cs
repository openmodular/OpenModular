using OpenModular.Authentication.JwtBearer;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Application.Users.Get;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Authentications;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Authentications.RefreshToken;

internal class RefreshTokenCommandHandler : CommandHandler<RefreshTokenCommand, UserDto>
{
    private readonly IAuthenticationTokenRepository _tokenRepository;
    private readonly IJwtOptionsProvider _jwtOptionsProvider;
    private readonly IUserRepository _userRepository;

    public RefreshTokenCommandHandler(IAuthenticationTokenRepository tokenRepository, IJwtOptionsProvider jwtOptionsProvider, IUserRepository userRepository)
    {
        _tokenRepository = tokenRepository;
        _jwtOptionsProvider = jwtOptionsProvider;
        _userRepository = userRepository;
    }

    public override async Task<UserDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Check.NotNull(request.RefreshToken, nameof(request.RefreshToken));

        var token = await _tokenRepository.FindAsync(m => m.RefreshToken == request.RefreshToken, cancellationToken);
        if (token == null)
        {
            throw new UAPBusinessException(UAPErrorCode.Auth_InvalidRefreshToken);
        }

        var jwtOptions = _jwtOptionsProvider.Get();

        if (token.Expires <= DateTimeOffset.UtcNow.AddDays(jwtOptions.Expires))
        {
            throw new UAPBusinessException(UAPErrorCode.Auth_RefreshTokenExpired);
        }

        var user = await _userRepository.FindAsync(m => m.Id == token.Id, cancellationToken);
        if (user == null)
        {
            throw new UAPBusinessException(UAPErrorCode.Auth_UserNotFound);
        }

        switch (user.Status)
        {
            case UserStatus.Inactive:
                throw new UAPBusinessException(UAPErrorCode.User_Inactive);
            case UserStatus.Deleted:
                throw new UAPBusinessException(UAPErrorCode.User_Deleted);
            case UserStatus.Disabled:
                throw new UAPBusinessException(UAPErrorCode.User_Disabled);
            case UserStatus.Unverified:
                throw new UAPBusinessException(UAPErrorCode.User_Unverified);
        }


    }
}