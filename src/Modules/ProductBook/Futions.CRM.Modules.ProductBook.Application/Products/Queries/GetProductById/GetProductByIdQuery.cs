using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductBookId;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetProductById;
public sealed record GetProductByIdQuery(
    Guid ProductBookId,
    Guid ProductId) : IQuery<ProductDto>;
