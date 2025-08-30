using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Domain.Users;

namespace Futions.CRM.Modules.Users.Application.Users.Commands.RegisterUser;
internal sealed class RegisterUserCommandHandler(
    IUsersUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUsersUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Result<User> result = User.Create(request.Email, request.Fullname);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        await _unitOfWork
            .GetWriteRepository<User>()
            .CreateAsync(result.Value, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
