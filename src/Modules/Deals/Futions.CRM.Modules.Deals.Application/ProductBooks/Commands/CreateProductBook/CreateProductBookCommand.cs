using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.CreateProductBook;
public record CreateProductBookCommand(
    Guid ProductBookId,
    string Title) : ICommand<Guid>;

