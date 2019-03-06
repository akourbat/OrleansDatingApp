using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatingAppOrleans.Grains.DataAccess
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDatingContext(this IServiceCollection services, string connectionString = "DefaultConnection")
        {
            services.AddEntityFrameworkSqlite().AddDbContext<DatingContext>((serviceProvider, options) =>
                 options
                    .UseSqlite(connectionString)
                    .UseInternalServiceProvider(serviceProvider));

            return services;
        }
    }
}
