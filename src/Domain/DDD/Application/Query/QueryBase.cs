namespace OpenModular.DDD.Application.Query;

public abstract class QueryBase<TRequest>(Guid id) : IQuery<TRequest>
{
    public Guid Id { get; } = id;

    protected QueryBase() : this(Guid.NewGuid())
    {
    }
}