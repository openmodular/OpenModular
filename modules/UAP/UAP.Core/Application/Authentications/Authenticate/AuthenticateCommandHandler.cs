using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenModular.Authentication.Abstractions;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.Module.UAP.Core.Conventions;
using OpenModular.Module.UAP.Core.Domain.Accounts;
using OpenModular.Module.UAP.Core.Domain.Authentications;

namespace OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;

internal class AuthenticateCommandHandler : CommandHandler<AuthenticateCommand, AuthenticateDto>
{
    private readonly IEnumerable<IAuthenticationIdentityHandler<Account>> _identityHandlers;
    private readonly IAuthenticationVerifyHandler<Account> _verifyHandler;
    private readonly IMapper _mapper;
    private readonly IAuthenticationRecordRepository _recordRepository;
    private readonly ILogger<AuthenticateCommandHandler> _logger;
    private readonly UAPModuleLocalizer _localizer;

    public AuthenticateCommandHandler(IEnumerable<IAuthenticationIdentityHandler<Account>> identityHandlers, IAuthenticationVerifyHandler<Account> verifyHandler, IMapper mapper, IAuthenticationRecordRepository recordRepository, ILogger<AuthenticateCommandHandler> logger, UAPModuleLocalizer localizer)
    {
        _identityHandlers = identityHandlers;
        _verifyHandler = verifyHandler;
        _mapper = mapper;
        _recordRepository = recordRepository;
        _logger = logger;
        _localizer = localizer;
    }

    public override async Task<AuthenticateDto> ExecuteAsync(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var context = new AuthenticationContext<Account>
        {
            Mode = request.Mode,
            Source = request.Source,
            IPv4 = request.IPv4,
            IPv6 = request.IPv6,
            Mac = request.Mac,
            Client = request.Client
        };

        var identityHandler = _identityHandlers.FirstOrDefault(x => x.Mode == request.Mode && x.Source == request.Source);
        if (identityHandler == null)
        {
            context.Status = AuthenticationStatus.InvalidMode;
            context.Message = _localizer["Authentication failed, invalid authentication mode"];
            return _mapper.Map<AuthenticateDto>(context);
        }

        await identityHandler.HandleAsync(request.Payload, context, cancellationToken);

        if (context.Success)
        {
            await _verifyHandler.HandleAsync(context, cancellationToken);
        }

        await AddAuthenticationRecordAsync(context, cancellationToken);

        context.Claims.Add(new(CustomClaimTypes.TENANT_ID, request.TenantId != null ? request.TenantId.Value.ToString() : ""));
        context.Claims.Add(new(CustomClaimTypes.ACCOUNT_ID, context.Account!.Id.ToString()));
        context.Claims.Add(new(CustomClaimTypes.LOGIN_TIME, context.AuthenticateTime.ToString()));

        return _mapper.Map<AuthenticateDto>(context);
    }

    /// <summary>
    /// 添加认证记录
    /// </summary>
    private async Task AddAuthenticationRecordAsync(AuthenticationContext<Account> context, CancellationToken cancellationToken)
    {
        try
        {
            var record = AuthenticationRecord.Create(context.Mode, context.Source, context.Client, context.AuthenticateTime);
            record.Client = context.Client;
            record.IPv4 = context.IPv4;
            record.IPv6 = context.IPv6;
            record.Mac = context.Mac;
            record.Status = context.Status;
            record.Message = context.Message;

            if (context.Account != null)
            {
                record.AccountId = context.Account.Id;
            }

            await _recordRepository.InsertAsync(record, true, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Insert authentication record failed.");
        }
    }
}