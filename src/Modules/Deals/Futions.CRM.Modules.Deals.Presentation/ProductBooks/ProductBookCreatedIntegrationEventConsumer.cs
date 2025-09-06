using Futions.CRM.Common.Domain.Exceptions;
using Futions.CRM.Common.Domain.Results;
using Futions.CRM.Modules.Catalogue.IntegrationEvents;
using Futions.CRM.Modules.Deals.Application.ProductBooks.Commands.CreateProductBook;
using MassTransit;
using MediatR;

namespace Futions.CRM.Modules.Deals.Presentation.ProductBooks;
public sealed class ProductBookCreatedIntegrationEventConsumer(
    ISender sender) : IConsumer<ProductBookCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductBookCreatedIntegrationEvent> context)
    {
        Result<Guid> result = await sender.Send(
            new CreateProductBookCommand(
                context.Message.ProductBookId,
                context.Message.Title),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new CrmException(nameof(CreateProductBookCommand), result.Error);
        }
    }
}
