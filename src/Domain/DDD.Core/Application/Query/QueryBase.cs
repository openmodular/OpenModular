namespace OpenModular.DDD.Core.Application.Query;

public abstract class QueryBase<TDto> : IQuery<TDto>
{
    public Guid QueryId { get; } = Guid.NewGuid();
}