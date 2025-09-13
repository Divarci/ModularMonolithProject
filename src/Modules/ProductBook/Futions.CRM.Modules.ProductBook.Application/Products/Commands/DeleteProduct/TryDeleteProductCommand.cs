using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.DeleteProduct;
public record TryDeleteProductCommand(
    Guid ProductBookId,
    Guid ProductId) : ICommand;
