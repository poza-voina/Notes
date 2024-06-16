using MediatR;
namespace Notes.Api.Tags.Commands;

public class DeleteTagCommand : IRequest<ValidatableResponse<int>>
{
    public int? Id { get; set; }
}