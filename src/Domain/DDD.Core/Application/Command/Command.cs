namespace OpenModular.DDD.Core.Application.Command;

public abstract class Command : ICommand
{
    public Guid CommandId { get; } = Guid.NewGuid();
}

public abstract class Command<TResult> : ICommand<TResult>
{
    public Guid CommandId { get; } = Guid.NewGuid();
}