using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace spheredistance
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
            .AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole(options =>
                {
                    options.IncludeScopes = true;
                });
                //loggingBuilder.SetMinimumLevel(LogLevel.Debug);
            }).AddTransient<SphereDistanceApp>();

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<SphereDistanceApp>().run();
        }
    }
}
