namespace StudentCouncilTracker.Application.Repositories.Bases;

public interface IRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity?> GetByIdAsync(object id);

    TEntity? GetById(object id);
}