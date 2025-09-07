using Futions.CRM.Common.Domain.Abstractions.Entities.Extensions;
using Futions.CRM.Common.Domain.Exceptions;

namespace Futions.CRM.Common.Domain.Entities.Messages;
public abstract class Message: IRootAggregate
{
    protected Message() { }
    protected Message(Guid id, string type, 
        string content, DateTime occurredOnUtc)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new CrmException(nameof(Message),
                Results.Error.Validation(
                    "OutbboxMessageCreate.NullObject",
                    $"{nameof(type)} is null"));
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new CrmException(nameof(Message),
                Results.Error.Validation(
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
