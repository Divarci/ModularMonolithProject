using System.Reflection;
using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Abstractions.IGenericRepositoies;
using Futions.CRM.Common.Infrastructure.Authentication;
using Futions.CRM.Common.Infrastructure.Authorisation;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Futions.CRM.Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers,
        Assembly[] assemblies)
    {
        AddRepositories(services);

        AddEventBus(services, moduleConfigureConsumers);

        AddAuthentication(services);

        AddAuthorization(services);

        AddBackgroundJobs(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
    }

    private static void AddEventBus(
        IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        services.AddSingleton<IEventBus, EventBus.EventBus>();

        services.AddMassTransit(configure =>
        {
            foreach (Action<IRegistrationConfigurator> configureConsumer in moduleConfigureConsumers)
            {
                configureConsumer(configure);
            }

            configure.SetKebabCaseEndpointNameFormatter();

            configure.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddJwtBearer();

        services.ConfigureOptions<JwtBearerConfigureOptions>();

        services.AddHttpContextAccessor();
    }

    private static void AddAuthorization(IServiceCollection services)
    {
        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }

    private static void AddBackgroundJobs(IServiceCollection services)
    {
        services.AddQuartz();

        services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
    }
}
