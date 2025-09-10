using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.Products.Commands.UpdateProduct;
internal sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(512);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);
    }
}
