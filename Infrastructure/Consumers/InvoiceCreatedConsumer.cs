
using Domain.Enums;
using Domain.Models;
using Domain.Respository;
using InvoicesService.Shared.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Consumers;

public class InvoiceCreatedConsumer(
    ILogger<InvoiceCreatedConsumer> logger,
    IPaymentRepository paymentRepository) 
    : IConsumer<InvoiceCreatedEvent>
{

    public async Task Consume(ConsumeContext<InvoiceCreatedEvent> context)
    {
        logger.LogInformation("Processing Invoice Payment: {InvoiceId}", context.Message.InvoiceId);
        logger.LogInformation("Invoice Amount: {Amount}", context.Message.Total);

        var payment = Payment.Create(
            context.Message.CustomerId,
            context.Message.Total,
            PaymentMethod.CREDIT_CARD,
            context.Message.InvoiceId);
        
        await paymentRepository.SavePaymentAsync(payment, default);

    }
}