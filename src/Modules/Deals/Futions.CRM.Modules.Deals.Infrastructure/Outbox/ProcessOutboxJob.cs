using Futions.CRM.Common.Domain.DomainEvents;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Infrastructure.Outbox;
using Futions.CRM.Common.Infrastructure.Serialization;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.OutboxMessages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace Futions.CRM.Modules.Deals.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxJob(
    IDealsUnitOfWork unitOfWork,
    IServiceScopeFactory serviceScopeFactory,
    IOptions<DealsOutboxOptions> outboxOptions,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly DealsOutboxOptions _outboxOptions = outboxOptions.Value;
    private readonly ILogger<ProcessOutboxJob> _logger = logger;

    private const string ModuleName = "Deals";

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{Module} - Beginning to process outbox messages", ModuleName);

        await _unitOfWork.BeginTransactionAsync();

        IReadOnlyList<OutboxMessageResponse> outboxMessages = await OutboxActionsFactory<DealsOutboxMessage>
            .GetMessages<IDealsUnitOfWork>(_serviceScopeFactory, _outboxOptions);

        foreach (OutboxMessageResponse outboxMessage in outboxMessages)
        {
            Exception? exception = null;

            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content, SerializerSettings.Instance);

                if (domainEvent is null)
                {
                    throw new CrmException(nameof(ProcessOutboxJob),
                        Error.Problem("DomainEvent.NullError", "Domain event deserialize object error."));
                }

                using IServiceScope scope = _serviceScopeFactory.CreateScope();

                IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                await publisher.Publish(domainEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Module} - Exception while processing outbox message {MessageId}", ModuleName, outboxMessage.Id);

                exception = ex;
            }

            await OutboxActionsFactory<DealsOutboxMessage>.Update<IDealsUnitOfWork>(
                _serviceScopeFactory, outboxMessage, exception);

        }

        await _unitOfWork.CommitTransactionAsync();

        _logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);


    }
}

