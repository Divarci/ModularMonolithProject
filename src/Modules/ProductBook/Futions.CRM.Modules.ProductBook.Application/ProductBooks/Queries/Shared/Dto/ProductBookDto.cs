using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;
public record ProductBookDto
{
    [Required, JsonProperty("id")]
    public required Guid Id { get; init; }

    [Required, JsonProperty("title")]
    public required string Title { get; init; }

    [Required, JsonProperty("inactive")]
    public required bool Inactive { get; init; }
}
