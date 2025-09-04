//using Futions.CRM.Common.Application.Messaging;
//using Futions.CRM.Common.Domain.IAuthorisation;
//using Futions.CRM.Common.Domain.IUnitOfWorks;
//using Futions.CRM.Common.Domain.Results;
//using Futions.CRM.Modules.Users.Domain.Users;

//namespace Futions.CRM.Modules.Users.Application.Users.Queries.GetUserPermissions;
//internal sealed class GetUserPermissionsQueryHandler(
//    IUnitOfWork unitOfWork) : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
//{
//    private readonly IUnitOfWork _unitOfWork = unitOfWork;

//    public async Task<Result<PermissionsResponse>> Handle(
//        GetUserPermissionsQuery request, CancellationToken cancellationToken)
//    {
//        var test = _unitOfWork.GetReadRepository<User>()
//            .GetAll()
//            .Where(x=>x.IdentityId == request.IdentityId)
//            .SelectMany(x=>x.Roles)
//            .Select(x=> new UserPermission
//            {
//                UserId =,
//                Permission = x.Permission
//            })
//}
//internal sealed class UserPermission
//{
//    internal Guid UserId { get; init; }

//    internal string Permission { get; init; }
//}
