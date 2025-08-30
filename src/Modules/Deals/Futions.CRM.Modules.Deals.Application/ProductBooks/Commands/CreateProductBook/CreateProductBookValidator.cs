using FluentValidation;

namespace Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.CreateProductBook;
internal sealed class CreateProductBookValidator : AbstractValidator<CreateProductBookCommand>
{
    public CreateProductBookValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(64);
    }
}
