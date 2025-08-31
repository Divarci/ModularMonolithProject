using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Futions.CRM.Common.Infrastructure.Authentication;
internal sealed class JwtBearerConfigureOptions(
    IConfiguration config) : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "Authentication";

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        config.GetSection(ConfigurationSectionName).Bind(options);
    }
}
