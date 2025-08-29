using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.DealProducts.Commands.DeleteDealProduct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Deals.Presentation.DealProducts;
internal sealed class DeleteDealProduct : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/deals/{id:guid}/products/{dealProductId:guid}",
            async (Guid id, Guid dealProductId, ISender sender, CancellationToken cancellationToken = default) =>
        {
            Result result = await sender.Send(
                new DeleteDealProductCommand(id, dealProductId), cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);

        }).WithTags(Tags.DealProducts);
    }
}
