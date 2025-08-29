using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.DeleteDeal;
public record DeleteDealCommand(
    Guid DealId) : ICommand;
