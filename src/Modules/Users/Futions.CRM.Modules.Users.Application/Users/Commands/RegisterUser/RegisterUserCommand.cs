using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Users.Application.Users.Commands.RegisterUser;
public record RegisterUserCommand(
    string Email,
    string Firstname,
    string Lastname,
    string Password) : ICommand<Guid>;
