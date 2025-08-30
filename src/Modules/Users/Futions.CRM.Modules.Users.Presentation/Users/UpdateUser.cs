using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Users.Application.Users.Commands.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Users.Presentation.Users;
internal sealed class UpdateUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("users/{id:guid}/profile",
            async (Guid id, UpdateUserDto request, 
            ISender sender, CancellationToken cancellationToken = default) =>
        {
            Result result = await sender.Send(
                new UpdateUserCommand(id, request.Email, request.Fullname), cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        }).WithTags(Tags.User);
    }
}

internal sealed record UpdateUserDto
{
    [JsonProperty("fullname")]
    public string Fullname { get; init; }

    [JsonProperty("email")]
    public string Email { get; init; }
}
