using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using spheredistance.Services;

namespace spheredistance
{
    class Program
    {
        //Initialize the app and set up dependencies
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            //Very basic input gathering and validation
            //Runs with defaults as provided by the problem if no input provided
            var earthRadiusKm = config.GetValue<double>("earthRadiusKm");
            earthRadiusKm = earthRadiusKm <= 0 ? 6371.0088 : earthRadiusKm;
            var inputFile = config.GetValue<string>("inputFile");
            inputFile = inputFile == null ? "./sample/customers.txt" : inputFile;
            var outputFile = config.GetValue<string>("outputFile");
            outputFile = outputFile == null ? "./sample/output.txt" : outputFile;
            var officeLongitude = config.GetValue<double>("officeLongitude");
            officeLongitude = officeLongitude == 0 ? -6.238335 : officeLongitude;
            var officeLatitude = config.GetValue<double>("officeLatitude");
            officeLatitude = officeLatitude == 0 ? 53.2451022 : officeLatitude;
            var rangeKm = config.GetValue<double>("rangeKm");
            rangeKm = rangeKm <= 0 ? 100 : rangeKm;

            //Set up dependencies for each part of the process: Reading input file, calculating circle distance and writing output
            //Using dependency injection
            var services = new ServiceCollection()
            .AddScoped(typeof(ICustomerReaderService), typeof(CustomerReaderService))
            .AddScoped<IGreatCircleDistanceService>(_serviceProvider => new GreatCircleDistanceService(earthRadiusKm))
            .AddScoped(typeof(ICustomerOutputService), typeof(CustomerOutputService))
            .AddTransient<SphereDistanceApp>();
            var serviceProvider = services.BuildServiceProvider();

            //Run the application
            serviceProvider.GetService<SphereDistanceApp>().run(inputFile, outputFile, officeLongitude, officeLatitude, rangeKm);
        }
    }
}
