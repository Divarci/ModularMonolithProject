using Futions.CRM.Common.Domain.Entities.OutboxMessages;
using Futions.CRM.Common.Infrastructure.Outbox;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.OutboxMessages;
using Futions.CRM.Modules.Deals.Infrastructure.Outbox;
using Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Deals.Infrastructure.UnitOfWorks;
using Futions.CRM.Modules.Deals.Presentation.ProductBooks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Modules.Deals.Infrastructure;
public static class DealsModule
{
    public static IServiceCollection AddDealModule(
        this IServiceCollection services, string connectionString, IConfiguration config)
    {
        AddOutbox(services, config);

        AddInfrastructure(services, connectionString);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void AddInfrastructure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDealsUnitOfWork, DealsUnitOfWork>();

        services.AddDbContext<DealsDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor<DealsOutboxMessage>>());
        });
    }
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<ProductBookCreatedIntegrationEventConsumer>();
    }

    public static void AddOutbox(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IOutboxMessageFactory<DealsOutboxMessage>, DealsOutboxMessage>();

        services.AddScoped(provider =>
            new InsertOutboxMessagesInterceptor<DealsOutboxMessage>(
                OutboxActionsFactory<DealsOutboxMessage>.Create<IDealsUnitOfWork>(provider),
                provider.GetRequiredService<IOutboxMessageFactory<DealsOutboxMessage>>()
        ));

        services.Configure<DealsOutboxOptions>(config.GetSection("Deals:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob<ProcessOutboxJob, DealsOutboxOptions>>();
    }
}
