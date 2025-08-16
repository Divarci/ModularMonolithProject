using Futions.CRM.Common.Domain.Results;
using MediatR;

namespace Futions.CRM.Common.Application.Messaging;
public interface IQueryHandler<in TQuery,TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
