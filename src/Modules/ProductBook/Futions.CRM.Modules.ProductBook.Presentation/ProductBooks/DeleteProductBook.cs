using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.DeleteProductBook;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Catalogue.Presentation.ProductBooks;
internal sealed class DeleteProductBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("productbooks/{id:guid}", 
            async (Guid id, ISender sender, CancellationToken cancellationToken = default) =>
        {
            Result result = await sender
                .Send(new DeleteProductBookCommand(id), cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
