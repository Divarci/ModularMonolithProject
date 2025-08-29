using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Deals.Domain.Deals;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.CreateDeal;
public record CreateDealCommand(
    string Title) : ICommand<Deal>;
