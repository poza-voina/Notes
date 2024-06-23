using MediatR;
using Notes.Api.Tags.ViewModels;

namespace Notes.Api.Tags.Queries;

public class GetTagQuery : IRequest<ValidatableResponse<TagVm>>
{
    public int? Id { get; set; }
}