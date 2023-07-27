using SkiaSharp;
using TraceMap.Drawning.Models;
using TraceMap.Models;

namespace TraceMap.Drawning;

public static class SkiaSharpHelper
{
    public static void PrintNode(Node node, Point point, SKCanvas canvas)
    {
        var paint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Center,
            TextSize = 25
        };
        
        canvas.DrawText(node.Value, point.MapToSkPoint(), paint);
    }
    
    public static void PrintPipe(Point a, Point b, SKCanvas canvas)
    {
        var paint = new SKPaint();
        canvas.DrawLine(a.MapToSkPoint(), b.MapToSkPoint(), paint);
    }
    
    public static void SaveToFile(SKSurface surface)
    {
        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite("foo.png");
        
        data.SaveTo(stream);
    }
}