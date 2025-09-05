using Futions.CRM.Common.Domain.Abstractions.Authorisations;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Application.Users.Queries.GetUserPermissions;
using MediatR;

namespace Futions.CRM.Modules.Users.Infrastructure.Authorisation;
internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)    
        => await sender.Send(new GetUserPermissionsQuery(identityId));
    
}
