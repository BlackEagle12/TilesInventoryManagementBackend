using Core;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Repo
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected readonly InventoryDBContext _context;
        protected DbSet<T> _db;
        public BaseRepo(
                InventoryDBContext context
            )
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public virtual async Task<KeyValuePair<int, IQueryable<T>>> GetPaginated(CommonGridParams gridParams)
        {
            Expression<Func<T, bool>> filterExp = x => true; //PradicateBuilder.GetExpression<T>();

            var queryable = _db.Where(filterExp);

            if (!string.IsNullOrEmpty(gridParams.SortBy))
            {
                Expression<Func<T, object>> sortExp = PradicateBuilder.CreateSortExpression<T>(gridParams.SortBy);

                if (gridParams.IsDescending)
                    queryable = queryable.OrderByDescending(sortExp);
                else
                    queryable = queryable.OrderBy(sortExp);
            }

            var count = queryable.CountAsync();
            queryable = queryable.Skip((gridParams.page-1) * gridParams.pageSize).Take(gridParams.pageSize);

            return new KeyValuePair<int, IQueryable<T>>(await count, queryable);
        }
        
        public virtual async Task<KeyValuePair<int, IQueryable<T>>> GetPaginated(string ortBy, bool isDescending, int page, int pageSize)
        {
            var queryable = _db;

            if (!string.IsNullOrEmpty(ortBy))
            {
                Expression<Func<T, object>> sortExp = PradicateBuilder.CreateSortExpression<T>(sortBy);

                if (isDescending)
                    queryable = queryable.OrderByDescending(sortExp);
                else
                    queryable = queryable.OrderBy(sortExp);
            }

            var count = queryable.CountAsync();
            queryable = queryable.Skip((page-1) * pageSize).Take(pageSize);

            return new KeyValuePair<int, IQueryable<T>>(await count, queryable);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _db.AnyAsync(expression);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task CompleTransaction(IDbContextTransaction transaction)
        {
            try
            {
                await transaction.CommitAsync();
            }
            finally
            {
                await transaction.RollbackAsync();
            }
        }

        public void Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _db.FindAsync(id);
        }

        public IQueryable<T> GetQueyable(bool asNoTracking = false)
        {
            return  asNoTracking ? _db.AsNoTracking() : _db;
        }

        public async Task InsertAsync(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Confusing with LINQ Select and this provide expression in GetQueryable only
        public IQueryable<T> Select(Expression<Func<T, bool>> expression) 
        {
            return _db.Where(expression);
        }

        public void Update(T entity)
        {
            _db.Update(entity);
        }
    }
}
