using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.Deals.Commands.CreateDeal;
using Futions.CRM.Modules.Deals.Domain.Deals;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;


namespace Futions.CRM.Modules.Deals.Presentation.Deals;
internal sealed class CreateDeal : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("deals",
            async (CreateDealDto request, ISender sender, CancellationToken cancellationToken = default) =>
            {
                var command = new CreateDealCommand(request.Title);

                Result<Deal> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Deal);
    }
}

internal sealed record CreateDealDto
{
    [JsonProperty("title")]
    public required string Title { get; init; }
}
