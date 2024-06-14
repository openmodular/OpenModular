using MediatR;

namespace OpenModular.DDD.Core.Application.Query;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>;