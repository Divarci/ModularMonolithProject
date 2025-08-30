using Futions.CRM.Common.Domain.IGenericRepositories;
using Futions.CRM.Common.Infrastructure.GenericRepositories;
using Futions.CRM.Common.Infrastructure.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        AddRepositories(services);

        AddInterceptors(services);  

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
    }

    private static void AddInterceptors(IServiceCollection services)
    {
        services.AddSingleton<PublishDomainEventsInterceptor>();
    }
}
