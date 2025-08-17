using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.Products.Commands.DeleteProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Catalogue.Presentation.Products;
internal sealed class DeleteProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("productbooks/{id:guid}/products/{productId:guid}",
            async (Guid id, Guid productId, ISender sender, CancellationToken cancellationToken = default) =>
        {
            Result result = await sender
                .Send(new DeleteProductCommand(id, productId), cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        });
    }
}
