using Domain.Models;

namespace Application.Features.GetPayment;

public static class GetPaymentMapper
{
    public static GetPaymentResponse ToPaymentResponse(this Payment payment)
    {
        return new GetPaymentResponse(
            payment.PaymentId,
            payment.InvoiceId,
            payment.Amount,
            payment.PaymentStatus,
            payment.PaymentMethod,
            payment.PaidAt);
    }
}