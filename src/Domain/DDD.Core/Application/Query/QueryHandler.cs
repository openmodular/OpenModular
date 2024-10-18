using OpenModular.DDD.Core.Application.Command;

namespace OpenModular.DDD.Core.Application.Query;

/// <summary>
/// 查询处理器基类
/// </summary>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResult"></typeparam>
public abstract class QueryHandler<TQuery, TResult> : HandlerBase, IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
    public abstract Task<TResult> ExecuteAsync(TQuery request, CancellationToken cancellationToken);

    public async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
    {
        var result = await ExecuteAsync(request, cancellationToken);
        return result;
    }
}