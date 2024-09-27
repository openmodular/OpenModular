using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Authentication.Abstractions;
using OpenModular.Authentication.JwtBearer;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;
using OpenModular.Module.UAP.Core.Application.Users.Get;
using OpenModular.Module.UAP.Web.Models.Auths;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("身份认证")]
public class AuthenticationController : ModuleController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly JwtSecurityTokenBuilder _builder;
    private readonly ITenantResolver _tenantResolver;

    public AuthenticationController(IMediator mediator, IMapper mapper, JwtSecurityTokenBuilder builder, ITenantResolver tenantResolver)
    {
        _mediator = mediator;
        _mapper = mapper;
        _builder = builder;
        _tenantResolver = tenantResolver;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [EndpointDescription("登录")]
    public async Task<APIResponse<LoginResponse>> Login(LoginRequest request)
    {
        var command = _mapper.Map<AuthenticateCommand>(request);
        command.TenantId = await _tenantResolver.ResolveAsync();
        command.IPv4 = IPv4;
        command.IPv6 = IPv6;

        var dto = await _mediator.Send(command);

        var response = new LoginResponse
        {
            Status = dto.Status,
            Message = dto.Message
        };

        if (dto.Success)
        {
            response.Jwt = BuildJwtCredential(dto.User, command.TenantId, dto.AuthenticateTime);
        }

        return APIResponse.Success(response);
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    [EndpointDescription("刷新令牌")]
    public async Task<APIResponse<JwtSecurityToken>> RefreshToken()
    {
        var credential = BuildJwtCredential(null, null, DateTimeOffset.UtcNow);

        return APIResponse.Success(credential);
    }

    private JwtSecurityToken BuildJwtCredential(UserDto user, TenantId tenantId, DateTimeOffset loginTime)
    {
        var claims = new List<Claim>
        {
            new(OpenModularClaimTypes.TENANT_ID, tenantId  != null ? tenantId.Value.ToString() : ""),
            new(OpenModularClaimTypes.USER_ID, user.Id.ToString()),
            new(OpenModularClaimTypes.USER_NAME, user.Username),
            new(OpenModularClaimTypes.LOGIN_TIME, loginTime.ToString())
        };

        return _builder.Build(claims);
    }
}