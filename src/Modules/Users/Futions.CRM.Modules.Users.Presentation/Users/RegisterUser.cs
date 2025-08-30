using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Users.Application.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Users.Presentation.Users;
internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register",
            async (RegisterUserDto request, ISender sender, CancellationToken cancellationToken = default) =>
            {
                Result<Guid> result = await sender.Send(
                    new RegisterUserCommand(request.Email, request.Fullname), cancellationToken);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .AllowAnonymous()
            .WithTags(Tags.User);
    }
}

internal sealed record RegisterUserDto
{
    [JsonProperty("fullname")]
    public required string Fullname { get; init; }

    [JsonProperty("email")]
    public required string Email { get; init; }

    [JsonProperty("password")]
    public required string Password { get; init; }
}
