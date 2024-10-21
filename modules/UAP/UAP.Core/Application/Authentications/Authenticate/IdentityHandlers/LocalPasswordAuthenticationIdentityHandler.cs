using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils.DependencyInjection;
using OpenModular.Common.Utils.Extensions;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Infrastructure;

namespace OpenModular.Module.UAP.Core.Application.Authentications.Authenticate.IdentityHandlers;

/// <summary>
/// 本地密码认证身份处理器实现
/// </summary>
internal class LocalPasswordAuthenticationIdentityHandler : IAuthenticationIdentityHandler<Account>, ITransientDependency
{
    public AuthenticationMode Mode => AuthenticationMode.Password;

    public AuthenticationSource Source => AuthenticationSource.Local;

    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IImageCaptchaService _imageCaptchaService;
    private readonly UAPConfig _config;

    public LocalPasswordAuthenticationIdentityHandler(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IImageCaptchaService imageCaptchaService, UAPConfig config)
    {
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _imageCaptchaService = imageCaptchaService;
        _config = config;
    }

    public async Task HandleAsync(string? payload, AuthenticationContext<Account> context, CancellationToken cancellationToken)
    {
        if (payload!.IsNullOrWhiteSpace())
        {
            context.Status = AuthenticationStatus.InvalidIdentity;
            context.Message = "Authentication failed, invalid authentication identity";

            return;
        }

        var identity = payload.ToModel<PasswordIdentity>();
        if (identity == null || identity.UserName!.IsNullOrWhiteSpace() || identity.Password!.IsNullOrWhiteSpace())
        {
            context.Status = AuthenticationStatus.InvalidIdentity;
            context.Message = "Authentication failed, invalid authentication identity";
            return;
        }

        if (_config.Authentication.ImageCaptcha.IsEnabled)
        {
            if (identity.CaptchaId!.IsNullOrWhiteSpace() || identity.Captcha!.IsNullOrWhiteSpace() || !await _imageCaptchaService.VerifyAsync(identity.CaptchaId!, identity.Captcha!))
            {
                context.Status = AuthenticationStatus.InvalidImageCaptcha;
                context.Message = "Authentication failed, invalid image captcha";
                return;
            }
        }

        var account = await _accountRepository.FindAsync(m => m.UserName == identity.UserName, cancellationToken);
        if (account == null)
        {
            context.Status = AuthenticationStatus.AccountNotFound;
            context.Message = "Authentication failed, user not found";
            return;
        }

        if (!_passwordHasher.VerifyHashedPassword(account, account.PasswordHash!, identity.Password!))
        {
            context.Status = AuthenticationStatus.IncorrectPassword;
            context.Message = "Authentication failed, invalid password";
            return;
        }

        context.Account = account;
    }
}

/// <summary>
/// 账密身份
/// </summary>
public class PasswordIdentity
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 图形验证码标识
    /// </summary>
    public string? CaptchaId { get; set; }

    /// <summary>
    /// 图形验证码
    /// </summary>
    public string? Captcha { get; set; }
}