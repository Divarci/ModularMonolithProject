using MassTransit;

namespace Futions.CRM.Modules.Catalogue.Presentation.Products.ProductDeleteSagaPattern;
public sealed class ProductDeleteState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }

    public int Version { get; set; }

    public string CurrentState { get; set; }
}
