using SkiaSharp;
using TraceMap.Drawning.Models;
using TraceMap.Models;
using TraceMap.Utils;

namespace TraceMap.Drawning;

public class DrawningCore : IDrawningCore
{
    private int resolutionWidth = 3840;
    
    private int resolutionHight = 2160;

    public void Draw(Node node)
    {
        using var surface = SKSurface.Create(new SKImageInfo(resolutionWidth, resolutionHight));
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);
        
        DrawTree(node, canvas);
        
        SaveToFile(surface);
    }

    private void DrawTree(Node node, SKCanvas canvas)
    {
        var depth = DrawningHelper.CalculateDepth(node);
        var treeMetaInfo = new MapTreeMetaInfo
        {
            Depth = depth,
            UnitDistance = resolutionHight / ( 2 * depth + 1 )
        };

        Console.WriteLine($"Unit distance = {treeMetaInfo.UnitDistance}");
        var nodeMetaInfo = new MapNodeMetaInfo
        {
            NodeRange = new MapNodeRange
            {
                From = 90,
                To = 270
            },
            ParentPoint = new Point
            {
                X = resolutionWidth/2 + treeMetaInfo.UnitDistance,
                Y = resolutionHight/2
            }
        };

        CalculateAndDrawNode(node, nodeMetaInfo, treeMetaInfo, canvas, false);
    }

    private void CalculateAndDrawNode(Node node, 
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
            PrintPipe(currentPoint, nodeMetaInfo.ParentPoint, canvas);
        }

        PrintNode(node, currentPoint, canvas);

        if (node.ChildrenNode.IsNullOrEmpty()) return;
        
        var anglePerChild = (nodeMetaInfo.NodeRange.To - nodeMetaInfo.NodeRange.From) / node.ChildrenNode.Count;

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
                
            CalculateAndDrawNode(
                node.ChildrenNode[nodeIndex], 
                childNodeMetaInfo,
                treeMetaInfo, 
                canvas);
        }

    }
    
    private void PrintNode(Node node, Point point, SKCanvas canvas)
    {
        Console.WriteLine($"print node {node.Value} with X = {point.X} / Y = {point.Y}");
        // draw some text
        var paint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Center,
            TextSize = 50
        };
        
        canvas.DrawText(node.Value, point.MapToSKPoint(), paint);
    }

    private void PrintPipe(Point a, Point b, SKCanvas canvas)
    {
        var paint = new SKPaint();
        canvas.DrawLine(a.MapToSKPoint(), b.MapToSKPoint(), paint);
    }

    private void SaveToFile(SKSurface surface)
    {
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite("foo.png");
        
        data.SaveTo(stream);
    }
}