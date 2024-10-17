using Microsoft.Extensions.DependencyInjection;
using OpenModular.DDD.Core.Uow;

namespace OpenModular.Persistence.Uow;

internal class UnitOfWorkManager : IUnitOfWorkManager
{
    public IUnitOfWork? Current => _current.Value;

    private readonly AsyncLocal<IUnitOfWork?> _current = new();

    private readonly IServiceProvider _serviceProvider;

    public UnitOfWorkManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IUnitOfWork Begin()
    {
        var outerUow = _current.Value;
        var scope = _serviceProvider.CreateScope();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        _current.Value = uow;

        uow.Disposed += (_, _) =>
        {
            _current.Value = outerUow;
            scope.Dispose();
        };

        return uow;
    }
}