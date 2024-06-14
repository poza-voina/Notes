using MediatR;
namespace Notes.Api.Tags.Queries;

public class GetTagQuery : IRequest<ValidatableResponse<TagVm>>
{
    public int? Id { get; set; }
}