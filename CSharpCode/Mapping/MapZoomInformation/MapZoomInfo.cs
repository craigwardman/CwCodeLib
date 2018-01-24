namespace CwCodeLib.Mapping.MapZoomInformation
{
    public class MapZoomInfo
    {
        public MapZoomInfo(GeographicalPoint centrePoint, int zoomLevel)
        {
            this.CentrePoint = centrePoint;
            this.ZoomLevel = zoomLevel;
        }

        public GeographicalPoint CentrePoint { get; private set; }
        public int ZoomLevel { get; private set; }
    }
}