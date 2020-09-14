using System.Collections.Generic;
using System.Linq;

namespace TraceMap.Common.Models
{
    public class Edge
    {
        public double Value { get; set; }

        public List<Vertex> Vertices { get; }

        public Edge(Vertex firstVertex, Vertex secondVertex)
        {
            Vertices = new List<Vertex> { firstVertex, secondVertex };
            firstVertex.AddEdge(this);
            secondVertex.AddEdge(this);
        }

        public Vertex GetNextNode(Vertex currentVertex)
        {
            return Vertices.First().Equals(currentVertex) ? Vertices.Last() : Vertices.First();
        }
    }
}
