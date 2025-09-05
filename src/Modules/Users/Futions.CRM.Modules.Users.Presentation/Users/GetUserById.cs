using System.Security.Claims;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Infrastructure.Authentication;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Common.Presentation.Results;
using Futions.CRM.Modules.Users.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Modules.Users.Presentation.Users;
internal sealed class GetUserById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/profile",
            async (ClaimsPrincipal claims, ISender sender, CancellationToken cancellationToken = default) =>
            {
                Result<UserDto> result = await sender.Send(new GetUserByIdQuery(claims.GetUserId()), cancellationToken);

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization()
            .WithTags(Tags.Users);
    }
}
