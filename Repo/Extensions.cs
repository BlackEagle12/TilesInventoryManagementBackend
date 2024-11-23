using Microsoft.EntityFrameworkCore;

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

    }
}
