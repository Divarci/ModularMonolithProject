using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Infrastructure.MessageBox;
using Futions.CRM.Common.Infrastructure.Serialization;
using Futions.CRM.Modules.Deals.Domain.Abstractions;
using Futions.CRM.Modules.Deals.Domain.InboxMessages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace Futions.CRM.Modules.Deals.Infrastructure.Inbox;

[DisallowConcurrentExecution]
internal sealed class ProcessInboxJob(
    IDealsUnitOfWork unitOfWork,
    IServiceScopeFactory serviceScopeFactory,
    IOptions<DealsInboxOptions> inboxOptions,
    ILogger<ProcessInboxJob> logger) : IJob
{
    private readonly IDealsUnitOfWork _unitOfWork = unitOfWork;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly DealsInboxOptions _inboxOptions = inboxOptions.Value;
    private readonly ILogger<ProcessInboxJob> _logger = logger;

    private const string ModuleName = "Deals";

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{Module} - Beginning to process inbox messages", ModuleName);

        await _unitOfWork.BeginTransactionAsync();

        IReadOnlyList<MessageResponse> inboxMessages = await ActionsFactory<DealsInboxMessage>
            .GetMessages<IDealsUnitOfWork>(_serviceScopeFactory, _inboxOptions);

        foreach (MessageResponse inboxMessage in inboxMessages)
        {
            Exception? exception = null;

            try
            {
                IIntegrationEvent integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(
                    inboxMessage.Content, SerializerSettings.Instance);

                if (integrationEvent is null)
                {
                    throw new CrmException(nameof(ProcessInboxJob),
                        Error.Problem("IntegrationEvent.NullError", "Integration event deserialize object error."));
                }

                using IServiceScope scope = _serviceScopeFactory.CreateScope();

                IEnumerable<IIntegrationEventHandler> integrationEventHandlers = EventHandlersFactory.GetHandlers<IIntegrationEventHandler, IIntegrationEvent>(
                    scope.ServiceProvider,
                    Presentation.AssemblyReference.Assembly);

                foreach (IIntegrationEventHandler integrationEventHandler in integrationEventHandlers)
                {
                    await integrationEventHandler.Handle(integrationEvent, context.CancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Module} - Exception while processing outbox message {MessageId}", ModuleName, inboxMessage.Id);

                exception = ex;
            }

            await ActionsFactory<DealsInboxMessage>.Update<IDealsUnitOfWork>(
                _serviceScopeFactory, inboxMessage, exception);

        }

        await _unitOfWork.CommitTransactionAsync();

        _logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);
    }
}

