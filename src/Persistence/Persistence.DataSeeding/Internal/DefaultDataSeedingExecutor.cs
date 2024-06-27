using Microsoft.Extensions.Logging;

namespace OpenModular.Persistence.DataSeeding.Internal;

internal class DefaultDataSeedingExecutor(IEnumerable<IDataSeedingHandler> handlers, ILogger<DefaultDataSeedingExecutor> logger) : IDataSeedingExecutor
{
    public async Task ExecuteAsync()
    {
        logger.LogDebug("Default data seeding executor start,the handler count is {count}", handlers.Count());

        foreach (var handler in handlers)
        {
            await handler.DoAsync();
        }

        logger.LogDebug("Default data seeding executor finished");
    }
}