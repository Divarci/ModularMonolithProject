using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Modules.Catalogue.Infrastructure;
public static class CatalogueModule
{
    public static IServiceCollection AddCatalogueModule(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

}
