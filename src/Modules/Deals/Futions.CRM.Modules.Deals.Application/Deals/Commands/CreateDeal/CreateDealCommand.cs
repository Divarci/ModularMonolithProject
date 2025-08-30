using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.CreateDeal;
public record CreateDealCommand(
    string Title) : ICommand<Guid>;
