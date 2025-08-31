using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Users.Domain.Users.Errors;
public static class IdentityErrors
{
    public static readonly Error EmailIsNotUnique = Error.Conflict(
        "Identity.EmailIsNotUnique",
        "The specified email is not unique.");
}
