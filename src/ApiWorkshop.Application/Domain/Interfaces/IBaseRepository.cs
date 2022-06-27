using ApiWorkshop.Application.Domain.Entities.Base;

namespace ApiWorkshop.Application.Domain.Interfaces;
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    int Count();
    int Count(Func<TEntity, bool> query);
    Task Delete(Guid id);
    void Delete(Func<TEntity, bool> query);
    Task Inactive(Guid id);
    void Insert(TEntity obj);
    Task SaveChangesAsync();
    void Update(TEntity obj);
    IQueryable<TEntity> Where();
    Task<TEntity> Where(Guid id);
    IQueryable<TEntity> Where(Func<TEntity, bool> query);
}