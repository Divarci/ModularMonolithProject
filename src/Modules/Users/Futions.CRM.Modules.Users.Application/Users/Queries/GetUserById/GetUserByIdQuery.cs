using Futions.CRM.Common.Application.Messaging;

namespace Futions.CRM.Modules.Users.Application.Users.Queries.GetUserById;
public record GetUserByIdQuery(
    Guid UserId) : IQuery<UserDto>;
