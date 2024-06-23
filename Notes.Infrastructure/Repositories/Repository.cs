using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Core.Interfaces.IRepositories;

namespace Notes.Infrastructure.Repositories;


public class Repository<TEntity> : IRepository<TEntity>, IDisposable, IAsyncDisposable where TEntity: BaseEntity
{
    protected ApplicationDbContext DbContext { get; }
    protected DbSet<TEntity> Set { get; }
    public IQueryable<TEntity> Items => Set.AsQueryable();

    public Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        Set = dbContext.Set<TEntity>();
    }
    
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        Set.Add(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> GetAsync(int id) =>
        await Set.FindAsync(id) ?? throw new ArgumentException($"Entity with id = {id} not found.");

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Set.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        Set.Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}