using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.Abstractions.Authorisations;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
