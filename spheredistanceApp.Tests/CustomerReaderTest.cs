using System;
using System.IO;
using spheredistance.Services;
using Xunit;

namespace spheredistanceApp.Tests
{
    public class CustomerReaderTest
    {
        CustomerReaderService init()
        {
            return new CustomerReaderService();
        }
        TextReader initMockText(string input)
        {
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.WriteLine(input);
            sw.Flush();
            ms.Position = 0;
            return new StreamReader(ms);
        }
        [Fact]
        public void ReadOneTest()
        {
            var customerReaderService = init();
            var mockDataReader = initMockText("{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}");
            var resultCustomer = customerReaderService.readCustomers(mockDataReader);
            Assert.Equal(12, resultCustomer[0].user_id);
        }

        [Fact]
        public void ReadTwoTest()
        {
            var customerReaderService = init();
            var mockDataReader = initMockText("{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}\n"
            + "{\"latitude\": \"53.008769\", \"user_id\": 11, \"name\": \"Richard Finnegan\", \"longitude\": \"-6.1056711\"}");
            var resultCustomer = customerReaderService.readCustomers(mockDataReader);
            Assert.Equal(11, resultCustomer[1].user_id);
        }
        [Fact]
        public void ReadWithBlankLines()
        {
            var customerReaderService = init();
            var mockDataReader = initMockText("{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}"
            + "\n\n"
            + "{\"latitude\": \"53.008769\", \"user_id\": 11, \"name\": \"Richard Finnegan\", \"longitude\": \"-6.1056711\"}");
            var resultCustomer = customerReaderService.readCustomers(mockDataReader);
            Assert.Equal(11, resultCustomer[1].user_id);
        }

        [Fact]
        public void FailInvalidJson()
        {
            var customerReaderService = init();
            //Removed first , from test input
            var mockDataReader = initMockText("{\"latitude\": \"52.986375\" \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}");

            var resultCustomer = customerReaderService.readCustomers(mockDataReader);
            //Correctly ignores invalid input
            Assert.Empty(resultCustomer);
        }

        [Fact]
        public void HandlesMissingData()
        {
            var customerReaderService = init();
            var mockDataReader = initMockText("{\"latitude\": \"52.986375\", \"user_id\": 12}");
            var resultCustomer = customerReaderService.readCustomers(mockDataReader);
            Assert.Equal(12, resultCustomer[0].user_id);
            Assert.Equal("Unknown", resultCustomer[0].name);
            Assert.Equal(0, resultCustomer[0].longitude);
        }
    }
}
