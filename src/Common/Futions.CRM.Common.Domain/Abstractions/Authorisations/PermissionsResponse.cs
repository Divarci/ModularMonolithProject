namespace Futions.CRM.Common.Domain.Abstractions.Authorisations;

public sealed record PermissionsResponse(Guid UserId, HashSet<string> Permissions);
