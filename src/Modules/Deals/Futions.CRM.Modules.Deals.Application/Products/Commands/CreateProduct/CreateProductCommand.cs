using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.CreateProduct;
public record CreateProductCommand(
    Guid ProductBookId,
    string Title,
    string Description,
    decimal Price) : ICommand<Guid>;
