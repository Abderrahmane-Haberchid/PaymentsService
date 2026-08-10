namespace Domain.DomainExceptions;

public class InvalidPaymentDataException(string message) : DomainException(message)
{
    
}