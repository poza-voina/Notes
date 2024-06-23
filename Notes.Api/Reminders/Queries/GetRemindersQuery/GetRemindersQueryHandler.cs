using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Api.Reminders.ViewModels;
using Notes.Core.Interfaces.IRepositories;

namespace Notes.Api.Reminders.Queries;

public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, RemindersVm>
{
    private readonly IRepository<Reminder> _reminderRepository;

    public GetRemindersQueryHandler(IRepository<Reminder> reminderRepository)
    {
        _reminderRepository = reminderRepository;
    }
    public async Task<RemindersVm> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
    {
        return new RemindersVm { Reminders = await _reminderRepository.Items.ToListAsync() };
    }
}