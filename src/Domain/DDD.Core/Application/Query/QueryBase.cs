namespace OpenModular.DDD.Core.Application.Query;

public abstract record QueryBase<TDto>(Guid Id) : IQuery<TDto>
{
    protected QueryBase() : this(Guid.NewGuid())
    {
    }
}