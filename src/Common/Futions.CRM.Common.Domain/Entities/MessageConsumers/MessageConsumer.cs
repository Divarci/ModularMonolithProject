using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;

namespace Futions.CRM.Common.Domain.Entities.MessageConsumers;
public abstract class MessageConsumer : IRootAggregate
{
    protected MessageConsumer() { }

    protected MessageConsumer(Guid id, string name)
    {
        Result result = name.Validate(nameof(name), 500, nameof(MessageConsumer), isRequired: true);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(MessageConsumer), result.Error);
        }

        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }
}
