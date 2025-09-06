using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.Exceptions;
public class CrmException : Exception
{
    public CrmException(
        string requestName, 
        Error? error = default, 
        Exception? innerException = default) :base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
