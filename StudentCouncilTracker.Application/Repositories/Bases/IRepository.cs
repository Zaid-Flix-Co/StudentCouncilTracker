using StudentCouncilTracker.Application.Entities.Base.Interfaces;

namespace StudentCouncilTracker.Application.Repositories.Bases;

public interface IRepository<TEntity> where TEntity : class
{
    ISaveChangesResult LastSaveChangesResult { get; }

    ValueTask<TEntity?> GetByIdAsync(object id);

    TEntity? GetById(object id);

    IQueryable<TEntity?> GetAll();

    void Insert(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    public Task<int> SaveChangesAsync();

    public int SaveChanges();
}