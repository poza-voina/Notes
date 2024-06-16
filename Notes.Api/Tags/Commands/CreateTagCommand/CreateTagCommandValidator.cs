using FluentValidation;
namespace Notes.Api.Tags.Commands;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}

