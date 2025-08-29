using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Deals.Application.Deals.Commands.DeleteDeal;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Deals.Presentation.Deals;
internal sealed class DeleteDeal : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("deals/{id:guid}",
            async (Guid id, ISender sender, CancellationToken cancellationToken = default) =>
        {
            var command = new DeleteDealCommand(id);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        }).WithTags(Tags.Deal);
    }
}
