using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Queries.GetAllProductsByProductId;
public record GetAllProductsByProductBookIdCommand(
    Guid ProductBookId) : IQuery<ProductDto[]>;
