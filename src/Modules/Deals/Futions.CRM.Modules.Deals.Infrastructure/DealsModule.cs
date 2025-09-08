using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Entities.MessageConsumers;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Infrastructure.MessageBox;
using Futions.CRM.Common.Infrastructure.MessageBox.Inbox;
using Futions.CRM.Common.Infrastructure.MessageBox.Outbox;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Catalogue.IntegrationEvents;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.InboxMessages;
using Futions.CRM.Modules.Deals.Domain.OutboxMessages;
using Futions.CRM.Modules.Deals.Infrastructure.Inbox;
using Futions.CRM.Modules.Deals.Infrastructure.Outbox;
using Futions.CRM.Modules.Deals.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Deals.Infrastructure.UnitOfWorks;
using Futions.CRM.Modules.Deals.Presentation.ProductBooks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Futions.CRM.Modules.Deals.Infrastructure;
public static class DealsModule
{
    public static IServiceCollection AddDealModule(
        this IServiceCollection services, string connectionString, IConfiguration config)
    {
        AddDomainEventHandlers(services);

        AddOutbox(services, config);

        AddInbox(services, config);

        AddIntegrationEventHandlers(services);

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
        registrationConfigurator.AddConsumer<
            IntegrationEventConsumer<
                ProductBookCreatedIntegrationEvent, IDealsUnitOfWork, DealsInboxMessage>>();
    }

    public static void AddOutbox(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IMessageFactory<DealsOutboxMessage>, DealsOutboxMessage>();

        services.AddScoped(provider =>
            new InsertOutboxMessagesInterceptor<DealsOutboxMessage>(
                ActionsFactory<DealsOutboxMessage>.Create<IDealsUnitOfWork>(provider),
                provider.GetRequiredService<IMessageFactory<DealsOutboxMessage>>()
        ));

        services.Configure<DealsOutboxOptions>(config.GetSection("Deals:Outbox"));

        services.ConfigureOptions<ConfigureProcessMessageBoxJob<ProcessOutboxJob, DealsOutboxOptions>>();
    }

    public static void AddInbox(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IMessageFactory<DealsInboxMessage>, DealsInboxMessage>();

        services.Configure<DealsInboxOptions>(config.GetSection("Deals:Inbox"));

        services.ConfigureOptions<ConfigureProcessMessageBoxJob<ProcessInboxJob, DealsInboxOptions>>();
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IMessageConsumerFactory<DealsOutboxMessageConsumer>, DealsOutboxMessageConsumer>();

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
                .MakeGenericType(domainEvent, typeof(IDealsUnitOfWork), typeof(DealsOutboxMessageConsumer));

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IMessageConsumerFactory<DealsInboxMessageConsumer>, DealsInboxMessageConsumer>();

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
                .MakeGenericType(domainEvent, typeof(IDealsUnitOfWork), typeof(DealsInboxMessageConsumer));

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}
