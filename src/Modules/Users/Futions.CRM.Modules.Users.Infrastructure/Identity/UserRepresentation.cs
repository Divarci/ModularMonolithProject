namespace Futions.CRM.Modules.Users.Infrastructure.Identity;
internal sealed record UserRepresentation(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    bool EmailVerified,
    bool Enabled,
    CredentialRepresentation[] Credentials);

internal sealed record CredentialRepresentation(
    string Type, 
    string Value, 
    bool Temporary);
