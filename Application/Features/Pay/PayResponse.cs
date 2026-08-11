using Domain.Enums;

namespace Application.Features.Pay;

public record PayResponse(Guid PaymentId, decimal AmountPaid, DateTime PaymentDate, PaymentStatus PaymentStatus, PaymentMethod PaymentMethod);