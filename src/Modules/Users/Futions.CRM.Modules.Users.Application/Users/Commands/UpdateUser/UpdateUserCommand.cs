using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Users.Application.Users.Commands.UpdateUser;
public record UpdateUserCommand(
    Guid UserId, 
    string? Email, 
    string? Firstname,
    string? Lastname) : ICommand;
