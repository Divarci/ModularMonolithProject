using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.UpdateProductBook;

public record UpdateProductBookCommand(
    Guid ProductBookId,
    string? Title = null,
    bool? Inactive = null) : ICommand;
