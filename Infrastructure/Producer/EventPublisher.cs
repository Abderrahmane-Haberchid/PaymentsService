using Application.Abstractions;
using MassTransit;

namespace Infrastructure.Producer;

public class EventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public async Task PublishPaymentEvent<T>(T paymentEvent, CancellationToken cancellationToken = default)
    {
        await publishEndpoint.Publish(paymentEvent, cancellationToken);
        
    }
}