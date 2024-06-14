namespace OpenModular.DDD.Core.Application.Query;

public abstract record QueryBase<TRequest>(Guid id) : IQuery<TRequest>
{
    public Guid Id { get; } = id;

    protected QueryBase() : this(Guid.NewGuid())
    {
    }
}