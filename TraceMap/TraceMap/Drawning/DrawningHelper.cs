using SkiaSharp;
using TraceMap.Drawning.Models;
using Point = TraceMap.Drawning.Models.Point;

namespace TraceMap.Drawning;

public static class DrawningHelper
{
    public static Point CalculateNextPoint(Point startPoint, MapTreeMetaInfo treeMetaInfo, MapNodeMetaInfo nodeMetaInfo)
    {
        var angleStep = (nodeMetaInfo.NodeRange.To - nodeMetaInfo.NodeRange.From) / 2;
        var angle = nodeMetaInfo.NodeRange.From + angleStep;
        
        return new Point
        {
            X = Math.Round(startPoint.X + Math.Cos(angle * Math.PI / 180) * treeMetaInfo.UnitDistance),
            Y = Math.Round(startPoint.Y + Math.Sin(angle * Math.PI / 180) * treeMetaInfo.UnitDistance),
        };
    }

    public static SKPoint MapToSkPoint(this Point point)
    {
        return new SKPoint((float)point.X, (float)point.Y);
    }
}