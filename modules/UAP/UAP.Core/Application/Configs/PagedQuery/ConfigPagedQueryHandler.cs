﻿using AutoMapper;
using OpenModular.DDD.Core.Application.Dto;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Configs;
using OpenModular.Module.UAP.Core.Domain.Configs.Models;

namespace OpenModular.Module.UAP.Core.Application.Configs.PagedQuery;

internal class ConfigPagedQueryHandler(IConfigRepository repository, IMapper mapper) : QueryHandler<ConfigPagedQuery, PagedDto<ConfigDto>>
{
    public override async Task<PagedDto<ConfigDto>> ExecuteAsync(ConfigPagedQuery request, CancellationToken cancellationToken)
    {
        var queryModel = new ConfigPagedQueryModel
        {
            ModuleCode = request.ModuleCode
        };

        var pagedResult = await repository.PagedQueryAsync(queryModel, request.Pagination, cancellationToken);

        return mapper.Map<PagedDto<ConfigDto>>(pagedResult);
    }
}