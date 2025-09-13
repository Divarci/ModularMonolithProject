using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Entities.MessageConsumers;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Infrastructure.Inbox;
using Futions.CRM.Common.Infrastructure.Outbox;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.InboxMessages;
using Futions.CRM.Modules.Catalogue.Domain.OutboxMessages;
using Futions.CRM.Modules.Catalogue.Infrastructure.Inbox;
using Futions.CRM.Modules.Catalogue.Infrastructure.Outbox;
using Futions.CRM.Modules.Catalogue.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Catalogue.Infrastructure.UnitOfWorks;
using Futions.CRM.Modules.Catalogue.IntegrationEvents.ProductBook;
using Futions.CRM.Modules.Catalogue.Presentation.Products.ProductDeleteSagaPattern;
using Futions.CRM.Modules.Deals.IntegrationEvents;
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

        AddInbox(services, config);

        AddInfrastructure(services, connectionString);

        AddDomainEventHandlers(services);

        AddIntegrationEventHandlers(services);

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
        registrationConfigurator.AddConsumer<
            IntegrationEventConsumer<
                ProductRemoveCompletedIntegrationEvent, ICatalogueUnitOfWork, CatalogueInboxMessage>>();

        registrationConfigurator
            .AddSagaStateMachine<ProductDeleteSaga, ProductDeleteState>()
            .InMemoryRepository();
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

    public static void AddInbox(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IMessageFactory<CatalogueInboxMessage>, CatalogueInboxMessage>();

        services.Configure<CatalogueInboxOptions>(config.GetSection("Catalogue:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob<ProcessInboxJob, CatalogueInboxOptions>>();
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

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IMessageConsumerFactory<CatalogueInboxMessageConsumer>, CatalogueInboxMessageConsumer>();

        Type[] integrationEventHandlers = [.. Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))];

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type domainEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentIntegrationEventHandler<,,>)
                .MakeGenericType(domainEvent, typeof(ICatalogueUnitOfWork), typeof(CatalogueInboxMessageConsumer));

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}
