using Domain.Models;
using Domain.Respository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistance;

public class PaymentRepository(
    AppDbContext dbContext,
    ILogger<PaymentRepository> logger) : IPaymentRepository
{
    public async Task<Payment> SavePaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        var savedPayment = await dbContext.Payment.AddAsync(payment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return savedPayment.Entity;
    }

    public async Task<Payment?> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken)
    {
        var  payment = await dbContext.Payment.FirstOrDefaultAsync(p => p.PaymentId == paymentId, cancellationToken);
        return  payment;
    }

    public async Task<IReadOnlyCollection<Payment>> GetPaymentsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
    {
        return await dbContext.Payment
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public Task<Payment> GetPaymentByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}