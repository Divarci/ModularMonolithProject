using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Catalogue.Domain.Products;

namespace Futions.CRM.Modules.Catalogue.Application.Products.Commands.CreateProduct;
public record CreateProductCommand(
    Guid ProductBookId, 
    string Title,
    string Description,
    decimal Price) : ICommand<Product>;
