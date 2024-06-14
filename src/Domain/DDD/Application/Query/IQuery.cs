namespace OpenModular.DDD.Application.Query;

public interface IQuery<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}