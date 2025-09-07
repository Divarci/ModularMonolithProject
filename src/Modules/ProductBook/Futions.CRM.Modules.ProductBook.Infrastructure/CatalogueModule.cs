using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Entities.MessageConsumers;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Infrastructure.Outbox;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
using Futions.CRM.Modules.Catalogue.Infrastructure.Outbox;
using Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Futions.CRM.Modules.Catalogue.Infrastructure;
public static class CatalogueModule
{
    public static IServiceCollection AddCatalogueModule(
        this IServiceCollection services, string connectionString, IConfiguration config)
    {
        AddOutbox(services, config);

        AddInfrastructure(services, connectionString);

        AddDomainEventHandlers(services);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    public static void AddInfrastructure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<ICatalogueUnitOfWork, CatalogueUnitOfWork>();

        services.AddDbContext<CatalogueDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor<CatalogueOutboxMessage>>());
        });

    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        //registrationConfigurator.AddConsumer<Int-Evnt-Consumer>;
    }

    public static void AddOutbox(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IMessageFactory<CatalogueOutboxMessage>, CatalogueOutboxMessage>();

        services.AddScoped(provider =>
            new InsertOutboxMessagesInterceptor<CatalogueOutboxMessage>(
                OutboxActionsFactory<CatalogueOutboxMessage>.Create<ICatalogueUnitOfWork>(provider),
                provider.GetRequiredService<IMessageFactory<CatalogueOutboxMessage>>()
        ));

        services.Configure<CatalogueOutboxOptions>(config.GetSection("Catalogue:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob<ProcessOutboxJob, CatalogueOutboxOptions>>();
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IMessageConsumerFactory<CatalogueOutboxMessageConsumer>, CatalogueOutboxMessageConsumer>();

        Type[] domainEventHandlers = [.. Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))];

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<,,>)
                .MakeGenericType(domainEvent, typeof(ICatalogueUnitOfWork), typeof(CatalogueOutboxMessageConsumer));

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }
}
