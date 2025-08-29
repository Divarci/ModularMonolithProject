using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.Products.Queries.GetAllProductByProductBookId;
public record GetAllProductsByProductBookIdQuery(
    Guid ProductBookId) : IQuery<ProductDto[]>;
