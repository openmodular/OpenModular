﻿using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Users;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Auth.Authenticate;

internal class AuthenticateCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : CommandHandler<AuthenticateCommand, AuthenticateDto>
{
    public override async Task<AuthenticateDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        Check.NotNull(request.UserName, nameof(request.UserName));
        Check.NotNull(request.Password, nameof(request.Password));
        Check.NotNull(request.Captcha, nameof(request.Captcha));

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

        return new AuthenticateDto();
    }
}