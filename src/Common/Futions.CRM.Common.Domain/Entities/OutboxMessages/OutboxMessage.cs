using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Exceptions;

namespace Futions.CRM.Common.Domain.Entities.OutboxMessages;
public abstract class OutboxMessage: IRootAggregate
{
    protected OutboxMessage() { }
    protected OutboxMessage(Guid id, string type, 
        string content, DateTime occurredOnUtc)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new CrmException(nameof(OutboxMessage),
                Results.Error.Problem(
                    "OutbboxMessageCreate.NullObject",
                    $"{nameof(type)} is null"));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new CrmException(nameof(OutboxMessage),
                Results.Error.Problem(
                    "OutbboxMessageCreate.NullObject",
                    $"{nameof(type)} is null"));
        }

        Id = id;
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
    }

    public Guid Id { get; private set; }

    public string Type { get; private set; }

    public string Content { get; private set; }

    public DateTime OccurredOnUtc { get; private set; }

    public DateTime? ProcessedOnUtc { get; private set; }

    public string? Error { get; private set; }

    public virtual void Update(string? exception)
    {
        ProcessedOnUtc = DateTime.UtcNow;
        Error = exception;
    }
}
