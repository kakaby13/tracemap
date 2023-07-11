using SkiaSharp;

namespace TraceMap.Drawning;

public class DrawningCore : IDrawningCore
{
    public void Draw()
    {
        // crate a surface
        var info = new SKImageInfo(3840, 2160);
        using var surface = SKSurface.Create(info);
        // the the canvas and properties
        var canvas = surface.Canvas;

        // make sure the canvas is blank
        canvas.Clear(SKColors.White);

        // draw some text
        var paint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Center,
            TextSize = 24
        };
        var coord = new SKPoint(info.Width / 2, (info.Height + paint.TextSize) / 2);
        canvas.DrawText("SkiaSharp", coord, paint);

        // save the file
        using (var image = surface.Snapshot())
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        using (var stream = File.OpenWrite("output.png"))
        {
            data.SaveTo(stream);
        }
    }
}