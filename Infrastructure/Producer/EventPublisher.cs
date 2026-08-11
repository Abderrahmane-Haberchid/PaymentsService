using Application.Abstractions;
using Domain.Respository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Producer;

public class EventPublisher(
    IPublishEndpoint publishEndpoint,
    IPaymentRepository paymentRepository,
    ILogger<EventPublisher> logger) : IEventPublisher
{
    public async Task PublishPaymentEvent<T>(T paymentEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("***********************************************************************");
        logger.LogInformation($"Publishing payment event: {paymentEvent.GetType().Name}");
        logger.LogInformation("***********************************************************************");
        
        await publishEndpoint.Publish(paymentEvent, cancellationToken);
        
    }
}