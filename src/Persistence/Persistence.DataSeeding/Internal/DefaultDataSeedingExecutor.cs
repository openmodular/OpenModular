namespace OpenModular.Persistence.DataSeeding.Internal;

internal class DefaultDataSeedingExecutor(IEnumerable<IDataSeedingHandler> handlers) : IDataSeedingExecutor
{
    public async Task ExecuteAsync()
    {
        foreach (var handler in handlers)
        {
            await handler.DoAsync();
        }
    }
}