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

    public AuthenticateCommandHandler(IEnumerable<IAuthenticationIdentityHandler<Account>> identityHandlers, IAuthenticationVerifyHandler<Account> verifyHandler, IMapper mapper, IAuthenticationRecordRepository recordRepository, ILogger<AuthenticateCommandHandler> logger)
    {
        _identityHandlers = identityHandlers;
        _verifyHandler = verifyHandler;
        _mapper = mapper;
        _recordRepository = recordRepository;
        _logger = logger;
    }

    public override async Task<AuthenticateDto> ExecuteAsync(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var identityHandler = _identityHandlers.FirstOrDefault(x => x.Mode == request.Mode && x.Source == request.Source);
        if (identityHandler == null)
        {
            throw new UAPBusinessException(UAPErrorCode.Auth_NotSupportMode);
        }

        var context = new AuthenticationContext<Account>
        {
            Mode = request.Mode,
            Source = request.Source,
            IPv4 = request.IPv4,
            IPv6 = request.IPv6,
            Mac = request.Mac,
            Client = request.Client
        };

        await identityHandler.HandleAsync(request.Payload, context, cancellationToken);

        if (context.Success)
        {
            await _verifyHandler.HandleAsync(context, cancellationToken);
        }

        #region ==添加认证记录==

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

            await _recordRepository.InsertAsync(record, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Insert authentication record failed.");
        }

        #endregion

        return _mapper.Map<AuthenticateDto>(context);
    }
}