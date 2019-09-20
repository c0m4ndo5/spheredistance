using System;
using System.Collections.Generic;
using System.IO;
using spheredistance.Model;
using Newtonsoft.Json;

namespace spheredistance.Services
{
    public interface ICustomerReaderService
    {
        //will I need this? leave for later
    }

    public class CustomerReaderService
    {
        /*         TextReader reader;
                public CustomerReaderService(TextReader reader)
                {
                    this.reader = reader;
                } */
        public List<Customer> readCustomers(TextReader inputReader)
        {
            List<Customer> customers = new List<Customer>();
            var currentCustomerText = inputReader.ReadLine();
            while (currentCustomerText != null)
            {
                //The line must contain at least opening and closing braces {} (other than whitespace)
                if (currentCustomerText.Replace(" ", "").Length > 2)
                {
                    var currentCustomerObject = JsonConvert.DeserializeObject<Customer>(currentCustomerText);
                    customers.Add(currentCustomerObject);
                }
                currentCustomerText = inputReader.ReadLine();
            }
            return customers;
        }
    }
}