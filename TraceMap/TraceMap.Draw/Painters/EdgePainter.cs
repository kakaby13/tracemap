using ImageMagick;
using System.Collections.Generic;
using System.Drawing;
using TraceMap.Common.Models;

namespace TraceMap.Draw.Painters
{
    public class EdgePainter
    {
        private readonly Dictionary<Vertex, Point> _points;
        private readonly MagickImage _image;

        public EdgePainter(Dictionary<Vertex, Point> points, MagickImage image)
        {
            _image = image;
            _points = points;
        }

        public void DrawEdges(Vertex rootVertex)
        {
            GoToNextEdge(rootVertex);
        }

        private void GoToNextEdge(Vertex currentPoint)
        {
            foreach (var childPoint in currentPoint.ChildVertexes)
            {
                DrawEdge(currentPoint, childPoint);
                GoToNextEdge(childPoint);
            }
        }

        private void DrawEdge(Vertex startVertex, Vertex endVertex)
        {
            var pointA = _points[startVertex];
            var pointB = _points[endVertex];

            new Drawables()
                .Line(pointA.X, pointA.Y, pointB.X, pointB.Y)
                .Draw(_image);
        }
    }
}
