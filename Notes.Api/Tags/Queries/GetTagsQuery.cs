using MediatR;

namespace Notes.Api.Tags.Queries;

public class GetTagsQuery : IRequest<TagsVm> { }