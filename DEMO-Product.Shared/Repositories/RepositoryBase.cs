using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DEMO_Product.Shared.Repositories
{
    public class RepositoryBase<T, TContext> : IRepositoryBase<T> where T : class
        where TContext : DbContext
    {
        private readonly TContext _applicationDbContext;
        public RepositoryBase(TContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null)
        {
            IQueryable<T> query = _applicationDbContext.Set<T>();

            if (expression == null)
                return await query.AsNoTracking().ToListAsync();

            return await query.AsNoTracking().Where(expression).ToListAsync();
        }
        public async Task<T?> GetSigleAsync(Expression<Func<T, bool>> expression)
        {
            return await _applicationDbContext.Set<T>().AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }
        public async Task CreateAsync(T entity)
        {
            await _applicationDbContext.Set<T>().AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
        public void Update(T entity)
        {
            _applicationDbContext.Set<T>().Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
            _applicationDbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            _applicationDbContext.Set<T>().Attach(entity);
            _applicationDbContext.Entry(entity).State = EntityState.Deleted;
            _applicationDbContext.SaveChanges();
        }
    }
}
