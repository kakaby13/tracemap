using System.Collections.Generic;

namespace TraceMap.Common.Models
{
    public class Vertex
    {
        public Vertex(string value)
        {
            Value = value;
            Edges = new List<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public List<Edge> Edges { get; }
        public string Value { get; }
    }
}
