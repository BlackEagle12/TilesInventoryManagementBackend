using Microsoft.Extensions.DependencyInjection;

namespace Repo
{
    public static class RepoDependencies
    {
        public static void InjectRepoDependencies(this IServiceCollection service)
        {
            service.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ICountryRepository, CountryRepository>();
            service.AddScoped<IStateRepository, StateRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IPermissionRepository, PermissionRepository>();
            service.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        }
    }
}
