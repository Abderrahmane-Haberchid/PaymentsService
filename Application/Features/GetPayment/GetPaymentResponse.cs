using Domain.Enums;

namespace Application.Features.GetPayment;

public record GetPaymentResponse(
    Guid PaymentId, 
    Guid InvoiceId,
    decimal Total, 
    PaymentStatus PaymentStatus,
    PaymentMethod PaymentMethod,
    DateTime PaymentDate);