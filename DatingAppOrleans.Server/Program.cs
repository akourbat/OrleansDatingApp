using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Orleans;
using Microsoft.Extensions.DependencyInjection;
using DatingAppOrleans.Grains.DataAccess;
using Orleans.Hosting;
using MediatR;
using System.IO;
using DatingAppOrleans.Grains.UserGrain;

namespace DatingAppOrleans.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var silo = new SiloHostBuilder()
                    .UseLocalhostClustering()
                    .ConfigureAppConfiguration((hostingContext, config) => 
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory());
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                    })
                    .ConfigureServices((hostingContext, services) =>
                    {
                        var connectionString = hostingContext.Configuration.GetConnectionString("DefaultConnection");
                        services.AddDatingContext(connectionString);
                        services.AddMediatR(typeof(UserGrain).Assembly);
                    })
                    .Build();

            await silo.StartAsync();

            var client = silo.Services.GetRequiredService<IClusterClient>();

            await WebHost.CreateDefaultBuilder(args)
                        .UseConfiguration(new ConfigurationBuilder()
                            .AddCommandLine(args)
                            .Build())
                        .ConfigureServices(services =>
                            services
                             .AddSingleton<IGrainFactory>(client)
                             .AddSingleton<IClusterClient>(client))
                        .UseStartup<Startup>()
                        .Build()
                        .RunAsync();
        }
    }
}
