using SkiaSharp;
using TraceMap.Drawning.Models;
using TraceMap.Models;

namespace TraceMap.Drawning;

public static class SkiaSharpHelper
{
    public static void PrintNode(Node node, Point point, SKCanvas canvas)
    {
        Console.WriteLine($"print node {node.Value} with X = {point.X} / Y = {point.Y}");
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
    
    public static void PrintPipe(Point a, Point b, SKCanvas canvas)
    {
        var paint = new SKPaint();
        canvas.DrawLine(a.MapToSKPoint(), b.MapToSKPoint(), paint);
    }
    
    public static void SaveToFile(SKSurface surface)
    {
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite("foo.png");
        
        data.SaveTo(stream);
    }
}