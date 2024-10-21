namespace OpenModular.Persistence;

/// <summary>
/// 数据库上下文提供者
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public interface IDbContextProvider<TDbContext> where TDbContext : EfDbContext<TDbContext>
{
    /// <summary>
    /// 获取数据库上下文
    /// </summary>
    /// <returns></returns>
    Task<TDbContext> GetDbContextAsync();
}