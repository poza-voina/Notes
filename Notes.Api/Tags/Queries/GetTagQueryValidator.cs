using FluentValidation;
namespace Notes.Api.Tags.Queries;

public class GetTagQueryValidator : AbstractValidator<GetTagQuery>
{
    public GetTagQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}