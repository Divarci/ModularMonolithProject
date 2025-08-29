using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.UpdateProduct;
public record UpdateProductCommand(
    Guid ProductBookId,
    Guid ProductId,
    string? Title,
    string? Description,
    decimal? Price) : ICommand;
