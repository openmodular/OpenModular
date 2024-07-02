namespace OpenModular.DDD.Core.Application.Command;

public abstract class CommandBase : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
}

public abstract class CommandBase<TResult> : ICommand<TResult>
{
    public Guid CommandId { get; } = Guid.NewGuid();
}