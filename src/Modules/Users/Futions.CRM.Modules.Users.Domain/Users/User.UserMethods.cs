using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Domain.Users.DomainEvents;

namespace Futions.CRM.Modules.Users.Domain.Users;
public sealed partial class User
{
    public static Result<User> Create(Guid roleId, string email, 
        string firstname, string lastname, string identityId)
    {
        Result emailResult = email.Validate(nameof(email), 64, "User", isEmail: true);

        if (emailResult.IsFailure)
        {
            return Result.Failure<User>(emailResult.Error);
        }

        Result firstnameResult = firstname.Validate(nameof(firstname), 64, "User");

        if (firstnameResult.IsFailure)
        {
            return Result.Failure<User>(firstnameResult.Error);
        }

        Result lastnameResult = firstname.Validate(nameof(lastname), 64, "User");

        if (lastnameResult.IsFailure)
        {
            return Result.Failure<User>(firstnameResult.Error);
        }

        var user = new User(email, firstname, lastname, identityId);

        var userRole = UserRole.Create(user.Id, roleId);

        user._userRoles.Add(userRole);

        user.Raise(new UserRegisteredDomainEvents(user.Id));

        return Result.Success(user);
    }

    public Result UpdateFirstname(string firstname)
    {
        Result firstnameResult = firstname.Validate(nameof(firstname), 64, "User");

        if (firstnameResult.IsFailure)
        {
            return Result.Failure<User>(firstnameResult.Error);
        }

        Firstname = firstname;

        return Result.Success();
    }

    public Result UpdateLastname(string lastname)
    {
        Result lastnameResult = lastname.Validate(nameof(lastname), 64, "User");

        if (lastnameResult.IsFailure)
        {
            return Result.Failure<User>(lastnameResult.Error);
        }

        Lastname = lastname;

        return Result.Success();
    }

    public Result UpdateEmail(string email)
    {
        Result emailResult = email.Validate(nameof(email), 64, "User", isEmail: true);

        if (emailResult.IsFailure)
        {
            return Result.Failure<User>(emailResult.Error);
        }

        Email = email;

        return Result.Success();
    }
}
