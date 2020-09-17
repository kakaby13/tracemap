using TraceMap.Common.Models;

namespace TraceMap.Draw.Models
{
    internal class CoordinateRestriction
    {
        public CoordinateRestriction(double minAngle, double maxAngle, Vertex vertex)
        {
            MinAngle = minAngle;
            MaxAngle = maxAngle;
            Vertex = vertex;
        }

        public double MinAngle { get; set; }

        public double MaxAngle { get; set; }

        public Vertex Vertex { get; set; }
    }
}
