using Application.Abstractions;
using Domain.Respository;
using InvoicesService.Shared.Contracts.Events;
using MediatR;

namespace Application.Features.Pay;

public class PayHandler(
    IPaymentRepository paymentRepository,
    IEventPublisher eventPublisher) 
    : IRequestHandler<PayQuery, PayResponse>
{
    public async Task<PayResponse> Handle(PayQuery request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetPaymentAsync(request.PaymentId, cancellationToken);

        if (payment == null)
        {
            throw new KeyNotFoundException("Payment not found");
        }
        
        payment.Pay(request.PaymentId, request.PaymentMethod);

        await eventPublisher.PublishPaymentEvent(new PaymentDoneEvent
        {
            PaymentId = payment.PaymentId,
            InvoiceId = payment.InvoiceId,
            PaidAt = payment.PaidAt,
            Total = payment.Amount
        }, cancellationToken);
        
        await paymentRepository.SaveChangesAsync(cancellationToken);

        return new PayResponse(
            payment.PaymentId,
            payment.Amount,
            payment.PaidAt,
            payment.PaymentStatus,
            payment.PaymentMethod
        );
    }
}