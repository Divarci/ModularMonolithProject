using Futions.CRM.Common.Domain.IUnitOfWorks;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Modules.Catalogue.Infrastructure;
public static class CatalogueModule
{
    public static IServiceCollection AddCatalogueModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        AddDatabase(services, config);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CatalogueDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("Database"));
        });
    }

}
