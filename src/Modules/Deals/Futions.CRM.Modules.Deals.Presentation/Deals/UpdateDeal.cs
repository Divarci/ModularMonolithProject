using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.Deals.Commands.UpdateDeal;
using Futions.CRM.Modules.Deals.Domain.Deals;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Deals.Presentation.Deals;
internal sealed class UpdateDeal : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("deals/{id:guid}", 
            async(Guid id, UpdateDealDto request, ISender sender, CancellationToken cancellationToken = default) =>
            {
                var command = new UpdateDealCommand(id, request.Title, request.DealStatus);

                Result result = await sender.Send(command, cancellationToken);

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Deals);
    }
}

internal sealed record UpdateDealDto
{
    [JsonProperty("title")]
    public string? Title { get; init; }

    [JsonProperty("dealStatus")]
    public DealStatus? DealStatus { get; init; }
}
