using MediatR;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
using Notes.Api.Reminders.ViewModels;

namespace Notes.Api.Reminders.Queries;

public class GetReminderQueryHandler : IRequestHandler<GetReminderQuery, ValidatableResponse<ReminderVm>>
{
    private readonly IRepository<Reminder> _reminderRepository;

    public GetReminderQueryHandler(IRepository<Reminder> reminderRepository)
    {
        _reminderRepository = reminderRepository;
    }
    public async Task<ValidatableResponse<ReminderVm>> Handle(GetReminderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var reminder = await _reminderRepository.GetAsync(request.Id);
            return new ValidatableResponse<ReminderVm>
                { Result = new ReminderVm { Id = reminder.Id, Title = reminder.Title, Text = reminder.Text, Tags = reminder.Tags} };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<ReminderVm>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError {Message = e.Message} }
            };
        }
        
    }
}