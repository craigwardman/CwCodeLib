using System;

namespace CwCodeLib.Mapping
{
    public class DistanceCalculatorService
    {
        public double DistanceInKm(GeographicalPoint pointA, GeographicalPoint pointB)
        {
            var p1Lat = this.DegToRad(pointA.Latitude);
            var p1Lon = this.DegToRad(pointA.Longitude);

            var p2Lat = this.DegToRad(pointB.Latitude);
            var p2Lon = this.DegToRad(pointB.Longitude);

            var r = 6371; // earth's mean radius in km
            var dLat = p2Lat - p1Lat;
            var dLong = p2Lon - p1Lon;
            var a = (Math.Sin(dLat / 2) * Math.Sin(dLat / 2)) + (Math.Cos(p1Lat) * Math.Cos(p2Lat) * Math.Sin(dLong / 2) * Math.Sin(dLong / 2));
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var disKm = r * c;

            return (double)Math.Round((decimal)disKm, 2);
        }

        public double DistanceInMiles(GeographicalPoint pointA, GeographicalPoint pointB)
        {
            double distanceInMiles = 0.0;
            double distanceInKm = this.DistanceInKm(pointA, pointB);
            distanceInMiles = distanceInKm * 0.621371d;

            return (double)Math.Round((decimal)distanceInMiles, 2);
        }

        private double DegToRad(double number)
        {
            return number * Math.PI / 180;
        }
    }
}