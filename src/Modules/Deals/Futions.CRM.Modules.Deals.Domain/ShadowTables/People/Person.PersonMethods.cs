using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.People;
public sealed partial class Person
{
    public static Result<Person> Create(string firstName, string? lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            return Result.Failure<Person>(PersonError.NullValue(nameof(firstName)));
        }

        if (firstName.Length > 64)
        {
            return Result.Failure<Person>(PersonError.MaxLength(nameof(firstName), 64));
        }

        if (lastName is not null && lastName.Length > 64)
        {
            return Result.Failure<Person>(PersonError.MaxLength(nameof(lastName), 64));
        }

        var person = new Person(firstName, lastName);

        return person;
    }
}
