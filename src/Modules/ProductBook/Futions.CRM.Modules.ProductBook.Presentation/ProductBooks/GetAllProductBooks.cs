using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetAllProductBooks;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Catalogue.Presentation.ProductBooks;
internal sealed class GetAllProductBooks : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("productbooks",
            async (ISender sender, CancellationToken cancellationToken = default) =>
        {
            Result<ProductBookDto[]> productBooks = await sender.Send(
                new GetAllProductBooksQuery(), cancellationToken);

            return productBooks.Match(Results.Ok, ApiResults.Problem);
        });
    }


}
