using System;
using Mapping.Domain.Services.MapZoomInfoCalculator.ZoomLevelCalculator;

namespace CwCodeLib.Mapping.MapZoomInformation
{
    internal class MapZoomInfoCalculatorService
    {
        private IZoomLevelCalculator zoomLevelCalculator;

        public MapZoomInfoCalculatorService()
            : this(new BingMapsZoomLevelCalculator())
        {
        }

        public MapZoomInfoCalculatorService(IZoomLevelCalculator zoomLevelCalculator)
        {
            if (zoomLevelCalculator == null)
            {
                throw new ArgumentNullException(nameof(zoomLevelCalculator));
            }

            this.zoomLevelCalculator = zoomLevelCalculator;
        }

        public MapZoomInfo CalculateMapZoomInfo(BoundingBox boundingBox, int viewingWidthPx)
        {
            GeographicalPoint centrePoint = this.GetCenterPoint(boundingBox);
            int zoomLevel = this.zoomLevelCalculator.GetZoomLevel(boundingBox, viewingWidthPx);

            return new MapZoomInfo(centrePoint, zoomLevel);
        }

        private GeographicalPoint GetCenterPoint(BoundingBox boundingBox)
        {
            return new GeographicalPoint((boundingBox.BottomLeft.Latitude + boundingBox.TopLeft.Latitude) / 2, (boundingBox.BottomLeft.Longitude + boundingBox.BottomRight.Longitude) / 2);
        }
    }
}