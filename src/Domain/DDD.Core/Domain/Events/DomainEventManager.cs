namespace OpenModular.DDD.Core.Domain.Events;

public sealed class DomainEventManager
{
    public static DomainEventManager Instance = new();

    private readonly AsyncLocal<List<IDomainEvent>> _events;

    private DomainEventManager()
    {
        _events = new AsyncLocal<List<IDomainEvent>>();
    }

    public void Add(IDomainEvent domainEvent)
    {
        _events.Value ??= new List<IDomainEvent>();

        _events.Value.Add(domainEvent);
    }

    public List<IDomainEvent>? GetAllEvents()
    {
        return _events.Value;
    }
}