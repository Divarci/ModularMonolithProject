using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.DealProducts.Queries.GetAllDealProducts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Deals.Presentation.DealProducts;
internal sealed class GetAllDealProducts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/deals/{id:guid}/products",
            async (Guid id, ISender sender, CancellationToken cancellationToken = default) =>
            {
                Result<DealProductDto[]> result = await sender.Send(
                    new GetAllDealProductQuery(id), cancellationToken);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.DealProducts);
    }
}
