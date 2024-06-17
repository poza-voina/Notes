using System.Data.Common;
using MediatR;
using Notes.Infrastructure.Repositories;
using Notes.Core.Entities;
namespace Notes.Api.Reminders.Commands;

public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand, ValidatableResponse<int>>
{
    private readonly IRepository<Reminder> _reminderRepository;

    public DeleteReminderCommandHandler(IRepository<Reminder> reminderRepository)
    {
        _reminderRepository = reminderRepository;
    }
    
    public async Task<ValidatableResponse<int>> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            int id = request.Id!.Value;
            _reminderRepository.DeleteAsync(await _reminderRepository.GetAsync(id));
            return new ValidatableResponse<int> { Result = id };
        }
        catch (ArgumentException e)
        {
            return new ValidatableResponse<int>
            {
                ProcessingErrors = new List<ProcessingError> { new ProcessingError {Message = e.Message}}
            };
        }
    }
}