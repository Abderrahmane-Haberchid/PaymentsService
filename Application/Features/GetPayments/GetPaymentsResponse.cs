namespace Application.Features.GetPayments;
using Domain.Enums;

public record GetPaymentsResponse(
    Guid PaymentId, 
    Guid InvoiceId,
    decimal Total, 
    PaymentStatus PaymentStatus,
    PaymentMethod PaymentMethod,
    DateTime PaymentDate);