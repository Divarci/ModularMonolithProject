using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Deals.Application.Deals.Queries.Shared;

namespace Futions.CRM.Modules.Deals.Application.Deals.Queries.GetAllDeals;
public record GetAllDealsQuery : IQuery<DealDto[]>;
