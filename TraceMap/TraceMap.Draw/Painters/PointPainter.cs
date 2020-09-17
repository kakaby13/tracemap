using ImageMagick;
using System.Collections.Generic;
using System.Drawing;
using TraceMap.Common.Models;
using TraceMap.Draw.Common;
using TraceMap.Draw.Enums;

namespace TraceMap.Draw.Painters
{
    public class PointPainter
    {
        private readonly Dictionary<Vertex, Point> _points;
        private readonly MagickImage _image;


        public PointPainter(Dictionary<Vertex, Point> points, MagickImage image)
        {
            _points = points;
            _image = image;
        }

        public void DrawPoints(Vertex rootPoint)
        {
            GoToNextPoints(rootPoint);
        }

        private void GoToNextPoints(Vertex currentPoint)
        {
            DrawPoint(currentPoint);
            foreach (var childVertex in currentPoint.ChildVertexes)
            {
                GoToNextPoints(childVertex);
            }
        }

        private void DrawPoint(Vertex vertex)
        {
            var pointColor = Constants.GetPointColor(CalculatePointType(vertex));
            var currentPoint = _points[vertex];

            new Drawables()
                .Circle(currentPoint.X, 
                        currentPoint.Y, 
                        currentPoint.X - Constants.CircleRadius,
                        currentPoint.Y - Constants.CircleRadius)
                .FillColor(pointColor)
                .Draw(_image);

            new Drawables()
                .FontPointSize(Constants.FontSize)
                .StrokeColor(Constants.TextColor)
                .TextAlignment(TextAlignment.Center)
                .Text(currentPoint.X, currentPoint.Y, vertex.Value)
                .Draw(_image);
        }

        private static PointType CalculatePointType(Vertex vertex)
        {
            if (vertex.ParentVertex == null)
            {
                return PointType.Host;
            } else if (vertex.ChildVertexes.Count == 0)
            {
                return PointType.Target;
            }
            else
            {
                return PointType.Common;
            }
        }
    }
}
