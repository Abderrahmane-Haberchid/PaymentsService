using Domain.Models;

namespace Application.Features.GetPayments;

public static class GetPaymentsMapper
{
    public static IReadOnlyCollection<GetPaymentsResponse> ToPaymentsResponses(this  IReadOnlyCollection<Payment> payments)
    {
        return payments.Select(p => new GetPaymentsResponse(
                p.PaymentId,
                p.InvoiceId,
                p.Amount,
                p.PaymentStatus,
                p.PaymentMethod,
                p.PaidAt))
            .ToArray();
    }
}