namespace OpenModular.Persistence.DataSeeding.Internal;

internal class DefaultDataSeedingExecutor : IDataSeedingExecutor
{
    private readonly IEnumerable<IDataSeedingHandler> _handlers;

    public DefaultDataSeedingExecutor(IEnumerable<IDataSeedingHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task ExecuteAsync()
    {
        foreach (var handler in _handlers)
        {
            await handler.DoAsync();
        }
    }
}