namespace Notes.Core.Interfaces.IRepositories;

public interface IRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> GetAsync(int id);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    public IQueryable<TEntity> Items { get; }
}