using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Organisations.Domain.ShadowTables.People;
public sealed partial class Person
{
    public static Result<Person> Create(string firstName, string? lastName)
    {
        Result resultFirstname = firstName.Validate(nameof(firstName), 64, "Organisations");

        if (resultFirstname.IsFailure)
        {
            return Result.Failure<Person>(resultFirstname.Error);
        }


        Result resultLastname = lastName.ValidateIfHasValue(nameof(lastName), 64, "Organisations");

        if (resultLastname.IsFailure)
        {
            return Result.Failure<Person>(resultLastname.Error);
        }

        var person = new Person(firstName, lastName);

        return person;
    }
}
