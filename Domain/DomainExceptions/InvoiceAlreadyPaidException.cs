namespace Domain.DomainExceptions;

public class InvoiceAlreadyPaidException(string message) : DomainException(message)
{
    
}