using Domain.DomainExceptions;
using Domain.Enums;
using Domain.Models;
using FluentAssertions;

namespace PaymentService.Test.PaymentService.UnitTests.Domain.Tests;

public class PaymentUnitTests
{


    [Fact]
    public async Task Create_ShouldReturnPayment_WhenCreated()
    {
        var invoiceId = Guid.NewGuid();
        
        var payment = Payment.Create(123, 10m, PaymentMethod.CASH, invoiceId);
        
        payment.Should().NotBeNull();
        payment.InvoiceId.Should().Be(invoiceId);
        payment.Amount.Should().Be(10);
        payment.PaymentMethod.Should().Be(PaymentMethod.CASH);
        payment.CustomerId.Should().Be(123);
    }
    [Fact]
    public async Task Create_ShouldThrowInvalidPaymentDataException_WhenAmountIsLessOrEqualZero()
    {   
        //Arrange + Act + Assert
        Assert.Throws<InvalidPaymentDataException>(() => Payment.Create(123, 0m, PaymentMethod.CASH, Guid.NewGuid()));
    }
    
    [Fact]
    public async Task Create_ShouldThrowInvalidPaymentDataException_WhenInvoiceIdIsEmpty()
    {   
        //Arrange + Act + Assert
        Assert.Throws<InvalidPaymentDataException>(() => Payment.Create(123, 10m, PaymentMethod.CASH, Guid.Empty));
    }
    [Fact]
    public async Task Create_ShouldThrowInvalidPaymentDataException_WhenCustomerIdIsLessOrEqualZero()
    {   
        //Arrange + Act + Assert
        Assert.Throws<InvalidPaymentDataException>(() => Payment.Create(-1, 10m, PaymentMethod.CASH, Guid.Empty));
    }
    
    [Fact]
    public async Task Pay_ShouldReturnPayment_WhenDone()
    {
        //Arrange
        var invoiceId = Guid.NewGuid();
        var payment = Payment.Create(123, 10m, PaymentMethod.CASH, invoiceId);
        
        //Act
        var paymentDone = payment.Pay(invoiceId, PaymentMethod.CASH);
        
        // Assert
        paymentDone.PaymentStatus.Should().Be(PaymentStatus.PAID);
        paymentDone.PaymentMethod.Should().Be(PaymentMethod.CASH);
    }

    [Fact]
    public async Task Pay_ShouldThrowInvoiceAlreadyPaidException_WhenPaymentStatusIsPaid()
    {
        //Arrange
        var invoiceId = Guid.NewGuid();
        var payment = Payment.Create(123, 10m, PaymentMethod.CASH, invoiceId);
        payment.Pay(invoiceId, PaymentMethod.CASH);
        
        //Act + Assert
        Assert.Throws<InvoiceAlreadyPaidException>(() => payment.Pay(invoiceId, PaymentMethod.CASH));
        
    }
    
    [Fact]
    public async Task Pay_ShouldThrowInvalidPaymentDataException_WhenInvoiceIdIsEmpty()
    {
        //Arrange
        var invoiceId = Guid.NewGuid();
        var payment = Payment.Create(123, 10m, PaymentMethod.CASH, invoiceId);
        
        //Act + Assert
        Assert.Throws<InvalidPaymentDataException>(() => payment.Pay(Guid.Empty, PaymentMethod.CASH));
        
    }
    
    
}