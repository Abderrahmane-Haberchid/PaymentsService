
using Domain.Enums;
using Domain.Models;
using Infrastructure.Persistance;
using InvoicesService.Shared.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Consumers;

public class InvoiceCreatedConsumer(
    ILogger<InvoiceCreatedConsumer> logger, 
    AppDbContext dbContext) 
    : IConsumer<InvoiceCreatedEvent>
{

    public async Task Consume(ConsumeContext<InvoiceCreatedEvent> context)
    {
        logger.LogInformation("Processing Invoice Payment: {InvoiceId}", context.Message.InvoiceId);
        logger.LogInformation("Invoice Amount: {Amount}", context.Message.Total);
        
    }
}