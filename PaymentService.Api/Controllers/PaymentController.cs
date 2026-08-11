using Application.Features.GetPayment;
using Application.Features.GetPayments;
using Application.Features.Pay;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Controllers;

[ApiController]
[Route("api/v1/payments")]
public class PaymentController(
    ILogger<PaymentController> logger,
    ISender sender
    ) : ControllerBase
{

    [HttpGet("{paymentId:guid}")]
    public async Task<IActionResult> Get(Guid paymentId)
    {
        if(paymentId == Guid.Empty)
            throw new ArgumentException(nameof(paymentId));
        
        var query = new GetPaymentQuery(paymentId);
        
        var response = await sender.Send(query);
        
        return Ok(response);
    }

    [HttpGet("{pageSize:int}&{pageNumber:int}")]
    public async Task<IActionResult> GetAll(int pageSize, int pageNumber)
    {
        var query = new GetPaymentsQuery(pageSize, pageNumber);
        var result = await sender.Send(query);
        
        return Ok(result);
    }

    [HttpPut("pay/{paymentId:guid}")]
    public async Task<IActionResult> Pay(Guid paymentId)
    {
        var query = new PayQuery(paymentId, PaymentMethod.CREDIT_CARD);
        var result = await sender.Send(query);

        return Ok(result);
    }
    
}