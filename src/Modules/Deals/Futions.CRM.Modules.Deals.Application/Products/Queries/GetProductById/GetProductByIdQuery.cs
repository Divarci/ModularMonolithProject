using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Deals.Application.Products.Queries.GetAllProductByProductBookId;

namespace Futions.CRM.Modules.Deals.Application.Products.Queries.GetProductById;
public sealed record GetProductByIdQuery(
    Guid ProductBookId,
    Guid ProductId) : IQuery<ProductDto>;
