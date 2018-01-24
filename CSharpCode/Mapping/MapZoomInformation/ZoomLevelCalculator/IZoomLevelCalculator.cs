namespace CwCodeLib.Mapping.MapZoomInformation.ZoomLevelCalculator
{
    internal interface IZoomLevelCalculator
    {
        int GetZoomLevel(BoundingBox boundingBox, int viewingWidthPx);
    }
}