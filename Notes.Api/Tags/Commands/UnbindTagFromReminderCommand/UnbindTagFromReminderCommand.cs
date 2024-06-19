using MediatR;

namespace Notes.Api.Tags.Commands;

public class UnbindTagFromReminderCommand : IRequest<ValidatableResponse<int>>
{
    public int? TagId { get; set; }
    public int? ReminderId { get; set; }
}