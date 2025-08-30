using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Domain.Users;
using Futions.CRM.Modules.Users.Domain.Users.Errors;

namespace Futions.CRM.Modules.Users.Application.Users.Commands.UpdateUser;
internal sealed class UpdateUserCommandHandler(
    IUsersUnitOfWork unitOfWork) : ICommandHandler<UpdateUserCommand>
{
    private readonly IUsersUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(
        UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _unitOfWork
            .GetReadRepository<User>()
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(
                UserErrors.NotFound(request.UserId));
        }

        if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.Fullname))
        {
            return Result.Failure(
                UserErrors.NothingToUpdate);
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            Result result = user.UpdateEmail(request.Email);

            if(result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Fullname))
        {
            Result result = user.UpdateFulname(request.Fullname);

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }
        }

        _unitOfWork
            .GetWriteRepository<User>()
            .Update(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success();
    }
}
