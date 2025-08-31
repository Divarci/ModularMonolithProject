using FluentValidation;

namespace Futions.CRM.Modules.Users.Application.Users.Commands.RegisterUser;
public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x=>x.Firstname)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x => x.Lastname)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(x=>x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(64);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
     }
}
