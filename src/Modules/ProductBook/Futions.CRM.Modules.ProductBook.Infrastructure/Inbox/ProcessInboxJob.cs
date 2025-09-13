using Futions.CRM.Common.Application.EventBus;
using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Common.Infrastructure.Inbox;
using Futions.CRM.Common.Infrastructure.Serialization;
using Futions.CRM.Modules.Catalogue.Domain.Abstractions;
using Futions.CRM.Modules.Catalogue.Domain.InboxMessages;
using Futions.CRM.Modules.Catalogue.Infrastructure.Inbox;
using Futions.CRM.Modules.Catalogue.Presentation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace Futions.CRM.Modules.Catalogue.Infrastructure.Inbox;

[DisallowConcurrentExecution]
internal sealed class ProcessInboxJob(
    ICatalogueUnitOfWork unitOfWork,
    IServiceScopeFactory serviceScopeFactory,
    IOptions<CatalogueInboxOptions> inboxOptions,
    ILogger<ProcessInboxJob> logger) : IJob
{
    private readonly ICatalogueUnitOfWork _unitOfWork = unitOfWork;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly CatalogueInboxOptions _inboxOptions = inboxOptions.Value;
    private readonly ILogger<ProcessInboxJob> _logger = logger;

    private const string ModuleName = "Deals";

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("{Module} - Beginning to process inbox messages", ModuleName);

        await _unitOfWork.BeginTransactionAsync();

        IReadOnlyList<InboxMessageResponse> inboxMessages = await InboxActionsFactory<CatalogueInboxMessage>
            .GetMessages<ICatalogueUnitOfWork>(_serviceScopeFactory, _inboxOptions);

        foreach (InboxMessageResponse inboxMessage in inboxMessages)
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

                IEnumerable<IIntegrationEventHandler> integrationEventHandlers = IntegrationEventHandlersFactory.GetHandlers(
                    integrationEvent.GetType(),
                    scope.ServiceProvider,
                    AssemblyReference.Assembly);

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

            await InboxActionsFactory<CatalogueInboxMessage>.Update<ICatalogueUnitOfWork>(
                _serviceScopeFactory, inboxMessage, exception);

        }

        await _unitOfWork.CommitTransactionAsync();

        _logger.LogInformation("{Module} - Completed processing outbox messages", ModuleName);
    }
}

