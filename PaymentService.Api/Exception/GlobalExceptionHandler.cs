using System.Net;
using Domain.DomainExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Exception;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        System.Exception exception, 
        CancellationToken cancellationToken)
    {
        var (message, status) = exception switch
        {
            InvalidPaymentDataException => ("Please verify payment info", HttpStatusCode.BadRequest),
            _ => ("An error has occured...", HttpStatusCode.InternalServerError)

        };

        var probDetails = new ProblemDetails
        {
            Title = message,
            Detail = exception.Message,
            Instance = httpContext.Request.Path,
            Status = (int)status,
        };

        await httpContext.Response.WriteAsJsonAsync(probDetails, cancellationToken);
        
        return true;
    }
}