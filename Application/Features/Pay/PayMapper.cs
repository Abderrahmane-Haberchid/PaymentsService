
using Domain.Models;

namespace Application.Features.Pay;

public static class PayMapper
{
    public static PayResponse ToPayResponse(this Payment payment)
    {
        return new PayResponse(
            payment.PaymentId,
            payment.Amount,
            payment.PaidAt,
            payment.PaymentStatus,
            payment.PaymentMethod
            );
    } 
}