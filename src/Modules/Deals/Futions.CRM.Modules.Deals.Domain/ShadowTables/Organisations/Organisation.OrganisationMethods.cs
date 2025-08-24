using Futions.CRM.Common.Domain.Extensions;
using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Modules.Deals.Domain.ShadowTables.Organisations;
public sealed partial class Organisation
{
    public static Result<Organisation> Create(string title)
    {
        Result result = title.Validate(nameof(title), 64, "Organiation");

        if(result.IsFailure)
        {
            return Result.Failure<Organisation>(result.Error);
        }

        var organisation = new Organisation(title);

        return Result.Success(organisation);
    }

    public Result UpdateTitle(string title)
    {
        Result result = title.Validate(nameof(title), 64, "Organiation");

        if (result.IsFailure)
        {
            return Result.Failure<Organisation>(result.Error);
        }

        Title = title;

        return Result.Success();
    }
}
