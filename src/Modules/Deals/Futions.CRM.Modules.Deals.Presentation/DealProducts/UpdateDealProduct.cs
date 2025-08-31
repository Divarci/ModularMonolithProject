using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.DealProducts.Commands.UpdateDealProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Deals.Presentation.DealProducts;
internal sealed class UpdateDealProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("deals/{id:guid}/products/{dealProductId:guid}",
            async (Guid id, Guid dealProductId, UpdateDealProductDto request,
                ISender sender, CancellationToken cancellationToken = default) =>
            {
                Result result = await sender.Send(
                    new UpdateDealProductCommand(id, dealProductId, request.Description,
                    request.Quantity, request.Price, request.Discount), cancellationToken);

                return result.Match(Results.NoContent, ApiResults.Problem);

            })
            .RequireAuthorization()
            .WithTags(Tags.DealProducts);
    }
}

internal sealed record UpdateDealProductDto
{
    [JsonProperty("quantity")]
    public required int Quantity { get; init; }

    [JsonProperty("description")]
    public required string Description { get; init; }

    [JsonProperty("price")]
    public required decimal Price { get; init; }

    [JsonProperty("discount")]
    public required decimal Discount { get; init; }
}

