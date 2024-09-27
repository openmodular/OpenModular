using OpenModular.DDD.Core.Domain.Repositories;

namespace OpenModular.Module.UAP.Core.Domain.Authentications;

/// <summary>
/// 认证记录仓储
/// </summary>
public interface IAuthenticationRecordRepository : IRepository<AuthenticationRecord, int>
{

}