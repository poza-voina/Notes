using MediatR;
using Notes.Api.Tags.ViewModels;

namespace Notes.Api.Tags.Queries;

public class GetTagsQuery : IRequest<TagsVm> { }