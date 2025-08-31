using System.Net.Http.Json;

namespace Futions.CRM.Modules.Users.Infrastructure.Identity;
internal sealed class KeyCloakClient(HttpClient httpClient)
{
    internal async Task<string> RegisterUserAsync(
        UserRepresentation user, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(
            "users",
            user,
            cancellationToken);

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            string errorContent = await httpResponseMessage.Content
                .ReadAsStringAsync(cancellationToken);

            throw new HttpRequestException($"{httpResponseMessage.ReasonPhrase}: {errorContent}");
        }

        return ExtractIdentityIdFromLocationHeader(httpResponseMessage);
    }

    private static string ExtractIdentityIdFromLocationHeader(
        HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "users/";

        string? locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header is null");
        }

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName,
            StringComparison.InvariantCultureIgnoreCase);

        string identityId = locationHeader.Substring(userSegmentValueIndex + usersSegmentName.Length);

        return identityId;
    }
}
