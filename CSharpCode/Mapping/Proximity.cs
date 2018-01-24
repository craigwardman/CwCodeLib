namespace CwCodeLib.Mapping
{
    public class Proximity : GeographicalPoint
    {
        private double radiusInMeters;

        public Proximity(double centerLatitude, double centerLongitude, double radiusInMeters)
            : base(centerLatitude, centerLongitude)
        {
            this.RadiusInMeters = radiusInMeters;
        }

        public double RadiusInMeters
        {
            get
            {
                return this.radiusInMeters;
            }

            private set
            {
                this.radiusInMeters = value;
            }
        }
    }
}