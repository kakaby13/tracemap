using SkiaSharp;
using TraceMap.Drawning.Models;
using TraceMap.Models;
using TraceMap.Scaner;
using TraceMap.Utils;

namespace TraceMap.Drawning;

public class DrawningCore : IDrawningCore
{
    private const int ResolutionWidth = 3840;

    private const int ResolutionHight = 2160;

    public void Draw(Node node)
    {
        using var surface = SKSurface.Create(new SKImageInfo(ResolutionWidth, ResolutionHight));
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);
        
        DrawTree(node, canvas);
        
        SkiaSharpHelper.SaveToFile(surface);
    }

    private void DrawTree(Node node, SKCanvas canvas)
    {
        var depth = node.CalculateDepth();
        var treeMetaInfo = new MapTreeMetaInfo
        {
            Depth = depth,
            UnitDistance = ResolutionHight / (depth + 1)
        };

        var nodeMetaInfo = GetInitialNodeMetaInfo(treeMetaInfo);

        CalculateAndDrawNode(node, nodeMetaInfo, treeMetaInfo, canvas, false);
    }

    private void CalculateAndDrawNode(
        Node node,
        MapNodeMetaInfo nodeMetaInfo,
        MapTreeMetaInfo treeMetaInfo,
        SKCanvas canvas,
        bool printPipe = true)
    {
        var currentPoint = DrawningHelper.CalculateNextPoint(
            nodeMetaInfo.ParentPoint,
            treeMetaInfo,
            nodeMetaInfo);

        if (printPipe)
        {
            SkiaSharpHelper.PrintPipe(currentPoint, nodeMetaInfo.ParentPoint, canvas);
        }

        SkiaSharpHelper.PrintNode(node, currentPoint, canvas);

        if (!node.ChildrenNode.IsNullOrEmpty())
        {
            ProcessChildrenNodes(node, nodeMetaInfo, treeMetaInfo, canvas, currentPoint);
        }
    }

    private void ProcessChildrenNodes(
        Node node, 
        MapNodeMetaInfo nodeMetaInfo, 
        MapTreeMetaInfo treeMetaInfo, 
        SKCanvas canvas,
        Point currentPoint)
    {
        var anglePerChild = (nodeMetaInfo.NodeRange.To - nodeMetaInfo.NodeRange.From) / node.ChildrenNode!.Count;

        for (var nodeIndex = 0; nodeIndex < node.ChildrenNode.Count; nodeIndex++)
        {
            var childAngleFrom = nodeMetaInfo.NodeRange.From + anglePerChild * nodeIndex;

            var childNodeMetaInfo = new MapNodeMetaInfo
            {
                ParentPoint = currentPoint,
                NodeRange = new MapNodeRange
                {
                    From = childAngleFrom,
                    To = childAngleFrom + anglePerChild
                }
            };

            CalculateAndDrawNode(node.ChildrenNode[nodeIndex], childNodeMetaInfo, treeMetaInfo, canvas);
        }
    }

    private MapNodeMetaInfo GetInitialNodeMetaInfo(MapTreeMetaInfo treeMetaInfo)
    {
        return new MapNodeMetaInfo
        {
            NodeRange = new MapNodeRange
            {
                From = 0,
                To = 180
            },
            ParentPoint = new Point
            {
                X = ResolutionWidth / 2 + treeMetaInfo.UnitDistance,
                Y = treeMetaInfo.UnitDistance
            }
        };
    }
}