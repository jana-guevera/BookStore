using Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Caching;
using Repositories.Database;
using Repositories.Repos;

namespace Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration) {
            // DbContext Service
            services.AddDbContext<BookStoreDbContext>(options => 
                                   options.UseSqlServer(configuration.GetConnectionString("db")));

            // Repository Service
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICacheService, CustomMemoryCache>();

            return services;
        }
    }
}
