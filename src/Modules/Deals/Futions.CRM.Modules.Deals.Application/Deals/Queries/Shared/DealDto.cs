using System.ComponentModel.DataAnnotations;
using Futions.CRM.Modules.Deals.Domain.Deals;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Deals.Application.Deals.Queries.Shared;
public record DealDto
{
    [Required, JsonProperty("id")]
    public required Guid Id { get; init; }

    [Required, JsonProperty("title")]
    public required string Title { get; init; }

    [Required, JsonProperty("dealStatus")]
    public required DealStatus DealStatus { get; init; }
}
