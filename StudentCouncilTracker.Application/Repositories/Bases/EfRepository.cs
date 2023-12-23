using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace StudentCouncilTracker.Application.Repositories.Bases;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    private readonly IMapper _mapper;

    public EfRepository(DbContext dbContext, IMapper mapper)
    {
        _dbSet = dbContext.Set<TEntity>();
        _mapper = mapper;
    }

    public ValueTask<TEntity?> GetByIdAsync(object id)
    {
        return _dbSet.FindAsync(id);
    }

    public TEntity? GetById(object id)
    {
        return _dbSet.Find(id);
    }
}