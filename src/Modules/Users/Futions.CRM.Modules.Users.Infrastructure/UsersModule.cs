using Futions.CRM.Common.Domain.Abstractions.Authorisations;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Infrastructure.Authorisation;
using Futions.CRM.Modules.Users.Infrastructure.Identity;
using Futions.CRM.Modules.Users.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Users.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Futions.CRM.Modules.Users.Infrastructure;
public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services, 
        string connectionString,
        IConfiguration config)
    {
        AddInfrastructure(services, connectionString);

        AddAuthentication(services, config);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IPermissionService, PermissionService>();

        services.Configure<KeyCloakOptions>(config.GetSection("Users:KeyCloak"));

        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        services
            .AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
            {
                KeyCloakOptions keycloakOptions = serviceProvider
                    .GetRequiredService<IOptions<KeyCloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        services.AddTransient<IIdentityProviderService, IdentityProviderService>();
    }
}
