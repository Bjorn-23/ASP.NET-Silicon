using Infrastructure.Context;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    private readonly DataContext _context = context;

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch { }
        return null!;
    }


}