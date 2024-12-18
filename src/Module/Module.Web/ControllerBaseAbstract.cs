﻿using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenModular.Authentication.Abstractions;
using OpenModular.Common.Utils;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.Web;

/// <summary>
/// 控制器抽象基类
/// </summary>
[ApiController]
public abstract class ControllerBaseAbstract : ControllerBase
{
    /// <summary>
    /// AutoMapper IMapper服务
    /// </summary>
    public IMapper ObjectMapper => GlobalServiceProvider.GetRequiredService<IMapper>();

    /// <summary>
    /// MediatoR服务
    /// </summary>
    public IMediator Mediator => GlobalServiceProvider.GetRequiredService<IMediator>();

    /// <summary>
    /// 当前租户标识
    /// </summary>
    public TenantId? CurrentTenantId
    {
        get
        {
            var tenantId = User.FindFirst(CustomClaimTypes.TENANT_ID);

            if (tenantId != null && tenantId.Value.NotNullOrWhiteSpace())
            {
                return new TenantId(tenantId.Value);
            }

            return null;
        }
    }

    /// <summary>
    /// 当前登录用户标识
    /// </summary>
    public AccountId? CurrentAccountId
    {
        get
        {
            var accountId = User.FindFirst(CustomClaimTypes.ACCOUNT_ID);

            if (accountId != null && accountId.Value.NotNullOrWhiteSpace())
            {
                return new AccountId(accountId.Value);
            }

            return null;
        }
    }

    /// <summary>
    /// 当前用户角色列表
    /// </summary>
    public List<string> CurrentRoles
    {
        get
        {
            return User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
        }
    }

    /// <summary>
    /// 获取当前用户IP(包含IPv和IPv6)
    /// </summary>
    public string? IP => HttpContext.Connection.RemoteIpAddress?.ToString();

    /// <summary>
    /// 获取当前用户IPv4
    /// </summary>
    public string? IPv4 => HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

    /// <summary>
    /// 获取当前用户IPv6
    /// </summary>
    public string? IPv6 => HttpContext.Connection.RemoteIpAddress?.MapToIPv6().ToString();

    /// <summary>
    /// 登录时间
    /// </summary>
    public long LoginTime
    {
        get
        {
            var ty = User.FindFirst(CustomClaimTypes.LOGIN_TIME);

            if (ty != null && ty.Value.NotNullOrWhiteSpace())
            {
                return ty.Value.ToLong();
            }

            return 0L;
        }
    }

    /// <summary>
    /// User-Agent
    /// </summary>
    public string? UserAgent => HttpContext.Request.Headers["User-Agent"];

    /// <summary>
    /// 将Request映射为Command
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <param name="request"></param>
    /// <returns></returns>
    protected TCommand Request2Command<TCommand>(object request) where TCommand : ICommand
    {
        var command = ObjectMapper.Map<TCommand>(request);
        command.OperatorId = CurrentAccountId;
        return command;
    }

    /// <summary>
    /// 将Request映射为Command
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="request"></param>
    /// <returns></returns>
    protected TCommand Request2Command<TCommand, TResult>(object request) where TCommand : ICommand<TResult>
    {
        var command = ObjectMapper.Map<TCommand>(request);
        command.OperatorId = CurrentAccountId;
        return command;
    }

    /// <summary>
    /// 将Request映射为Query
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="request"></param>
    /// <returns></returns>
    protected TQuery Request2Query<TQuery, TResult>(object request) where TQuery : IQuery<TResult>
    {
        var query = ObjectMapper.Map<TQuery>(request);
        query.OperatorId = CurrentAccountId;
        return query;
    }
}