using System;
using System.Collections.Generic;
using TraceMap.Common.Models;
using TraceMap.Draw;

namespace TraceMap.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var painter = new Painter(GenerateStabData());
            painter.Draw();
        }

        [Obsolete]
        private static List<Vertex> GenerateStabData()
        {
            var v1 = new Vertex("loruum ipsum 1", true);
            var v2 = new Vertex("loruum ipsum 2");
            var v3 = new Vertex("loruum ipsum 3");
            var v4 = new Vertex("loruum ipsum 4");
            var v5 = new Vertex("loruum ipsum 5");
            new Edge(v1, v2);
            new Edge(v2, v3);
            new Edge(v3, v4);
            new Edge(v3, v5);

            return new List<Vertex> {v1, v2, v3, v4, v5};
        }
    }
}
