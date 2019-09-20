using System;
using System.Collections.Generic;

namespace spheredistance.Services
{
    public interface IGreatCircleDistanceService
    {
        double calculateGreatCircleDistance(double long1, double lat1, double long2, double lat2);
        double calculateCentralAngle(double long1Radians, double lat1Radians,
        double long2Radians, double lat2Radians);
        double degreesToRadians(double angle);
    }

    public class GreatCircleDistanceService : IGreatCircleDistanceService
    {
        public double sphereRadius;
        public GreatCircleDistanceService(double sphereRadius)
        {
            this.sphereRadius = sphereRadius;
        }
        public double calculateGreatCircleDistance(double long1, double lat1, double long2, double lat2)
        {
            double long1Radians = degreesToRadians(long1);
            double lat1Radians = degreesToRadians(lat1);
            double long2Radians = degreesToRadians(long2);
            double lat2Radians = degreesToRadians(lat2);

            double centralAngle = calculateCentralAngle(long1Radians, lat1Radians, long2Radians, lat2Radians);

            return sphereRadius * centralAngle;
        }

        public double calculateCentralAngle(double long1Radians, double lat1Radians,
        double long2Radians, double lat2Radians)
        {
            return Math.Acos(Math.Sin(lat1Radians) * Math.Sin(lat2Radians) +
                        Math.Cos(lat1Radians) * Math.Cos(lat2Radians) * Math.Cos(long2Radians - long1Radians));
        }

        public double degreesToRadians(double angle)
        {
            return angle * Math.PI / 180;
        }
    }
}