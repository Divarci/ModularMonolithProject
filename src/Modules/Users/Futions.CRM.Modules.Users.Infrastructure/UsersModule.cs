using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Users.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Modules.Users.Infrastructure;
public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;


    }
}
