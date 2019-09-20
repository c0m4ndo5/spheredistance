using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using spheredistance.Model;

namespace spheredistance.Services
{
    public interface ICustomerOutputService
    {
        void writeCustomers(List<Customer> customers, TextWriter textWriter);
    }
    public class CustomerOutputService : ICustomerOutputService
    {
        public void writeCustomers(List<Customer> customers, TextWriter textWriter)
        {
            foreach (var customer in customers)
            {
                var customerTextLine = "ID: " + customer.user_id + "; Name: " + customer.name;
                textWriter.WriteLine(customerTextLine);
            }
            textWriter.Flush();
        }
    }
}