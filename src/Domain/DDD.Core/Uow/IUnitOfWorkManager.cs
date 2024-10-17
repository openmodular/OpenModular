namespace OpenModular.DDD.Core.Uow;

/// <summary>
/// 工作单元管理器
/// </summary>
public interface IUnitOfWorkManager
{
    /// <summary>
    /// 当前工作单元
    /// </summary>
    IUnitOfWork? Current { get; }

    /// <summary>
    /// 开启一个新的工作单元
    /// </summary>
    /// <returns></returns>
    IUnitOfWork Begin();
}