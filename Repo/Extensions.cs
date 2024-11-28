using Core;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repo
{
    public static class Extensions
    {
        public static async Task<List<T>> GetList<T>(this IQueryable<T> queryable, CancellationToken cancellationToken = default)
        {
            return await queryable.ToListAsync(cancellationToken);
        }

        public static async Task<T?> GetSingle<T>(this IQueryable<T> queryable, bool throwIfNull = false, CancellationToken cancellationToken = default)
        {
            var value = await queryable.FirstOrDefaultAsync(cancellationToken);

            if (value is null && throwIfNull)
                throw new NullReferenceException();

            return value;
        }

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> source, List<CommonFilterParams>? gridParams)
        {
            if (gridParams != null && gridParams.Any())
            {
                Expression<Func<T, bool>> filterExp = PradicateBuilder.BuildFilterExpression<T>(gridParams);
                source = source.Where(filterExp);           
            } 

            return source;
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> source, string sortField, bool isDescending = false)
        {
            if (!string.IsNullOrEmpty(sortField))
            {
                Expression<Func<T, object>> sortExp = PradicateBuilder.BuildSortExpression<T>(sortField);

                if (isDescending)
                    source = source.OrderByDescending(sortExp);
                else
                    source = source.OrderBy(sortExp);
            }

            return source;
        }

        public static async Task<KeyValuePair<int, List<T>>> GetPaginatedAsync<T>(this IQueryable<T> source, int page, int pageSize)
        {
            var count = await source.Select(x => 0).CountAsync();

            var result = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new KeyValuePair<int, List<T>>(count, result);

        }
        
        public static IQueryable<T> GetPaginatedAsync<T>(this IQueryable<T> source, int page, int pageSize, out int count)
        {
            count = source.Select(x => 0).Count();

            source = source.Skip((page - 1) * pageSize).Take(pageSize);

            return source;
        }
    }
}
