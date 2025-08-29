using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.CreateDeal;
internal sealed class CreateDealValidator : AbstractValidator<CreateDealCommand>
{
    public CreateDealValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(64);
    }
}
