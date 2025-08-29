using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.DealProducts.Commands.UpdateDealProduct;
internal sealed class UpdateDealProductValidator:AbstractValidator<UpdateDealProductCommand>
{
    public UpdateDealProductValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.Description)
            .MaximumLength(512);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 100);
    }
}
