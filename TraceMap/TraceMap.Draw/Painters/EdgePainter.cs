using ImageMagick;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TraceMap.Common.Models;

namespace TraceMap.Draw.Painters
{
    public class EdgePainter
    {
        private readonly List<Vertex> _graph;
        private readonly Dictionary<Vertex, Point> _points;
        private readonly MagickImage _image;

        public EdgePainter(List<Vertex> graph, Dictionary<Vertex, Point> points, MagickImage image)
        {
            _image = image;
            _points = points;
            _graph = graph;
        }

        public void DrawEdges()
        {
            var rootVertex = _graph.Single(c => c.IsItRoot);
            GoToNextEdge(rootVertex);
        }

        public void GoToNextEdge(Vertex currentPoint, Edge previousEdge = null)
        {
            foreach (var edge in currentPoint.GetNextEdges(previousEdge))
            {
                DrawEdge(edge);
                GoToNextEdge(edge.GetNextNode(currentPoint), edge);
            }
        }

        private void DrawEdge(Edge edge)
        {
            var pointA = _points[edge.Vertices.First()];
            var pointB = _points[edge.Vertices.Last()];


            new Drawables()
                .Line(pointA.X, pointA.Y, pointB.X, pointB.Y)
                .Draw(_image);
        }
    }
}
