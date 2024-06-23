using MediatR;
using Notes.Api.Tags.ViewModels;
using Notes.Core.Entities;

namespace Notes.Api.Tags.Commands;

public class UpdateTagCommand : IRequest<ValidatableResponse<TagVm>>
{
    public int? Id { get; set; }
    public string? Title { get; set; }
}