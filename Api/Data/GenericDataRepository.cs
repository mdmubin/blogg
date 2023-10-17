using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Data;

public class GenericDataRepository<T>
    where T : class
{
    private readonly BloggContext context;

    public GenericDataRepository(BloggContext context)
    {
        this.context = context;
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool tracking = false)
    {
        return tracking
            ? context.Set<T>().Where(condition).AsTracking()
            : context.Set<T>().Where(condition).AsNoTracking();
    }

    public void Create(T item)
    {
        context.Set<T>().Add(item);
    }

    public void Update(T item)
    {
        context.Set<T>().Update(item);
    }

    public void Delete(T item)
    {
        context.Set<T>().Remove(item);
    }

    public void DeleteAll(ICollection<T> items)
    {
        context.Set<T>().RemoveRange(items);
    }
}
