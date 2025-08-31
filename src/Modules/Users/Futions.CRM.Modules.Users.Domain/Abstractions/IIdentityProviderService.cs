using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Users.Domain.Abstractions;
public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(
        string firstname, string lastname, string email, string password, 
        CancellationToken cancellationToken = default);
}
