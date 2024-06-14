namespace OpenModular.DDD.Domain.Entities;

public class AggregateRoot : Entity, IAggregateRoot;

public class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
{
    public AggregateRoot()
    {

    }

    public AggregateRoot(TKey id) : base(id)
    {

    }
}