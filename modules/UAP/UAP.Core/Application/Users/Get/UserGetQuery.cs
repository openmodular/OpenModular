using OpenModular.DDD.Core.Application.Query;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;

namespace OpenModular.Module.UAP.Core.Application.Users.Get;

public record UserGetQuery(UserId UserId) : QueryBase<UserDto>;