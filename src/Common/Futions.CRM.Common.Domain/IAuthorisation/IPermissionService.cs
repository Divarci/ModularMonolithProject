using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.IAuthorisation;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
