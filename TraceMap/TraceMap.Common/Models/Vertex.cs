using System.Collections.Generic;

namespace TraceMap.Common.Models
{
    public class Vertex
    {
        public Vertex()
        {
            ChildVertexes = new List<Vertex>();
        }

        public string Value { get; set; }

        public Vertex ParentVertex { get; set; }

        public double DistanceToParentVertex { get; set; }

        public List<Vertex> ChildVertexes { get; set; }
    }
}
