using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.Products.Commands.CreateProduct;
using Futions.CRM.Modules.Catalogue.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Catalogue.Presentation.Products;
internal sealed class CreateProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("productbooks/{id:guid}/products", 
            async (Guid id, CreateProductDto request, ISender sender, CancellationToken cancellationToken = default) =>
        {
            var command = new CreateProductCommand(
                id, request.Title, request.Description, request.Price);

            Result<Product> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}

internal sealed record CreateProductDto
{
    [JsonProperty("title")]
    public required string Title { get; init; }

    [JsonProperty("description")]
    public required string Description { get; init; }

    [JsonProperty("price")]
    public required decimal Price { get; init; }
}
