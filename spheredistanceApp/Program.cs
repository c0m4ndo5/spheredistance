using System;
using Microsoft.Extensions.DependencyInjection;
using spheredistance.Services;

namespace spheredistance
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
            .AddScoped(typeof(ICustomerReaderService), typeof(CustomerReaderService))
            .AddScoped<IGreatCircleDistanceService>(_serviceProvider => new GreatCircleDistanceService(6371.0088))
            .AddScoped(typeof(ICustomerOutputService), typeof(CustomerOutputService))
            .AddTransient<SphereDistanceApp>();

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<SphereDistanceApp>().run("./sample/customers.txt", "./sample/output.txt", -6.238335, 53.2451022, 100);
        }
    }
}
