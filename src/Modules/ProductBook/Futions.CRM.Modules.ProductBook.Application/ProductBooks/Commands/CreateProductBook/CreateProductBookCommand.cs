using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Catalogue.Domain.ProductBooks;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.CreateProductBook;
public record CreateProductBookCommand(
    string Title) : ICommand<ProductBook>;

