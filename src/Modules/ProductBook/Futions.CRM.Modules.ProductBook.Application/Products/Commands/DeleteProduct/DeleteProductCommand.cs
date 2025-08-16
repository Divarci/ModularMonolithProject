using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.DeleteProduct;
public record DeleteProductCommand(
    Guid ProductBookId,
    Guid ProductId) : ICommand;
