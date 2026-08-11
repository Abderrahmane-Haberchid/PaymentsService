

namespace Application.Abstractions;

public interface IEventPublisher
{
    Task PublishPaymentEvent<T>(T paymentEvent, CancellationToken cancellationToken);
}