using System;
using System.Collections.Generic;
using System.IO;
using spheredistance.Model;
using spheredistance.Services;
using Xunit;

namespace spheredistanceApp.Tests
{
    public class CustomerOutputTest
    {
        CustomerOutputService init()
        {
            return new CustomerOutputService();
        }

        List<Customer> initMockCustomers()
        {
            List<Customer> mockCustomers = new List<Customer>();
            mockCustomers.Add(new Customer()
            {
                user_id = 1,
                name = "Patrick",
                latitude = 1,
                longitude = 2
            });
            mockCustomers.Add(new Customer()
            {
                user_id = 2,
                name = "John Doe",
                latitude = 1,
                longitude = 2
            });
            return mockCustomers;
        }

        [Fact]
        public void testOutput()
        {
            var customerOutputService = init();
            var mockCustomers = initMockCustomers();
            var memoryStream = new MemoryStream();
            var mockWriter = new StreamWriter(memoryStream);
            customerOutputService.writeCustomers(mockCustomers, mockWriter);
            memoryStream.Position = 0;
            var resultReader = new StreamReader(memoryStream);
            var resultLine1 = resultReader.ReadLine();
            Assert.Equal("ID: 1; Name: Patrick", resultLine1);
            var resultLine2 = resultReader.ReadLine();
            Assert.Equal("ID: 2; Name: John Doe", resultLine2);
        }
    }
}