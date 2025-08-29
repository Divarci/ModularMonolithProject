using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Deals.Domain.Deals;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.UpdateDeal;
public record UpdateDealCommand(
    Guid DealId,
    string? Title,
    DealStatus? DealStatus) : ICommand;
