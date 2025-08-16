using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.Exceptions;
public class DomainException : Exception
{
    public DomainException(
        string entityName,
        Error? error = default,
        Exception? innerException = default) : base("Domain exception", innerException)
    {
        EntityName = entityName;
        Error = error;
    }

    public string EntityName { get; }

    public Error? Error { get; }
}
