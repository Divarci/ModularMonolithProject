using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Queries.GetAllDealProducts;
public record DealProductDto
{
    [Required, JsonProperty("id")]
    public required Guid Id { get; init; }

    [Required, JsonProperty("title")]
    public required string Title { get; init; }

    [Required, JsonProperty("quantity")]
    public required int Quantity { get; init; }

    [Required, JsonProperty("description")]
    public required string Description { get; init; }

    [Required, JsonProperty("price")]
    public required decimal Price { get; init; }

    [Required, JsonProperty("discount")]
    public required decimal Discount { get; init; }
}
