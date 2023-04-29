using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFaculty.Application.Common.Interfaces;
using System;

namespace MyFaculty.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionString"];
            services.AddDbContext<MFDbContext>(options =>
            {
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 11)));
            });
            services.AddScoped<IMFDbContext>(provider => provider.GetService<MFDbContext>());
            return services;
        }
    }
}
