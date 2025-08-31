using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.Deals.Queries.GetDealById;
using Futions.CRM.Modules.Deals.Application.Deals.Queries.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Deals.Presentation.Deals;
internal sealed class GetDealById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("deals/{id:guid}", 
            async (Guid id, ISender sender, CancellationToken cancellationToken = default) =>
            {
                Result<DealDto> result = await sender.Send(new GetDealByIdQuery(id), cancellationToken);

                return result.Match(Results.Ok, ApiResults.Problem);

            })
            .RequireAuthorization()
            .WithTags(Tags.Deals);
    }
}
