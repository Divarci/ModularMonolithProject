using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.UpdateProductBook;
internal sealed class UpdateProductBookValidator : AbstractValidator<UpdateProductBookCommand>
{
    public UpdateProductBookValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(64)
            .When(x => x.Title is not null);
    }
}
