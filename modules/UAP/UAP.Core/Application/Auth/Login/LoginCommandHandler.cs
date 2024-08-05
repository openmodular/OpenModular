using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Auth.Login;

internal class LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : CommandHandler<LoginCommand, LoginDto>
{
    public override async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Check.NotNullOrWhiteSpace(request.UserName, nameof(request.UserName));
        Check.NotNullOrWhiteSpace(request.Password, nameof(request.Password));
        Check.NotNullOrWhiteSpace(request.VerifyCode, nameof(request.VerifyCode));

        var user = await userRepository.GetAsync(m => m.UserName == request.UserName, cancellationToken);

        var passwordHash = passwordHasher.HashPassword(user, request.Password);
        if (passwordHasher.VerifyHashedPassword(user, passwordHash, user.PasswordHash))
        {
            throw new UAPBusinessException(UAPErrorCode.Auth_PasswordError);
        }

        switch (user.Status)
        {
            case UserStatus.Deleted:
                throw new UAPBusinessException(UAPErrorCode.Auth_UserDeleted);
            case UserStatus.Disabled:
                throw new UAPBusinessException(UAPErrorCode.Auth_UserDisabled);
        }

        return new LoginDto();
    }
}