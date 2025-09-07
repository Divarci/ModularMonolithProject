using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.Entities.OutboxMessageConsumers;
public abstract class OutboxMessageConsumer : IRootAggregate
{
    protected OutboxMessageConsumer() { }

    protected OutboxMessageConsumer(Guid outboxMessageId, string name)
    {
        Result result = name.Validate(nameof(name), 500, nameof(OutboxMessageConsumer), isRequired: true);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(OutboxMessageConsumer), result.Error);
        }

        Id = outboxMessageId;
        Name = name;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }
}
