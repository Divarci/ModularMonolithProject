using FluentValidation;
using Futions.CRM.Modules.Users.Application.Users.Commands.RegisterUser;

namespace Futions.CRM.Modules.Users.Application.Users.Commands.UpdateUser;
public class UpdateUserValidator : AbstractValidator<RegisterUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Firstname)
            .MaximumLength(64);

        RuleFor(x => x.Firstname)
            .MaximumLength(64);

        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(64);
    }
}
