
using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.DeleteProductBook;
public record DeleteProductBookCommand(
    Guid ProductBookId) : ICommand;
