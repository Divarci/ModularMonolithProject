using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.DealProducts.Commands.CreateDealProduct;
using Futions.CRM.Modules.Deals.Domain.Deals;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Deals.Presentation.DealProducts;
internal sealed class CreateDealProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/deals/{dealId:guid}/products",
            async (Guid dealId, CreateDealProductDto request, ISender sender, CancellationToken cancellationToken) =>
        {
            Result<DealProduct> result = await sender.Send(
                new CreateDealProductCommand(dealId, request.ProductId, request.Quantity,
                request.Description, request.Price, request.Discount), cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        }).WithTags(Tags.DealProducts);
    }
}

internal sealed record CreateDealProductDto
{
    [JsonProperty("productId")]
    public required Guid ProductId { get; init; }

    [JsonProperty("quantity")]
    public required int Quantity { get; init; }

    [JsonProperty("description")]
    public required string Description { get; init; }

    [JsonProperty("price")]
    public required decimal Price { get; init; }

    [JsonProperty("discount")]
    public required decimal Discount { get; init; }
}
