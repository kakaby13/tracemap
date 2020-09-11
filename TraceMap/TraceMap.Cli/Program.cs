using System;
using TraceMap.Common.Models;

namespace TraceMap.Cli
{
    class Program
    {
        static void Main(string[] args)
        {

            var v1 = new Vertex("1");
            var v2 = new Vertex("2");

            var e1 = new Edge(v1, v2);

            Console.WriteLine("Hello World!");

        }
    }
}
