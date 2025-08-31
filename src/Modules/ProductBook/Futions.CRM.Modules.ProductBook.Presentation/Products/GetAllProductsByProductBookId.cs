using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductBookId;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Catalogue.Presentation.Products;
public class GetAllProductsByProductBookId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("productbooks/{id:guid}/products",
            async (Guid id, ISender sender, CancellationToken cancellationToken = default) =>
            {
                Result<ProductDto[]> products = await sender
                    .Send(new GetAllProductsByProductBookIdQuery(id), cancellationToken);

                return products.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Products);
    }
}
