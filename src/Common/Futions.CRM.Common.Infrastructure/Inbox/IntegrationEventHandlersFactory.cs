﻿using System.Collections.Concurrent;
using System.Reflection;
using Futions.CRM.Common.Application.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure.Inbox;
public static class IntegrationEventHandlersFactory
{
    private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = [];

    public static IEnumerable<IIntegrationEventHandler> GetHandlers(
        Type type,
        IServiceProvider serviceProvider,
        Assembly assembly)
    {
        Type[] integrationEventHandlerTypes = HandlersDictionary.GetOrAdd(
            $"{assembly.GetName().Name}{type.Name}", _ =>
            {
                Type[] integrationEventHandlers = [.. assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler<>).MakeGenericType(type)))];

                return integrationEventHandlers;
            });

        List<IIntegrationEventHandler> handlers = [];

        foreach (Type integrationEventHandlerType in integrationEventHandlerTypes)
        {
            object integrationEventHandler = serviceProvider.GetRequiredService(integrationEventHandlerType);

            handlers.Add((integrationEventHandler as IIntegrationEventHandler)!);
        }

        return handlers;
    }
}
