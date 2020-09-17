using ImageMagick;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TraceMap.Common.Helpers;
using TraceMap.Common.Models;
using TraceMap.Draw.Common;
using TraceMap.Draw.Helpers;
using TraceMap.Draw.Painters;

namespace TraceMap.Draw
{
    public class Painter
    {
        private readonly Vertex _rootVertex;
        private readonly int _imageSize;
        private readonly Dictionary<Vertex, Point> _points;

        public Painter(Vertex rootVertex)
        {
            _rootVertex = rootVertex;
            new GraphReductionHelper().ReduceGraph(_rootVertex);
            _imageSize = CalculateImageSize();
            _points = new CoordinatesHelper().CalculatePointsCoordinates(_rootVertex, _imageSize);
        }

        public void Draw(string outputFileName = null, string fileExtension = null, string outputPath = null)
        {
            var outputFileFullName 
                = $"{outputFileName ?? Constants.DefaultFileName}.{fileExtension ?? Constants.DefaultFileExtension}";
            var pathToImage = outputPath == null ? outputFileFullName : Path.Combine(outputPath, outputFileFullName);

            using (var image = new MagickImage(new MagickColor(MagickColors.White), _imageSize, _imageSize))
            {
                new EdgePainter(_points, image).DrawEdges(_rootVertex);
                new PointPainter(_points, image).DrawPoints(_rootVertex);
                image.Write(pathToImage);
            }
        }
        
        private int CalculateImageSize()
        {
            var graphHelper = new GraphHelper();
            var maxChain = graphHelper.MahChainLength(_rootVertex) * 2;
            return (maxChain + 2) * 75 *10;
        }
    }
}
