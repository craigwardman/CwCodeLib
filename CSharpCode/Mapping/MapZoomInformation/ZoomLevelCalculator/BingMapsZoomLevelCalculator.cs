using System;

namespace CwCodeLib.Mapping.MapZoomInformation.ZoomLevelCalculator
{
    internal class BingMapsZoomLevelCalculator : IZoomLevelCalculator
    {
        private const double MsMagicNumber = 156543.04;  // this number was defined by MS for map scale calculations (based on diameter of earth)

        public int GetZoomLevel(BoundingBox boundingBox, int viewingWidthPx)
        {
            // declare the variables required for calculating the zoom level
            double distanceInKm;
            int zoomLevel, zoomLevelPower, targetScale;

            // calculate the distance between the points
            distanceInKm = new DistanceCalculator().DistanceInKm(boundingBox.TopLeft, boundingBox.BottomRight);

            if (distanceInKm != 0)
            {
                targetScale = (int)(distanceInKm * 1000) / viewingWidthPx;    // km->m / viewing area

                if (targetScale == 0)
                {
                    targetScale = 1;
                }

                // Microsoft's equation:
                // Map resolution = 156543.04 meters/pixel * cos(latitude) / (2 ^ zoomlevel)
                // scale=ms_magic_number*Math.cos(((maxLat+minLat)/2)*Math.PI/180)/Math.pow(2, zoomLevel)
                // .'.
                zoomLevelPower = (int)(MsMagicNumber * Math.Cos(((boundingBox.TopLeft.Latitude + boundingBox.BottomRight.Latitude) / 2) * Math.PI / 180) / targetScale);

                // now the zoomLevelPower contains (2 ^ zoomLevel), so unwind this info using bit shifting, counting the powers of 2
                zoomLevel = 0;
                while (zoomLevelPower > 1)
                {
                    zoomLevel += 1;
                    zoomLevelPower = zoomLevelPower >> 1;
                }
            }
            else
            {
                // only one site, or very close together, maximum zoom
                zoomLevel = 19;
            }

            // step back
            zoomLevel -= 1;

            return zoomLevel;
        }
    }
}