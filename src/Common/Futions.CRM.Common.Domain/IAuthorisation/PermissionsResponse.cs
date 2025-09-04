namespace Futions.CRM.Common.Domain.IAuthorisation;

public sealed record PermissionsResponse(Guid UserId, HashSet<string> Permissions);
