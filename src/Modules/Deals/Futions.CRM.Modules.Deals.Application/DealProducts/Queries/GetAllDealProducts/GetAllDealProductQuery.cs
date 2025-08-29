using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Queries.GetAllDealProducts;
public record GetAllDealProductQuery(
    Guid DealId) : IQuery<DealProductDto[]>;
