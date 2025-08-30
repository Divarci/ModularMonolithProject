using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.CreateProduct;
public record CreateProductCommand(
    Guid ProductBookId, 
    string Title,
    string Description,
    decimal Price) : ICommand<Guid>;
