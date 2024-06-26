﻿using OpenModular.DDD.Core.Application.Query;
using OpenModular.Module.UAP.Core.Domain.Organizations;

namespace OpenModular.Module.UAP.Core.Application.Organizations.GetOrganizationTree;

internal record GetOrganizationTreeQuery(OrganizationId OrganizationId) : QueryBase<OrganizationTreeDto>;