﻿using Contracts.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Database;
using Repositories.Repos;

namespace Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration) {
            // DbContext Service
            services.AddDbContext<BookStoreDbContext>(options => 
                                   options.UseSqlServer(configuration.GetConnectionString("default")));

            // Repository Service
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
