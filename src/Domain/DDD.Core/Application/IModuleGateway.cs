using OpenModular.DDD.Core.Application.Command;
using OpenModular.DDD.Core.Application.Query;

namespace OpenModular.DDD.Core.Application;

/// <summary>
/// The gateway for module, this is the entry point for the module.
/// </summary>
public interface IModuleGateway
{
    /// <summary>
    /// Execute a command.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task ExecuteCommandAsync(ICommand command);

    /// <summary>
    /// Execute a command and return a TResult
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

    /// <summary>
    /// Execute a query and return a TResult
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}