using FluentValidation;

namespace Application.Features.GetPayment;

public class GetPaymentQueryValidator : AbstractValidator<GetPaymentQuery>
{
    public GetPaymentQueryValidator()
    {
        RuleFor(x => x.PaymentId)
            .NotEmpty();
    }
}