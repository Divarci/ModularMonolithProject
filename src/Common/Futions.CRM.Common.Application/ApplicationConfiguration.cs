using System.Reflection;
using FluentValidation;
using Futions.CRM.Common.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Futions.CRM.Common.Application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services, Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(moduleAssemblies, includeInternalTypes: true);

        return services;
    }
}
