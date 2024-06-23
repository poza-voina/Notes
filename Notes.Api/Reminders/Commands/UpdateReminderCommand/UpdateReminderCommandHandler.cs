using System.Diagnostics;
using MediatR;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Interfaces.IServices;
using Notes.Api.Reminders.ViewModels;

namespace Notes.Api.Reminders.Commands;

public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand, ValidatableResponse<ReminderVm>>
{
    private readonly IRepository<Reminder> _reminderRepository;
    private readonly ITagsService _tagsService;

    public UpdateReminderCommandHandler(IRepository<Reminder> reminderRepository, ITagsService tagsService)
    {
        _reminderRepository = reminderRepository;
        _tagsService = tagsService;
    }
    
    public async Task<ValidatableResponse<ReminderVm>> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var reminder = await _reminderRepository.GetAsync(request.Id!.Value);
            reminder.Text = request.Text ?? reminder.Text;
            reminder.Title = request.Title ?? reminder.Title;
            if (request.TagsTitles is not null)
            {
                await _tagsService.SetTagsToReminderAsync(request.TagsTitles, reminder);
            }
            await _reminderRepository.UpdateAsync(reminder);
            return new ValidatableResponse<ReminderVm>
            {
                Result = new ReminderVm
                {
                    Id = reminder.Id,
                    Title = reminder.Title,
                    Text = reminder.Text,
                    Tags = reminder.Tags
                }
            };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<ReminderVm>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError { Message = e.Message } }
            };
        }
    }
}