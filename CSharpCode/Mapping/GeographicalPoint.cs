namespace CwCodeLib.Mapping
{
    public class GeographicalPoint
    {
        private double latitude;
        private double longitude;

        public GeographicalPoint(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude
        {
            get
            {
                return this.latitude;
            }

            private set
            {
                if (value < -90 || value > 90)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                this.latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                return this.longitude;
            }

            private set
            {
                if (value < -180 || value > 180)
                {
                    throw new System.ArgumentOutOfRangeException();
                }

                this.longitude = value;
            }
        }
    }
}