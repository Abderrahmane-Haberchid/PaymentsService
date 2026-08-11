
using Domain.Respository;
using MediatR;

namespace Application.Features.GetPayments;

public class GetPaymentsHandler(
    IPaymentRepository paymentRepository) 
    : IRequestHandler<GetPaymentsQuery, IReadOnlyCollection<GetPaymentsResponse>>
{
    public async Task<IReadOnlyCollection<GetPaymentsResponse>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = await paymentRepository.GetPaymentsAsync(request.PageSize, request.PageNumber, cancellationToken);

        return payments.ToPaymentsResponses();
    }
}