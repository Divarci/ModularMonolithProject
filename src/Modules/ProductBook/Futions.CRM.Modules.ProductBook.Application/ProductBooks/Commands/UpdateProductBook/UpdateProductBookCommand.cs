using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Catalogue.Application.ProductBooks.Commands.UpdateProductBook;

public record UpdateProductBookCommand(
    Guid ProductBookId,
    string? Title,
    bool? Inactive) : ICommand;
