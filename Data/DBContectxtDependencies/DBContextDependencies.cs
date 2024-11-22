using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class DBContextDependencies
    {
        public static void InjectDBContextDependencies(this IServiceCollection service, string connString)
        {
            service.AddDbContext<InventoryDBContext>(options => options.UseSqlServer(connString));
        }
    }
}
