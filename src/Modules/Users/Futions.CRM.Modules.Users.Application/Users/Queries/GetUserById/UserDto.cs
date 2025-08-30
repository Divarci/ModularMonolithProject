using Newtonsoft.Json;

namespace Futions.CRM.Modules.Users.Application.Users.Queries.GetUserById;
public record UserDto
{
    [JsonProperty("id")]
    public required Guid Id { get; init; }

    [JsonProperty("email")]
    public required string Email { get; init; }

    [JsonProperty("fullname")]
    public required string Fullname { get; init; }
}
