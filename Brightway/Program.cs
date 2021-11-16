using Brightway.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Brightway
{
    class Program
    {
        static void Main(string[] args)
        {
            StartApplication(args);
        }


        public static void StartApplication(string[] args)
        {
            var serviceProvider = ConfigureServices().BuildServiceProvider();

            serviceProvider.GetService<PizzaApplication>().Start();
        }


        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            BuildConfiguration(services);

            services.AddTransient<IPizzaToppingRepo, PizzaToppingRepo>();
            services.AddTransient<IRequestClient, HttpWebClient>();

            services.AddTransient<PizzaApplication>();

            return services;
        }

        public static void BuildConfiguration(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            services.AddScoped(_ => configuration);
        }
    }
}
