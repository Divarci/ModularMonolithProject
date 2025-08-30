using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.CreateDealProduct;
public record CreateDealProductCommand(
    Guid DealId,
    Guid ProductId,
    int Quantity,
    string Description,
    decimal Price,
    decimal Discount) : ICommand<Guid>;
