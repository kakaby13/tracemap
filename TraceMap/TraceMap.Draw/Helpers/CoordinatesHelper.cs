using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TraceMap.Common.Models;
using TraceMap.Draw.Common;
using TraceMap.Draw.Models;

namespace TraceMap.Draw.Helpers
{
    internal class CoordinatesHelper
    {
        private Dictionary<Vertex, Point> _points;

        public Dictionary<Vertex, Point> CalculatePointsCoordinates(Vertex rootVertex, int imageSize)
        {
            _points = new Dictionary<Vertex, Point>
            {
                {
                    rootVertex,
                    new Point(Convert.ToInt32(imageSize / 2), Convert.ToInt32(imageSize / 2))
                }
            };
            var coordinateRestriction = new CoordinateRestriction(0, 2 * Math.PI, rootVertex);
            CalculateChildrenForThisNode(coordinateRestriction);

            return _points;
        }


        private void CalculateChildrenForThisNode(CoordinateRestriction coordinateRestriction)
        {
            var children = coordinateRestriction.Vertex.ChildVertexes.ToList();
            if (children.Count == 0)
            {
                return;
            }

            var vacantAngleForEachChild =
                (coordinateRestriction.MaxAngle - coordinateRestriction.MinAngle) / children.Count;
            var currentChildMinAngle = coordinateRestriction.MinAngle;

            foreach (var child in children)
            {
                var childMinAngle = currentChildMinAngle;
                var childMaxAngle = childMinAngle + vacantAngleForEachChild;
                var thisChildDirectionAngle = (childMinAngle + childMaxAngle) / 2;
                var childCoo = new CoordinateRestriction(childMinAngle, childMaxAngle, child);
                _points.Add(child,
                    CalculateNewPointLocation(child.DistanceToParentVertex, thisChildDirectionAngle,
                        _points[coordinateRestriction.Vertex]));
                CalculateChildrenForThisNode(childCoo);
                currentChildMinAngle += vacantAngleForEachChild;
            }

        }

        private static Point CalculateNewPointLocation(double radius, double angle, Point parentPoint)
        {
            radius *= Constants.ScalingRadiusConstant;

            var x = Convert.ToInt32(radius * Math.Cos(angle));
            var y = Convert.ToInt32(radius * Math.Sin(angle));

            return new Point(parentPoint.X + x, parentPoint.Y + y);
        }
    }
}
