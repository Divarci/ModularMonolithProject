using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetProductBookById;
public record GetProductBookByIdQuery(
    Guid ProductBookId) : IQuery<ProductBookDto>;