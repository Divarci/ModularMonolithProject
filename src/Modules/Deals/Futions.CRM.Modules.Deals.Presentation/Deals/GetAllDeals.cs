using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.Deals.Queries.GetAllDeals;
using Futions.CRM.Modules.Deals.Application.Deals.Queries.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Deals.Presentation.Deals;
internal sealed class GetAllDeals : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("deals", async (ISender sender, CancellationToken cancellationToken = default) =>
        {
            Result<DealDto[]> deals = await sender.Send(
                new GetAllDealsQuery(), cancellationToken);

            return deals.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.Deal);
    }
}
