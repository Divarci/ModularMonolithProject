using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Deals.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Modules.Deals.Infrastructure;
public static class DealsModule
{
    public static IServiceCollection AddDealModule(
        this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDealsUnitOfWork, DealsUnitOfWork>();

        services.AddDbContext<DealsDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;


    }
}
