using Domain.DomainExceptions;
using Domain.Enums;

namespace Domain.Models;

public class Payment
{
    public Guid PaymentId { get; private set; }
    public int CustomerId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethod  PaymentMethod { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; }
    public Guid InvoiceId { get; private set; }
    public DateTime PaidAt { get; private set; }
    public Guid Version { get; private set; }
    
    private Payment() { }

    private Payment(int customerId, decimal amount, PaymentMethod paymentMethod, PaymentStatus paymentStatus, Guid invoiceId)
    {
        PaymentId = Guid.NewGuid();
        CustomerId = customerId;
        Amount = amount;
        PaymentMethod = paymentMethod;
        PaymentStatus = paymentStatus;
        InvoiceId = invoiceId;
        PaidAt = DateTime.UtcNow;
    }

    public static Payment Create(int customerId, decimal amount, PaymentMethod paymentMethod,  Guid invoiceId)
    {
        if (amount <= 0 || invoiceId == Guid.Empty || customerId <= 0 || !Enum.IsDefined(paymentMethod))
            throw new InvalidPaymentDataException("Payment Data should be correct!");
        
        return new Payment(customerId, amount, paymentMethod, PaymentStatus.WAITING, invoiceId);
    }

    public Payment Pay(Guid invoiceId, PaymentMethod paymentMethod)
    {
        if (PaymentStatus == PaymentStatus.PAID)
        {
            throw new InvoiceAlreadyPaidException("This invoice has already been paid!");
        }

        if (InvoiceId == Guid.Empty)
        {
            throw new InvalidPaymentDataException("Please make sur InvoiceId is not empty!");
        }

        PaymentStatus = PaymentStatus.PAID;
        PaymentMethod = paymentMethod;

        return this;
    }
}