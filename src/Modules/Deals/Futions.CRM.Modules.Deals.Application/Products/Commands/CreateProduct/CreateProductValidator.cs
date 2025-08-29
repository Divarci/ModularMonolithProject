using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.CreateProduct;
internal sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(512);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}
