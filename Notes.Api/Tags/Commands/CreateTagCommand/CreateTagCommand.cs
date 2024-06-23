using MediatR;
using Notes.Api.Tags.ViewModels;
namespace Notes.Api.Tags.Commands;


public class CreateTagCommand : IRequest<ValidatableResponse<TagVm>>
{
    public string? Title { get; set; }
} 