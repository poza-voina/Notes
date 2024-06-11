using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Results;

namespace Notes.Api;

public interface IValidatable<TResponse>
{
    public TResponse? Result { get; init; }
    public ICollection<ValidationFailure>? Errors { get; init; }
    public bool IsValid { get; }
}

public class ValidatableResponse<TResponse> : IValidatable<TResponse>
{
    public TResponse? Result { get; init; }
    public ICollection<ValidationFailure>? Errors { get; init; }
    public bool IsValid => Errors is null || !Errors.Any();

    public ValidatableResponse(TResponse? result, ICollection<ValidationFailure>? errors)
    {
        Result = result;
        Errors = errors;
    }
    public ValidatableResponse() { }
}