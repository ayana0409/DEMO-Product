using System.Linq.Expressions;
using DEMO_Product.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace DEMO_Product.Shared.Repositories
{
    public class RepositoryQueryBase<T, TContext> : IRepositoryQueryBase<T, TContext>
        where T : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public RepositoryQueryBase(TContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges ? _dbContext.Set<T>().AsNoTracking() :
                _dbContext.Set<T>();

        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges ?
                _dbContext.Set<T>().Where(expression).AsNoTracking() :
                _dbContext.Set<T>().Where(expression);

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items, (current, includeProperties) => current.Include(includeProperties));
            return items;
        }

        public async Task<T?> GetByIdAsync(long id) =>
            await FindByCondition(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();

        public async Task<T?> GetByIdAsync(long id, params Expression<Func<T, object>>[] includeProperties) =>
            await FindByCondition(x => x.Id.Equals(id), false, includeProperties)
                .FirstOrDefaultAsync();
    }
}
