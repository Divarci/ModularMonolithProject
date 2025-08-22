using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations.Errors;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations;
public sealed partial class Organisation
{
    public static Result<Organisation> Create(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure<Organisation>(OrganisationErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            return Result.Failure<Organisation>(OrganisationErrors.MaxLength(nameof(title), 64));
        }

        var organisation = new Organisation(title);

        return Result.Success(organisation);
    }

    public Result UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return Result.Failure(OrganisationErrors.NullValue(nameof(title)));
        }

        if (title.Length > 64)
        {
            return Result.Failure(OrganisationErrors.MaxLength(nameof(title), 64));
        }

        Title = title;

        return Result.Success();
    }
}
