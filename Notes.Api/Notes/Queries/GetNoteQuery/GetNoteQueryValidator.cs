using FluentValidation;
namespace Notes.Api.Notes.Queries;

public class GetNoteQueryValidator : AbstractValidator<GetNoteQuery>
{
    public GetNoteQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }    
}