using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.DeleteDealProduct;
public record DeleteDealProductCommand(
    Guid DealId, Guid DealProductId) : ICommand;
