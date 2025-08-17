using Microsoft.AspNetCore.Routing;

namespace Futions.CRM.Common.Presentation.Endpoints;
public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
