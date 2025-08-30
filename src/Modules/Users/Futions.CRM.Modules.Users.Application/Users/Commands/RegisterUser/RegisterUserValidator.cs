using FluentValidation;

namespace Futions.CRM.Modules.Users.Application.Users.Commands.RegisterUser;
public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x=>x.Fullname)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x=>x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(64);
    }
}
