using OpenModular.DDD.Core.Application.Query;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Users;

namespace OpenModular.Module.UAP.Core.Application.Users.GetUser;

public record UserGetQuery(UserId UserId) : QueryBase<UserDto>;