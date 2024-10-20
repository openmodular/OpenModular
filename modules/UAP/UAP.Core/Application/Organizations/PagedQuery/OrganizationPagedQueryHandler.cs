﻿using AutoMapper;
using OpenModular.DDD.Core.Application.Dto;
using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Application.Organizations.Get;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.PagedQuery;

internal class OrganizationPagedQueryHandler(IOrganizationRepository repository, IMapper mapper) : QueryHandler<OrganizationPagedQueryQuery, PagedDto<OrganizationDto>>
{
    public override async Task<PagedDto<OrganizationDto>> ExecuteAsync(OrganizationPagedQueryQuery request, CancellationToken cancellationToken)
    {
        var pagedResult = await repository.PageQueryAsync(new Domain.Organizations.Models.OrganizationQueryModel
        {
            Name = request.Name,
            Code = request.Code
        }, request.Pagination, cancellationToken);

        return mapper.Map<PagedDto<OrganizationDto>>(mapper);
    }
}