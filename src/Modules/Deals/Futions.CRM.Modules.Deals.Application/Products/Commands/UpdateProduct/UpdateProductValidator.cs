using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.UpdateProduct;
internal sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(64)
            .When(x => x.Title is not null);

        RuleFor(x => x.Description)
            .MaximumLength(512)
            .When(x => x.Description is not null);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .When(x => x.Price is not null);
    }
}
