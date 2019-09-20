using System;
using System.IO;
using spheredistance.Services;
using spheredistance.Model;
using System.Collections.Generic;

namespace spheredistance
{
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
            List<Customer> customerList = customerReaderService.readCustomers(new StreamReader(fileToRead));
            List<Customer> inRangeCustomerList = new List<Customer>();
            foreach (var customer in customerList)
            {
                var distance = greatCircleDistanceService.calculateGreatCircleDistance(targetLongitude, targetLatitude, customer.longitude, customer.latitude);
                if (distance <= 100) inRangeCustomerList.Add(customer);
            }
            inRangeCustomerList.Sort((item1, item2) => item1.user_id.CompareTo(item2.user_id));
            customerOutputService.writeCustomers(inRangeCustomerList, new StreamWriter(outputFile));
        }
    }
}
