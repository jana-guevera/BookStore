using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<BookStoreDbContext>(options => 
                                   options.UseSqlServer(configuration.GetConnectionString("default")));

            return services;
        }
    }
}
