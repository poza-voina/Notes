using FluentValidation;
using MediatR;
using Notes.Api.Reminders.ViewModels;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;
using Notes.Core.Interfaces.IServices;

namespace Notes.Api.Reminders.Commands;

public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, ValidatableResponse<ReminderVm>>
{
    private readonly IRepository<Reminder> _reminderRepository;
    private readonly ITagsService _tagsService;
    public CreateReminderCommandHandler(IRepository<Reminder> reminderRepository, ITagsService tagsService)
    {
        _reminderRepository = reminderRepository;
        _tagsService = tagsService;
    }
    
    public async Task<ValidatableResponse<ReminderVm>> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
    {
        Reminder reminder = new Reminder { Title = request.Title ?? "", Text = request.Text ?? ""};
        if (request.TagsTitles is not null && request.TagsTitles.Count != 0)
        {
            await _tagsService.SetTagsToReminderAsync(request.TagsTitles, reminder);
        }
        
        reminder = await _reminderRepository.CreateAsync(reminder);
        return new ValidatableResponse<ReminderVm> {Result = new ReminderVm {Id = reminder.Id, Title = reminder.Title, Text = reminder.Text, Tags = reminder.Tags}};
    }
}