using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Infrastructure.Repositories;
using Notes.Api.Notes.ViewModels;


namespace Notes.Api.Notes.Queries;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, NotesVm>
{
    private readonly IRepository<Note> _repository;

    public GetNotesQueryHandler(IRepository<Note> repository)
    {
        _repository = repository;
    }
    public async Task<NotesVm> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        return new NotesVm { Notes = await _repository.Items.ToListAsync() };
    }
}