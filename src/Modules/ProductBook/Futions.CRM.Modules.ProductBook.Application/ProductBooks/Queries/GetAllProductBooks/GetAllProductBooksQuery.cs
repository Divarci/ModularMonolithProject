using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.Shared.Dto;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Queries.GetAllProductBooks;
public record GetAllProductBooksQuery : IQuery<ProductBookDto[]>;
