using Futions.CRM.Common.Domain.Results;
using MediatR;

namespace Futions.CRM.Common.Application.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
