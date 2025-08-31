using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Domain.Users;
using Futions.CRM.Modules.Users.Domain.Users.Errors;

namespace Futions.CRM.Modules.Users.Application.Users.Queries.GetUserById;
internal sealed class GetUserByIdQueryHandler(
    IUsersUnitOfWork unitOfWork) : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUsersUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UserDto>> Handle(
        GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.GetReadRepository<User>()
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(UserErrors.NotFound(request.UserId));
        }

        return Result.Success(new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Firstname = user.Firstname,
            Lastname = user.Lastname
        });
    }
}
