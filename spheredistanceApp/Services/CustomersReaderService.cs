using System;
using System.Collections.Generic;
using System.IO;
using spheredistance.Model;
using Newtonsoft.Json;

namespace spheredistance.Services
{
    //This class simply reads an input text stream and converts it to a list of customer objects
    //Input must be in a specific format specified by the problem statement
    public interface ICustomerReaderService
    {
        List<Customer> readCustomers(TextReader inputReader);
    }

    public class CustomerReaderService : ICustomerReaderService
    {
        public List<Customer> readCustomers(TextReader inputReader)
        {
            List<Customer> customers = new List<Customer>();
            //Attempt to read an input line
            var currentCustomerText = inputReader.ReadLine();
            //While we have new input, keep reading text
            while (currentCustomerText != null)
            {
                //The line must contain at least opening and closing braces {} (other than whitespace)
                if (currentCustomerText.Replace(" ", "").Length > 2)
                {
                    //De-serialize the line into a Customer object. For this implementation, expect a correct input json
                    try
                    {
                        var currentCustomerObject = JsonConvert.DeserializeObject<Customer>(currentCustomerText);
                        if (currentCustomerObject.name == null)
                        {
                            currentCustomerObject.name = "Unknown";
                            Console.WriteLine("Warning: Customer name not provided, using 'Unknown'");
                        }
                        if (currentCustomerObject.latitude == 0)
                            Console.WriteLine("Warning: Using default latitude of 0, input data may be incomplete");
                        if (currentCustomerObject.longitude == 0)
                            Console.WriteLine("Warning: Using default longitude of 0, input data may be incomplete");
                        if (currentCustomerObject.user_id == 0)
                            Console.WriteLine("Warning: Using default user_id of 0, input data may be incomplete");
                        customers.Add(currentCustomerObject);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Warning: error reading input line, skipping." + e.Message);
                    }
                }
                currentCustomerText = inputReader.ReadLine();
            }
            return customers;
        }
    }
}