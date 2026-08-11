using Domain.Models;

namespace Domain.Respository;

public interface IPaymentRepository
{
    Task<Payment> SavePaymentAsync(Payment payment, CancellationToken cancellationToken);
    Task<Payment?> GetPaymentAsync(Guid paymentId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Payment>> GetPaymentsAsync(int pageSize, int pageNumber, CancellationToken cancellationToken);
    Task<Payment> GetPaymentByInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}