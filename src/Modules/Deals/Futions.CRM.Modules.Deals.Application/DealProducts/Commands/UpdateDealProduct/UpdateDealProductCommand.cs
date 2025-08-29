using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.UpdateDealProduct;
public record UpdateDealProductCommand(
    Guid DealId,
    Guid DealProductId,
    string? Description,
    int? Quantity,
    decimal? Price,
    decimal? Discount) : ICommand;
