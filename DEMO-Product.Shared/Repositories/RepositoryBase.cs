using DEMO_Product.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DEMO_Product.Shared.Repositories
{
    public class RepositoryBase<T, TContext> : RepositoryQueryBase<T, TContext>,
        IRepositoryBaseAsync<T, TContext> where T : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public RepositoryBase(TContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<IDbContextTransaction> BeginTransactionAsync() => _dbContext.Database.BeginTransactionAsync();

        public async Task EndTransactionAsync()
        {
            await SaveChangeAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        public Task RollbackTransactionAsync() => _dbContext.Database.RollbackTransactionAsync();

        public void Create(T entity) => _dbContext.Set<T>().Add(entity);

        public async Task<long> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveChangeAsync();
            return entity.Id;
        }

        public IList<long> CreateList(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            return entities.Select(x => x.Id).ToList();
        }

        public async Task<IList<long>> CreateListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await SaveChangeAsync();
            return entities.Select(x => x.Id).ToList();
        }

        public void Update(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;

            T? exist = _dbContext.Set<T>().Find(entity.Id);
            if (exist == null) return;

            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged) return;

            T? exist = _dbContext.Set<T>().Find(entity.Id);
            if (exist == null) return;

            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            await SaveChangeAsync();
        }

        public void UpdateList(IEnumerable<T> entities) => _dbContext.Set<T>().AddRangeAsync(entities);

        public async Task UpdateListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await SaveChangeAsync();
        }

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteList(IEnumerable<T> entities) => _dbContext.Set<T>().RemoveRange(entities);

        public async Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public Task<int> SaveChangeAsync() => _dbContext.SaveChangesAsync();

    }
}
