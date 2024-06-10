using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Api.Middlewares;

public class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("ValidationExceptionHandlingMiddleware");
        try
        {
            await _next(context);
            Console.WriteLine("[ValidationExceptionHandlingMiddleware] (await next)");
        }
        catch (Exception e)
        {
            // ValidationException exception = (ValidationException)e.InnerExceptions; 
            Console.WriteLine("catch MIDDLEWARE");
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };
            // if (exception.Errors is not null)
            // {
            //     problemDetails.Extensions["errors"] = exception.Errors;
            // }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}