using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Modules.Catalogue.Infrastructure;
public static class CatalogueModule
{
    public static IServiceCollection AddCatalogueModule(
        this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<CatalogueDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;


    }
}
