using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            CalculateImageSize();
            CalculatePointsCoordinates();
        }

        public void Draw()
        {
            using (var image = new MagickImage(new MagickColor(MagickColors.White), _width, _height))
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
            _height = _width = (maxChain + 2) * 75 *10;
        }

        [Obsolete]
        private void StabCoordinates()
        {
            _points = new Dictionary<Vertex, Point>();
            var random = new Random();
            foreach (var vertex in _graph)
            {
                _points.Add(vertex, new Point(random.Next(0, _width), random.Next(0, _height)));
            }
        }
    }
}
