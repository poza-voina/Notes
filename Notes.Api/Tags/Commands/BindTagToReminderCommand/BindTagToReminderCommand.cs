using MediatR;
using Notes.Core.Entities;

namespace Notes.Api.Tags.Commands;


public class BindTagToReminderCommand : IRequest<ValidatableResponse<int>>
{
    public int? TagId { get; set; }
    public int? ReminderId { get; set; }
}