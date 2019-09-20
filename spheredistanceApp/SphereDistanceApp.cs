using System;
using System.IO;
using spheredistance.Services;
using spheredistance.Model;
using System.Collections.Generic;

namespace spheredistance
{
    //This class is responsible for putting together all of the services and using them
    public class SphereDistanceApp
    {
        ICustomerReaderService customerReaderService;
        IGreatCircleDistanceService greatCircleDistanceService;
        ICustomerOutputService customerOutputService;
        public SphereDistanceApp(ICustomerReaderService customerReaderService,
        IGreatCircleDistanceService greatCircleDistanceService,
        ICustomerOutputService customerOutputService)
        {
            this.customerReaderService = customerReaderService;
            this.customerOutputService = customerOutputService;
            this.greatCircleDistanceService = greatCircleDistanceService;
        }
        public void run(string fileToRead, string outputFile, double targetLongitude, double targetLatitude, double rangeKm)
        {
            //Main flow of the problem resolution
            //1. Read input customer list using customer reader service
            List<Customer> customerList = customerReaderService.readCustomers(new StreamReader(fileToRead));
            List<Customer> inRangeCustomerList = new List<Customer>();
            //2. For each of the customers...
            foreach (var customer in customerList)
            {
                //...Calculate the great circle distance using the circle distance service
                var distance = greatCircleDistanceService.calculateGreatCircleDistance(targetLongitude, targetLatitude, customer.longitude, customer.latitude);
                //And check if within the range specified. Then, add to a result list
                if (distance <= rangeKm) inRangeCustomerList.Add(customer);
            }
            //3. Sort the result list in ascending order of user id
            inRangeCustomerList.Sort((item1, item2) => item1.user_id.CompareTo(item2.user_id));
            //4. Write to an output file using customer output service
            customerOutputService.writeCustomers(inRangeCustomerList, new StreamWriter(outputFile));
        }
    }
}
