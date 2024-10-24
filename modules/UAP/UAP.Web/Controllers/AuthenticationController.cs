﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Authentication.Abstractions;
using OpenModular.Authentication.JwtBearer;
using OpenModular.Module.UAP.Core.Application.Authentications.Authenticate;
using OpenModular.Module.UAP.Core.Application.Authentications.RefreshToken;
using OpenModular.Module.UAP.Web.Models.Authentications;
using OpenModular.Module.Web;

namespace OpenModular.Module.UAP.Web.Controllers;

[Tags("身份认证")]
public class AuthenticationController : ModuleController
{
    private readonly JwtSecurityTokenBuilder _builder;
    private readonly ITenantResolver _tenantResolver;
    private readonly IJwtSecurityTokenStorage _tokenStorage;

    public AuthenticationController(JwtSecurityTokenBuilder builder, ITenantResolver tenantResolver, IJwtSecurityTokenStorage tokenStorage)
    {
        _builder = builder;
        _tenantResolver = tenantResolver;
        _tokenStorage = tokenStorage;
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
        var command = ObjectMapper.Map<AuthenticateCommand>(request);
        command.TenantId = await _tenantResolver.ResolveAsync();
        command.IPv4 = IPv4;
        command.IPv6 = IPv6;

        var dto = await Mediator.Send(command);

        var response = new LoginResponse
        {
            Status = dto.Status,
            Message = dto.Message
        };

        if (dto.Success)
        {
            response.Jwt = await _builder.BuildAsync(dto.Claims);

            await _tokenStorage.SaveAsync(dto.Account.Id, dto.Client, response.Jwt);
        }

        return APIResponse.Success(response);
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [EndpointDescription("刷新令牌")]
    public async Task<APIResponse<JwtSecurityToken>> RefreshToken(RefreshTokenRequest request)
    {
        var command = new RefreshTokenCommand { RefreshToken = request.RefreshToken, Client = AuthenticationClient.Find(request.Client) };
        var user = await Mediator.Send(command);
        var credential = await _builder.BuildAsync(User.Claims.ToList());

        await _tokenStorage.SaveAsync(user.Id, command.Client, credential);

        return APIResponse.Success(credential);
    }
}