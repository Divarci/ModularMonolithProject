using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Abstractions.Authorisations;

namespace Futions.CRM.Modules.Users.Application.Users.Queries.GetUserPermissions;
public record GetUserPermissionsQuery(
    string IdentityId) : IQuery<PermissionsResponse>;
