namespace OpenModular.DDD.Core.Application.Command;

public abstract record CommandBase(Guid CommandId) : ICommand
{
    protected CommandBase() : this(Guid.NewGuid())
    {
    }
}

public abstract record CommandBase<TResult>(Guid CommandId) : ICommand<TResult>
{
    protected CommandBase() : this(Guid.NewGuid())
    {
    }
}