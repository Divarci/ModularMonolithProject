using System.Reflection;
using Futions.CRM.Common.Application.Exceptions;
using Futions.CRM.Common.Domain.Abstractions.AutoSeed;
using Futions.CRM.Common.Domain.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Futions.CRM.Common.Infrastructure.Extensions;
public static class ModelBuilderExtensions
{
    public static ModelBuilder AutoSeedData(this ModelBuilder modelBuilder)
    {        
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Select(x => x.ClrType)
            .Where(x => x.GetInterface(nameof(IHaveAutoseedData)) != null)
            .SelectMany(e => e.GetFields())
            .Where(e => e.GetCustomAttribute<AutoSeedDataAttribute>() != null)
            .GroupBy(e => e.DeclaringType)
            .ToList();

        foreach (IGrouping<Type?,FieldInfo> group in entityTypes)
        {
            EntityTypeBuilder entityType = modelBuilder.Entity(group.Key!);

            foreach (FieldInfo field in group)
            {
                object value = field.GetValue(Activator.CreateInstance(field.DeclaringType!));

                entityType.HasData(value ?? throw new CrmException(nameof(ModelBuilderExtensions),
                    Error.Conflict("AutoSeedData.Error", "Failed to get seed data for " + field.Name)));
            }
        }

        return modelBuilder;
    }
}
