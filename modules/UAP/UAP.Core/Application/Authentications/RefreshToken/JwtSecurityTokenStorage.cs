using OpenModular.Authentication.Abstractions;
using OpenModular.Authentication.JwtBearer;
using OpenModular.DDD.Core.Domain.Entities.TypeIds;
using OpenModular.Module.UAP.Core.Domain.Authentications;

namespace OpenModular.Module.UAP.Core.Application.Authentications.RefreshToken;

internal class JwtSecurityTokenStorage : IJwtSecurityTokenStorage
{
    private readonly IAuthenticationTokenRepository _repository;

    public JwtSecurityTokenStorage(IAuthenticationTokenRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveAsync(UserId userId, AuthenticationClient client, JwtSecurityToken token)
    {
        var entity = await _repository.FindAsync(m => m.Id == userId && m.Client == client);
        var isExists = entity != null;
        if (!isExists)
        {
            entity = AuthenticationToken.Create(userId, client);
        }

        entity.AccessToken = token.AccessToken;
        entity.RefreshToken = token.RefreshToken;
        entity.Expires = DateTimeOffset.UtcNow.AddSeconds(token.ExpiresIn);

        if (isExists)
        {
            await _repository.UpdateAsync(entity);
        }
        else
        {
            await _repository.InsertAsync(entity);
        }
    }
}