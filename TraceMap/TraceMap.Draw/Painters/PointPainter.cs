using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ImageMagick;
using TraceMap.Common.Models;
using TraceMap.Draw.Common;
using TraceMap.Draw.Enums;

namespace TraceMap.Draw.Painters
{
    public class PointPainter
    {
        private readonly List<Vertex> _graph;
        private readonly Dictionary<Vertex, Point> _points;
        private readonly MagickImage _image;


        public PointPainter(List<Vertex> graph, Dictionary<Vertex, Point> points, MagickImage image)
        {
            _graph = graph;
            _points = points;
            _image = image;
        }

        public void DrawPoints()
        {
            var rootPoint = _graph.Single(c => c.IsItRoot);
            GoToNextPoints(rootPoint);
        }

        private void GoToNextPoints(Vertex currentPoint, Edge previousEdge = null)
        {
            DrawPoint(currentPoint, previousEdge);
            foreach (var edge in currentPoint.GetNextEdges(previousEdge))
            {
                var nextPoint = edge.GetNextNode(currentPoint);
                GoToNextPoints(nextPoint, edge);
            }
        }

        private void DrawPoint(Vertex vertex, Edge previousEdge)
        {
            var pointColor = Constants.GetPointColor(CalculatePointType(vertex, previousEdge));
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

        private PointType CalculatePointType(Vertex vertex, Edge previousEdge)
        {
            if (previousEdge == null)
            {
                return PointType.Host;
            } else if (vertex.GetNextEdges(previousEdge).Count == 0)
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
