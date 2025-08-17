using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductBookId;
public record GetAllProductsByProductBookIdQuery(
    Guid ProductBookId) : IQuery<ProductDto[]>;
