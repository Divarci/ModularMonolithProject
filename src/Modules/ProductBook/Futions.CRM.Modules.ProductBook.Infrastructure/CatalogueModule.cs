using Futions.CRM.Common.Infrastructure.Interceptors;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Modules.Catalogue.Infrastructure;
public static class CatalogueModule
{
    public static IServiceCollection AddCatalogueModule(
        this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ICatalogueUnitOfWork, CatalogueUnitOfWork>();

        services.AddDbContext<CatalogueDbContext>((sp,options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>());
        });

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        //registrationConfigurator.AddConsumer<Int-Evnt-Consumer>;
    }
}
