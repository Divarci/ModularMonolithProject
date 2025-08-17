using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.UpdateProductBook;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Catalogue.Presentation.ProductBooks;
internal sealed class UpdateProductBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("productbooks/{id:guid}",
            async (Guid id, UpdateProductBookDto request, ISender sender, CancellationToken cancellationToken = default) =>
        {
            var command = new UpdateProductBookCommand(
                id, request.Title, request.Inactive);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        });
    }
}

internal sealed record UpdateProductBookDto
{
    [JsonProperty("title")]
    public string? Title { get; init; }

    [JsonProperty("inactive")]
    public bool? Inactive { get; init; }
}

