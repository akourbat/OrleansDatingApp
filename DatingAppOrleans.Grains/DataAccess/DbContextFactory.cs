using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DatingAppOrleans.Grains.DataAccess
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DatingContext>
    {
        public DatingContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<DatingContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            dbContextBuilder.UseSqlite(connectionString, options => options.MigrationsAssembly(typeof(DatingContext).Assembly.GetName().Name));

            return new DatingContext(dbContextBuilder.Options);
        }
    }
}
