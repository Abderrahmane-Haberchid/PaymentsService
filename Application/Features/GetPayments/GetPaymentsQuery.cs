using MediatR;

namespace Application.Features.GetPayments;

public sealed record GetPaymentsQuery(int PageSize, int PageNumber) : IRequest<IReadOnlyCollection<GetPaymentsResponse>>;