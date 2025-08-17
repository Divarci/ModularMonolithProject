using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductBookId;
public record ProductDto
{
    [Required, JsonProperty("id")]
    public required Guid Id { get; init; }

    [Required, JsonProperty("title")]
    public required string Title { get; init; }

    [Required, JsonProperty("description")]
    public required string Description { get; init; }

    [Required, JsonProperty("price")]
    public required decimal Price { get; init; }
}
