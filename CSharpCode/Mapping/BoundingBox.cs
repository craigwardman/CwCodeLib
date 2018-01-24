using System;
using CwCodeLib.Mapping.MapZoomInformation;

namespace CwCodeLib.Mapping
{
    public class BoundingBox
    {
        private GeographicalPoint topLeft;
        private GeographicalPoint topRight;
        private GeographicalPoint bottomLeft;
        private GeographicalPoint bottomRight;

        public BoundingBox(GeographicalPoint topLeft, GeographicalPoint topRight, GeographicalPoint bottomLeft, GeographicalPoint bottomRight)
        {
            this.TopLeft = topLeft;
            this.TopRight = topRight;
            this.BottomLeft = bottomLeft;
            this.BottomRight = bottomRight;

            if (!this.IsValidBoundingBox())
            {
                throw new ArgumentException("Bounding box values were not valid.");
            }
        }

        public GeographicalPoint TopLeft
        {
            get
            {
                return this.topLeft;
            }

            private set
            {
                this.topLeft = value;
            }
        }

        public GeographicalPoint TopRight
        {
            get
            {
                return this.topRight;
            }

            private set
            {
                this.topRight = value;
            }
        }

        public GeographicalPoint BottomLeft
        {
            get
            {
                return this.bottomLeft;
            }

            private set
            {
                this.bottomLeft = value;
            }
        }

        public GeographicalPoint BottomRight
        {
            get
            {
                return this.bottomRight;
            }

            private set
            {
                this.bottomRight = value;
            }
        }

        public MapZoomInfo GetMapZoomInfo(int viewingWidthPx)
        {
            return new MapZoomInfoCalculator().CalculateMapZoomInfo(this, viewingWidthPx);
        }

        private bool IsValidBoundingBox()
        {
            bool isValidBoundingBox = true;

            // check top left is left of all rights
            isValidBoundingBox = isValidBoundingBox && (this.topLeft.Longitude <= this.topRight.Longitude && this.topLeft.Longitude <= this.bottomRight.Longitude);

            // check bottom left is left of all rights
            isValidBoundingBox = isValidBoundingBox && (this.bottomLeft.Longitude <= this.bottomRight.Longitude && this.bottomLeft.Longitude <= this.topRight.Longitude);

            // check top left is above all bottoms
            isValidBoundingBox = isValidBoundingBox && (this.topLeft.Latitude >= this.bottomLeft.Latitude && this.topLeft.Latitude >= this.bottomRight.Latitude);

            // check top right is above all bottoms
            isValidBoundingBox = isValidBoundingBox && (this.topRight.Latitude >= this.bottomRight.Latitude && this.topRight.Latitude >= this.bottomLeft.Latitude);

            return isValidBoundingBox;
        }
    }
}