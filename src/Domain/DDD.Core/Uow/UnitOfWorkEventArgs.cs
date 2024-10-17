namespace OpenModular.DDD.Core.Uow;

public class UnitOfWorkEventArgs : EventArgs
{
    public IUnitOfWork UnitOfWork { get; }

    public UnitOfWorkEventArgs(IUnitOfWork unitOfWork)
    {
        Check.NotNull(unitOfWork, nameof(unitOfWork));

        UnitOfWork = unitOfWork;
    }
}