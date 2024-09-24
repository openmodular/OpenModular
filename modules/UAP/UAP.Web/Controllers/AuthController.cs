using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Authentication.Abstractions;
using OpenModular.Authentication.JwtBearer;
using OpenModular.Module.UAP.Core.Application.Auth.Authenticate;
using OpenModular.Module.UAP.Web.Models.Auths;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("身份认证")]
public class AuthController : ModuleController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly JwtCredentialBuilder _builder;

    public AuthController(IMediator mediator, IMapper mapper, JwtCredentialBuilder builder)
    {
        _mediator = mediator;
        _mapper = mapper;
        _builder = builder;
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
            var claims = new List<Claim>
            {
                new(OpenModularClaimTypes.TENANT_ID, dto.TenantId != null ? dto.TenantId.ToString() : ""),
                new(OpenModularClaimTypes.USER_ID, dto.User.Id.ToString()),
                new(OpenModularClaimTypes.USER_NAME, dto.User.Username),
                new(OpenModularClaimTypes.LOGIN_TIME, dto.AuthenticateTime.ToString())
            };

            response.Jwt = _builder.Build(claims);
        }

        return APIResponse.Success(response);
    }
}