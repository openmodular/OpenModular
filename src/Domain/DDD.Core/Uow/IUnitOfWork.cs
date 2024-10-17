namespace OpenModular.DDD.Core.Uow;

/// <summary>
/// 工作单元接口
/// </summary>
public interface IUnitOfWork : IDisposable
{
    event EventHandler<UnitOfWorkEventArgs> Disposed;

    Task CompleteAsync(CancellationToken cancellationToken = default);

    Task RollbackAsync(CancellationToken cancellationToken = default);
}