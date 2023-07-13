using TraceMap.Drawning.Models;
using TraceMap.Models;
using TraceMap.Utils;
using Point = TraceMap.Drawning.Models.Point;

namespace TraceMap.Drawning;

public static class DrawningCalculationHelper
{
    public static int CalculateDepth(Node node, int currentDepth = 1)
    {
        return node.ChildrenNode.IsNullOrEmpty()
            ? currentDepth
            : node.ChildrenNode
                .Select(c => CalculateDepth(c, currentDepth + 1))
                .Max();
    }

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
}