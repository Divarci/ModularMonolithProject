using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.IGenericRepositories;
using Futions.CRM.Common.Infrastructure.Authentication;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using Futions.CRM.Common.Infrastructure.Interceptors;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<IRegistrationConfigurator>[] moduleConfigureConsumers)
    {
        AddRepositories(services);

        AddEventBus(services, moduleConfigureConsumers);

        AddInterceptors(services);

        AddAuthentication(services);

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

    private static void AddInterceptors(IServiceCollection services)
    {
        services.AddSingleton<PublishDomainEventsInterceptor>();
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddJwtBearer();

        services.ConfigureOptions<JwtBearerConfigureOptions>();

        services.AddHttpContextAccessor();
    }
}
