using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;

namespace Notes.Api;

public interface IValidatable<TResponse>
{
    public TResponse? Result { get; init; }
    public ICollection<ValidationFailure>? ValidationFailures { get; init; }
    public ICollection<ProcessingError>? ProcessingErrors { get; init; }
    public bool IsValid { get; }
    public bool IsValidationValid { get; }
    public bool IsProcessingValid { get; }
}

public class ValidatableResponse<TResponse> : IValidatable<TResponse>
{
    public TResponse? Result { get; init; }
    public ICollection<ValidationFailure>? ValidationFailures { get; init; }
    public ICollection<ProcessingError>? ProcessingErrors { get; init; }
    public bool IsValid => IsValidationValid && IsProcessingValid;
    public bool IsValidationValid => ValidationFailures is null || !ValidationFailures.Any();
    public bool IsProcessingValid => ProcessingErrors is null || !ProcessingErrors.Any();

    public ValidatableResponse(TResponse? result, ICollection<ValidationFailure>? validationFailures, ICollection<ProcessingError>? processingErrors)
    {
        Result = result;
        ValidationFailures = validationFailures;
        ProcessingErrors = processingErrors;
    }
    public ValidatableResponse() { }
}