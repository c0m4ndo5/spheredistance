using System;
using spheredistance.Services;
using Xunit;

namespace spheredistanceApp.Tests
{
    //Tests with 10 decimals precision with values obtained through operating system calculator
    public class GreatCircleDistanceTest
    {
        GreatCircleDistanceService init()
        {
            return new GreatCircleDistanceService(6371.0088);//using mean earth radius
        }

        [Fact]
        public void testRadiansCalculation()
        {
            var greatCircleDistanceService = init();
            double radians = greatCircleDistanceService.degreesToRadians(100);
            Assert.Equal(17453292519, (long)(radians * 10000000000));

            radians = greatCircleDistanceService.degreesToRadians(-50);
            Assert.Equal(-8726646259, (long)(radians * 10000000000));
        }

        [Fact]
        public void testCentralAngleCalculation()
        {
            var greatCircleDistanceService = init();
            double centralAngleRadians = greatCircleDistanceService.calculateCentralAngle(-0.1047197551, 0.8901179185, -0.1221730476, 0.9075712110);

            Assert.Equal(205583336, (long)(centralAngleRadians * 10000000000));
            //Test closer distance, close to real use case in this problem
            centralAngleRadians = greatCircleDistanceService.calculateCentralAngle(-0.1117010721, 0.8918632477, -0.1134464013, 0.8936085770);
            Assert.Equal(20602901, (long)(centralAngleRadians * 10000000000));
            //Test opposite long/latitudes, should result in the same value
            centralAngleRadians = greatCircleDistanceService.calculateCentralAngle(-0.1134464013, 0.8936085770, -0.1117010721, 0.8918632477);
            Assert.Equal(20602901, (long)(centralAngleRadians * 10000000000));
        }

        [Fact]
        public void testGreatCircleDistance()
        {
            var greatCircleDistanceService = init();
            var distance = greatCircleDistanceService.calculateGreatCircleDistance(-6.4, 51.1, -6.5, 51.2);
            //Assuming we dont need more than 0.5% precision. Using calculated value from https://www.gpsvisualizer.com/calculators
            double error = Math.Abs(13.143 - distance);
            Assert.True(error < 13.143 * 0.005);
            //test with real use case
            distance = greatCircleDistanceService.calculateGreatCircleDistance(-6.257664, 53.339428, -6.238335, 53.2451022);
            error = Math.Abs(10.577 - distance);
            Assert.True(error < 10.577 * 0.005);
        }
    }
}