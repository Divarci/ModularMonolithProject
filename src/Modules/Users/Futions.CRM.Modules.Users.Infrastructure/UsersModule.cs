using Futions.CRM.Common.Application.Messaging;
using Futions.CRM.Common.Domain.Abstractions.Authorisations;
using Futions.CRM.Common.Domain.Entities.MessageConsumers;
using Futions.CRM.Common.Domain.Entities.Messages;
using Futions.CRM.Common.Infrastructure.Outbox;
using Futions.CRM.Common.Presentation.Endpoints;
using Futions.CRM.Modules.Users.Domain.Abstractions;
using Futions.CRM.Modules.Users.Domain.OutboxMessages;
using Futions.CRM.Modules.Users.Infrastructure.Authorisation;
using Futions.CRM.Modules.Users.Infrastructure.Identity;
using Futions.CRM.Modules.Users.Infrastructure.Outbox;
using Futions.CRM.Modules.Users.Infrastructure.Persistance.Database;
using Futions.CRM.Modules.Users.Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Futions.CRM.Modules.Users.Infrastructure;
public static class UsersModule
{
    public static IServiceCollection AddUsersModule(
        this IServiceCollection services,
        string connectionString,
        IConfiguration config)
    {
        AddInfrastructure(services, connectionString);

        AddAuthentication(services, config);

        AddOutbox(services, config);

        AddDomainEventHandlers(services);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUsersUnitOfWork, UsersUnitOfWork>();

        services.AddDbContext<UsersDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor<UsersOutboxMessage>>());
        });
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IPermissionService, PermissionService>();

        services.Configure<KeyCloakOptions>(config.GetSection("Users:KeyCloak"));

        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        services
            .AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
            {
                KeyCloakOptions keycloakOptions = serviceProvider
                    .GetRequiredService<IOptions<KeyCloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
            .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        services.AddTransient<IIdentityProviderService, IdentityProviderService>();
    }

    public static void AddOutbox(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IMessageFactory<UsersOutboxMessage>, UsersOutboxMessage>();

        services.AddScoped(provider =>
            new InsertOutboxMessagesInterceptor<UsersOutboxMessage>(
                OutboxActionsFactory<UsersOutboxMessage>.Create<IUsersUnitOfWork>(provider),
                provider.GetRequiredService<IMessageFactory<UsersOutboxMessage>>()
        ));

        services.Configure<UsersOutboxOptions>(config.GetSection("Users:Outbox"));

        services.ConfigureOptions<ConfigureProcessOutboxJob<ProcessOutboxJob, UsersOutboxOptions>>();
    }
    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IMessageConsumerFactory<UsersOutboxMessageConsumer>, UsersOutboxMessageConsumer>();

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
                .MakeGenericType(domainEvent,typeof(IUsersUnitOfWork),typeof(UsersOutboxMessageConsumer));

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }
}
