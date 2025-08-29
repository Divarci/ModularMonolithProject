using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.CreateDealProduct;
internal sealed class CreateDealProductValidator : AbstractValidator<CreateDealProductCommand>
{
    public CreateDealProductValidator()
    {
        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(512);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 100);
    }
}
