using OpenModular.DDD.Core.Application.Query;

namespace OpenModular.Module.UAP.Core.Application.Users.GetUser;

internal class UserGetQueryHandler : IQueryHandler<UserGetQuery, UserDto>
{
    public Task<UserDto> Handle(UserGetQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}