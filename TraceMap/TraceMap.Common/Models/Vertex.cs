using System.Collections.Generic;
using System.Linq;

namespace TraceMap.Common.Models
{
    public class Vertex
    {
        public string Value { get; }

        public bool IsItRoot { get; }

        public List<Edge> Edges { get; }

        public Vertex(string value, bool isItRoot = false)
        {
            Value = value;
            Edges = new List<Edge>();
            IsItRoot = isItRoot;
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public List<Edge> GetNextEdges(Edge previousEdge)
        {
            return Edges.Except(new[] {previousEdge}).ToList();
        }
    }
}
