using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Deals.Domain.Deals;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.CreateDealProduct;
public record CreateDealProductCommand(
    Guid DealId,
    Guid ProductId,
    int Quantity,
    string Description,
    decimal Price,
    decimal Discount) : ICommand<DealProduct>;
