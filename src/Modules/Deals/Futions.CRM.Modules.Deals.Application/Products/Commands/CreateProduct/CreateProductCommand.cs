using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Modules.Deals.Domain.ShadowTables.Products;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.CreateProduct;
public record CreateProductCommand(
    Guid ProductBookId,
    string Title,
    string Description,
    decimal Price) : ICommand<Product>;
