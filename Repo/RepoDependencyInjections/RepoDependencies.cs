using Microsoft.Extensions.DependencyInjection;
using Repo.UserRepo;

namespace Repo
{
    public static class RepoDependencies
    {
        public static void InjectRepoDependencies(this IServiceCollection service)
        {
            service.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ICountryRepository, CountryRepository>();
            service.AddScoped<IStateRepoRepository, StateRepository>();
        }
    }
}
