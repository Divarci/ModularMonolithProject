using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Users.Domain.Users.DomainEvents;

namespace Futions.CRM.Modules.Users.Domain.Users;
public sealed partial class User
{
    public static Result<User> Create(string email, string fullname)
    {
        Result emailResult = email.Validate(nameof(email), 64, "User", isEmail: true);

        if (emailResult.IsFailure)
        {
            return Result.Failure<User>(emailResult.Error);
        }

        Result fullnameResult = fullname.Validate(nameof(fullname), 64, "User");

        if (fullnameResult.IsFailure)
        {
            return Result.Failure<User>(fullnameResult.Error);
        }

        var user = new User(email, fullname);

        user.Raise(new UserRegisteredDomainEvents(user.Id));

        return Result.Success(user);
    }

    public Result UpdateFulname(string fullname)
    {
        Result fullnameResult = fullname.Validate(nameof(fullname), 64, "User");

        if (fullnameResult.IsFailure)
        {
            return Result.Failure<User>(fullnameResult.Error);
        }

        Fullname = fullname;

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
