using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.Deals.Commands.UpdateDeal;
internal sealed class UpdateDealValidator : AbstractValidator<UpdateDealCommand>
{
    public UpdateDealValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(64);
    }
}
