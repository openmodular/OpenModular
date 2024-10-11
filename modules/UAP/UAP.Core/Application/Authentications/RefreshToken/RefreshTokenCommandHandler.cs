using AutoMapper;
using OpenModular.Authentication.JwtBearer;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Application.Accounts.Get;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Domain.Authentications;

namespace OpenModular.Module.UAP.Core.Application.Authentications.RefreshToken;

internal class RefreshTokenCommandHandler : CommandHandler<RefreshTokenCommand, AccountDto>
{
    private readonly IAuthenticationTokenRepository _tokenRepository;
    private readonly IJwtOptionsProvider _jwtOptionsProvider;
    private readonly IAccountRepository _userRepository;
    private readonly IMapper _mapper;

    public RefreshTokenCommandHandler(IAuthenticationTokenRepository tokenRepository, IJwtOptionsProvider jwtOptionsProvider, IAccountRepository userRepository, IMapper mapper)
    {
        _tokenRepository = tokenRepository;
        _jwtOptionsProvider = jwtOptionsProvider;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public override async Task<AccountDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        Check.NotNull(request.RefreshToken, nameof(request.RefreshToken));

        var token = await _tokenRepository.FindAsync(m => m.RefreshToken == request.RefreshToken && m.Client == request.Client, cancellationToken);
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
            case AccountStatus.Inactive:
                throw new UAPBusinessException(UAPErrorCode.Account_Inactive);
            case AccountStatus.Deleted:
                throw new UAPBusinessException(UAPErrorCode.Account_Deleted);
            case AccountStatus.Disabled:
                throw new UAPBusinessException(UAPErrorCode.Account_Disabled);
            case AccountStatus.Unverified:
                throw new UAPBusinessException(UAPErrorCode.Account_Unverified);
        }

        return _mapper.Map<AccountDto>(user);
    }
}