using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.CreateProductBook;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Catalogue.Presentation.ProductBooks;
internal sealed class CreateProductBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("productbooks",
            async (CreateProductBookDto request, ISender sender, CancellationToken cancellationToken = default) =>
            {
                Result<Guid> result = await sender
                    .Send(new CreateProductBookCommand(request.Title), cancellationToken);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.ProductBooks);
    }
}

internal sealed record CreateProductBookDto
{
    [JsonProperty("title")]
    public required string Title { get; init; }
}
