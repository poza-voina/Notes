using FluentValidation;
namespace Notes.Api.Tags.Commands;

public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}