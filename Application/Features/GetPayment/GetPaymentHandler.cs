using Application.Exceptions;
using Domain.Respository;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.GetPayment;

public class GetPaymentHandler(
    IPaymentRepository paymentRepository,
    IValidator<GetPaymentQuery> validator,
    ILogger<GetPaymentHandler> logger) 
    : IRequestHandler<GetPaymentQuery, GetPaymentResponse>
{
    public async Task<GetPaymentResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling GetPaymentQuery, Validation...");
        
        var validate = await validator.ValidateAsync(request, cancellationToken);
        if (!validate.IsValid)
            throw new ValidationException("PaymentId should not be empty");
        
        logger.LogInformation("Fetching payment from database...");
        var savedPayment = await paymentRepository.GetPaymentAsync(request.PaymentId, cancellationToken);

        return savedPayment == null 
            ? throw new SavingPaymentToDatabaseException("An error has occured hile saving to database") 
            : savedPayment.ToPaymentResponse();
    }
}