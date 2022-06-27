using ApiWorkshop.Application.Data.Context;
using ApiWorkshop.Application.Domain.Entities.Base;
using ApiWorkshop.Application.Domain.Enums;
using ApiWorkshop.Application.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiWorkshop.Application.Data.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationContext _context;

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id) => _context.Set<TEntity>().Remove(await Where(id));
    public void Delete(Func<TEntity, bool> query) => Where(query).ToList().ForEach(entity => _context.Set<TEntity>().Remove(entity));

    public async Task Inactive(Guid id)
    {
        var obj = await Where(id);
        obj.UpdatedAt = DateTime.UtcNow;
        obj.Status = Status.INACTIVE;
        _context.Set<TEntity>().Update(obj);
    }

    public void Insert(TEntity obj) => _context.Set<TEntity>().Add(obj);

    public void Update(TEntity obj)
    {
        obj.UpdatedAt = DateTime.UtcNow;
        _context.Set<TEntity>().Update(obj);
    }

    public IQueryable<TEntity> Where() => _context.Set<TEntity>().AsNoTracking();

    public async Task<TEntity> Where(Guid id) => (await _context.Set<TEntity>().FindAsync(id))!;

    public IQueryable<TEntity> Where(Func<TEntity, bool> query) => _context.Set<TEntity>().Where(query).AsQueryable();

    public int Count() => _context.Set<TEntity>().Count();
    public int Count(Func<TEntity, bool> query) => _context.Set<TEntity>().Count(query);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}