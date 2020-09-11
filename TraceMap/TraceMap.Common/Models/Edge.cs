using System.Collections.Generic;

namespace TraceMap.Common.Models
{
    public class Edge
    {
        public List<Vertex> Vertices { get; }
        public Edge(Vertex firstVertex, Vertex secondVertex)
        {
            Vertices = new List<Vertex> { firstVertex, secondVertex };
            firstVertex.AddEdge(this);
            secondVertex.AddEdge(this);
        }
    }
}
