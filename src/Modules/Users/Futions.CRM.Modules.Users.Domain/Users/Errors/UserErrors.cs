using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Users.Domain.Users.Errors;
public static class UserErrors
{
    public static Error NotFound(Guid userId) => Error.NotFound(
        "User.NotFound",
        $"User with ID '{userId}' was not found.");

    public static Error NothingToUpdate => Error.NotFound(
        "User.NothingToUpdate",
        "Nothing to update. Both email and fullname are null or empty.");
}
