using MediatR;

namespace Application.Features.GetPayment;

public record GetPaymentQuery(Guid PaymentId) : IRequest<GetPaymentResponse>;