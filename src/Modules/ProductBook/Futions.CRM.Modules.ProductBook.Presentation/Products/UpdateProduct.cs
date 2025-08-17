using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.Products.Commands.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Catalogue.Presentation.Products;
internal sealed class UpdateProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("productbooks/{id:guid}/products/{productId:guid}",
            async (Guid id, Guid productId, UpdateProductDto request, 
            ISender sender, CancellationToken cancellationToken = default) =>
        {
            var command = new UpdateProductComand(
                id, productId, request.Title, request.Description, request.Price);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        });
    }
}

internal sealed record UpdateProductDto
{
    [JsonProperty("title")]
    public string? Title { get; init; }

    [JsonProperty("description")]
    public string? Description { get; init; }

    [JsonProperty("price")]
    public decimal? Price { get; init; }
}
