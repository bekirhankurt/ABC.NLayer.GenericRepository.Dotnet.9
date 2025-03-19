using System.Linq.Expressions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository.EntityFrameworkCore;

public class EntityFrameworkRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
where TEntity : class, IEntity, new()
where TContext : DbContext, new()
{
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        await using var context = new TContext();
        return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        await using var context = new TContext();
        return filter == null
            ? await context.Set<TEntity>().ToListAsync()
            : await context.Set<TEntity>().Where(filter).ToListAsync();
    }

    public void Add(TEntity entity)
    {
        using var context = new TContext();
        var entityToBeAdded = context.Entry(entity);
        entityToBeAdded.State = EntityState.Added;
        context.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        using var context = new TContext();
        var entityToBeUpdated = context.Entry(entity);
        entityToBeUpdated.State = EntityState.Modified;
        context.SaveChanges();
    }

    public void Delete(TEntity entity)
    {
        using var context = new TContext();
        var entityToBeDeleted = context.Entry(entity);
        entityToBeDeleted.State = EntityState.Deleted;
        context.SaveChanges();



    }
}