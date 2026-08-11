using Domain.Enums;
using Domain.Models;
using MediatR;

namespace Application.Features.Pay;

public record PayQuery(Guid PaymentId, PaymentMethod PaymentMethod) : IRequest<PayResponse>;