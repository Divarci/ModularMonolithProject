﻿using System.Collections.Concurrent;
using System.Reflection;
using Futions.CRM.Common.Application.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure.Outbox;
public static class DomainEventHandlersFactory
{
    private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = [];

    public static IEnumerable<IDomainEventHandler> GetHandlers(
        Type type,
        IServiceProvider serviceProvider,
        Assembly assembly)
    {
        Type[] domainEventHandlerTypes = HandlersDictionary.GetOrAdd(
            $"{assembly.GetName().Name}{type.Name}", _ =>
            {
                Type[] domainEventHandlerTypes = [.. assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler<>).MakeGenericType(type)))];

                return domainEventHandlerTypes;
            });

        List<IDomainEventHandler> handlers = [];

        foreach (Type domainEventHandlerType in domainEventHandlerTypes)
        {
            object domainEventHandler = serviceProvider.GetRequiredService(domainEventHandlerType);

            handlers.Add((domainEventHandler as IDomainEventHandler)!);
        }

        return handlers;
    }
}
