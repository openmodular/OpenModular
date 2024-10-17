using OpenModular.Common.Utils;
using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 命令处理器基类
/// </summary>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResult"></typeparam>
public abstract class QueryHandler<TQuery, TResult> : HandlerBase, IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    public abstract Task<TResult> ExecuteAsync(TQuery request, CancellationToken cancellationToken);

    public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var unitOfWorkManager = GlobalServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        using var uow = unitOfWorkManager.Begin();

        var result = await ExecuteAsync(request, cancellationToken);

        await uow.CompleteAsync(cancellationToken);

        return result;
    }
}