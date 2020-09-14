using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TraceMap.Common.Helpers;
using TraceMap.Common.Models;
using TraceMap.Draw.Common;
using TraceMap.Draw.Painters;

namespace TraceMap.Draw
{
    public class Painter
    {
        private readonly List<Vertex> _graph;
        private int _width;
        private int _height;
        private Dictionary<Vertex, Point> _points;

        public Painter(List<Vertex> graph)
        {
            _graph = graph;
            CalculatePointsCoordinates();
            CalculateImageSize();
        }

        public void Draw()
        {
            using (var image = new MagickImage(new MagickColor(MagickColors.White), _width*10, _height*10))
            {
                new EdgePainter(_graph, _points, image).DrawEdges();
                new PointPainter(_graph, _points, image).DrawPoints();
                image.Write(new FileInfo(Constants.DefaultFileName));
            }
        }

        private void CalculatePointsCoordinates()
        {
            StabCoordinates();
            //todo write some magic code here G.Model
        }

        private void CalculateImageSize()
        {
            var graphHelper = new GraphHelper();
            var maxChain = graphHelper.MahChainLength(_graph) * 2;
            _height = _width = (maxChain + 2) * 75;
        }

        [Obsolete]
        private void StabCoordinates()
        {
            _points = new Dictionary<Vertex, Point>
            {
                {_graph.First(), new Point(50, 50)},
                {_graph.Skip(1).First(), new Point(150, 150)},
                {_graph.Skip(2).First(), new Point(300, 200)},
                {_graph.Skip(3).First(), new Point(500, 150)},
                {_graph.Last(), new Point(450, 450)}
            };
        }
    }
}
