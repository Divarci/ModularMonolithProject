using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Abstractions.Authorisations;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace Futions.CRM.Modules.Users.Application.Users.Queries.GetUserPermissions;
internal sealed class GetUserPermissionsQueryHandler(
    IUsersUnitOfWork unitOfWork) : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
{
    private readonly IUsersUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<PermissionsResponse>> Handle(
        GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        UserPermission[] userPermissions = await _unitOfWork.GetReadRepository<RolePermission>()
            .GetAll()
            .Where(x => x.Role.UserRoles.Any(x => x.User.IdentityId == request.IdentityId))
            .Select(x => x.Permission)
            .Select(x => new UserPermission
            {
                UserId = x.RolePermissions.First().Role.UserRoles.First().UserId,
                Permission = x.Code
            })
            .ToArrayAsync(cancellationToken);

        PermissionsResponse response = new(
            userPermissions[0].UserId,
            [.. userPermissions.Select(x => x.Permission)]);

        return Result.Success(response);
    }

    internal sealed class UserPermission
    {
        internal Guid UserId { get; init; }

        internal string Permission { get; init; }
    }
}
