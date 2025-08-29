using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.DeleteProduct;
public record DeleteProductCommand(
    Guid ProductBookId,
    Guid ProductId) : ICommand;
