using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentCouncilTracker.Application.Entities.Base;
using StudentCouncilTracker.Application.Entities.Base.Interfaces;
using StudentCouncilTracker.Application.Entities.Base.UserCU;
using StudentCouncilTracker.Application.Entities.Interfaces.Haves;

namespace StudentCouncilTracker.Application.Repositories.Bases;

public class EfRepository<TEntity>(DbContext dbContext, IMapper mapper) : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    public ISaveChangesResult LastSaveChangesResult { get; } = new SaveChangesResult();

    public ValueTask<TEntity?> GetByIdAsync(object id)
    {
        return _dbSet.FindAsync(id);
    }

    public TEntity? GetById(object id)
    {
        return _dbSet.Find(id);
    }

    public virtual IQueryable<TEntity?> GetAll()
    {
        return _dbSet;
    }

    public void Insert(TEntity entity)
    {
        entity = FillEntityInsert(entity);
        _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        entity = FillEntityUpdate(entity);
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            LastSaveChangesResult.Exception = e;
            return 0;
        }
    }

    public int SaveChanges()
    {
        try
        {
            return dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            LastSaveChangesResult.Exception = e;
            return 0;
        }
    }

    protected TEntity FillEntityInsert(TEntity entity)
    {
        if (entity is UserCuBase cu)
            cu.CreatedDate = DateTime.Now;

        if (entity is IHaveIsTemp t)
            t.IsTemp = true;

        return entity;
    }

    protected TEntity FillEntityUpdate(TEntity entity)
    {
        if (entity is UserCuBase cu)
            cu.UpdatedDate = DateTime.Now;

        if (entity is IHaveIsTemp t)
            t.IsTemp = false;

        return entity;
    }
}