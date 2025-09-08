using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Infrastructure.MessageBox;

public static class EventHandlersFactory
{
    private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = new();

    public static IEnumerable<THandler> GetHandlers<THandler, TEvent>(
        IServiceProvider serviceProvider,
        Assembly assembly)
    {
        Type handlerInterface = typeof(THandler).IsGenericType
            ? typeof(THandler).GetGenericTypeDefinition().MakeGenericType(typeof(TEvent))
            : typeof(THandler);

        Type[] handlerTypes = HandlersDictionary.GetOrAdd(
            $"{assembly.GetName().Name}_{typeof(TEvent).Name}_{typeof(THandler).Name}", _ =>
            {
                return assembly.GetTypes()
                    .Where(t => t.IsAssignableTo(handlerInterface))
                    .ToArray();
            });

        List<THandler> handlers = new();

        foreach (Type handlerType in handlerTypes)
        {
            object handler = serviceProvider.GetRequiredService(handlerType);
            handlers.Add((THandler)handler);
        }

        return handlers;
    }
}
