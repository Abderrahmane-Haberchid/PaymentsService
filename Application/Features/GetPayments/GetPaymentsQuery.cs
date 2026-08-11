using MediatR;

namespace Application.Features.GetPayments;

public sealed record GetPaymentsQuery(int PageSize = 20, int PageNumber = 1) : IRequest<IReadOnlyCollection<GetPaymentsResponse>>;