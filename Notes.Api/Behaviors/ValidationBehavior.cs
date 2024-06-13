using System.ComponentModel.DataAnnotations;
using MediatR;
using FluentValidation;
using FluentValidation.Results;
using ValidationException = FluentValidation.ValidationException;

namespace Notes.Api.Behaviors;



public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }
    
    public ValidationBehavior() {}
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            var response = await next();
            return response;   
        }
        Console.WriteLine("ValidationBehaviorHandle");
        ValidationContext<TRequest> context = new (request);
        var validationResult = await _validator.ValidateAsync(context);
        var errors = validationResult.Errors;
        // var validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context)));
        // var errors = validationResults
            // .Where(valudationResult => !valudationResult.IsValid)
            // .SelectMany(validationResult => validationResult.Errors)
            // .ToList();
        // foreach (var validator in _validators)
        // {
            // Console.WriteLine(validator);
        // }
        
        if (errors.Any())
        {
            foreach (var error in errors)
            {
                Console.WriteLine($"error = {error.PropertyName} {error.ErrorMessage}");
            }

            return (Activator.CreateInstance(typeof(TResponse), null, errors, null) as TResponse)!;
            
        }
        else
        {
            var response = await next();
            return response;
        }
    }   
}