using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;

namespace Notes.Infrastructure.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(int id);
    Task<TEntity> UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    public IQueryable<TEntity> Items { get; }
}

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

    public async void DeleteAsync(TEntity entity)
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