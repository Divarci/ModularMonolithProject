using System.Net;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Domain.Users.Errors;
using Microsoft.Extensions.Logging;

namespace Futions.CRM.Modules.Users.Infrastructure.Identity;
internal sealed class IdentityProviderService(
    KeyCloakClient keyCloakClient, 
    ILogger<IdentityProviderService> logger) : IIdentityProviderService
{
    private const string PasswordCredentialType = "password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(
        string firstname, string lastname, string email, string password,
        CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            FirstName: firstname,
            LastName: lastname,
            Email: email,
            Username: email,
            EmailVerified: true,
            Enabled: true,
            Credentials: [new CredentialRepresentation(
                PasswordCredentialType, password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(
                userRepresentation, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User registration failed");

            return Result.Failure<string>(IdentityErrors.EmailIsNotUnique);
        }
    }
}




