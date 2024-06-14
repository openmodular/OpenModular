namespace OpenModular.DDD.Core.Application.Command;

public abstract record CommandBase(Guid id) : ICommand
{
    public Guid Id { get; } = id;

    protected CommandBase() : this(Guid.NewGuid())
    {
    }
}

public abstract record CommandBase<TResult>(Guid id) : ICommand<TResult>
{
    public Guid Id { get; } = id;

    protected CommandBase() : this(Guid.NewGuid())
    {
    }
}