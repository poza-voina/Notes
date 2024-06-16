using MediatR;
using Notes.Api.Tags.Queries.ViewModels;

namespace Notes.Api.Tags.Queries;

public class GetTagsQuery : IRequest<TagsVm> { }