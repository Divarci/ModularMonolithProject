using Futions.CRM.Modules.Catalogue.IntegrationEvents.Product;
using Futions.CRM.Modules.Deals.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Futions.CRM.Modules.Catalogue.Presentation.Products.ProductDeleteSagaPattern;
public sealed class ProductDeleteSaga : MassTransitStateMachine<ProductDeleteState>
{
    public State ProductDeleteStarted { get; private set; }
    public State ProductCanBeDeleted { get; private set; }
    public State ProductCanNotBeDeleted { get; private set; }


    public Event<CheckProductIfCanbeRemovedIntegrationEvent> ProductDeleteCheckStarted { get; private set; }
    public Event<ProductRemoveCompletedIntegrationEvent> ProductDeleted { get; private set; }
    public Event<ProductRemoveFailedIntegrationEvent> ProductStillExist { get; private set; }


    public ProductDeleteSaga()
    {
        Event(() => ProductDeleteCheckStarted, c => c.CorrelateById(m => m.Message.ProductId));
        Event(() => ProductDeleted, c => c.CorrelateById(m => m.Message.ProductId));
        Event(() => ProductStillExist, c => c.CorrelateById(m => m.Message.ProductId));

        InstanceState(s => s.CurrentState);

        Initially(
            When(ProductDeleteCheckStarted)
                .Then(context =>
                {
                    ILogger<ProductDeleteSaga> logger = context
                        .GetPayload<IServiceProvider>()
                        .GetRequiredService<ILogger<ProductDeleteSaga>>();

                    logger.LogInformation(
                        "Product delete started for product id : {ProductId}",
                        context.Message.ProductId);
                })
            .TransitionTo(ProductDeleteStarted));

        During(ProductDeleteStarted,
            When(ProductDeleted)
                .Then(context =>
                {
                    ILogger<ProductDeleteSaga> logger = context
                        .GetPayload<IServiceProvider>()
                        .GetRequiredService<ILogger<ProductDeleteSaga>>();

                    logger.LogInformation(
                        "Product successfully deleted: {ProductId}", 
                        context.Message.ProductId);
                })
                .TransitionTo(ProductCanBeDeleted)
                .Finalize());

        During(ProductDeleteStarted,
            When(ProductStillExist)
                .Then(context =>
                {
                    ILogger<ProductDeleteSaga> logger = context
                        .GetPayload<IServiceProvider>()
                        .GetRequiredService<ILogger<ProductDeleteSaga>>();

                    logger.LogInformation(
                        "Product delete failed: {ProductId}",
                        context.Message.ProductId);
                })
                .TransitionTo(ProductCanNotBeDeleted)
                .Finalize());
    }
}
