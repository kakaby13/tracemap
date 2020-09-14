using System;
using System.Collections.Generic;
using System.Linq;
using TraceMap.Common.Models;
using TraceMap.Draw;
using TraceMap.TraceRouteIntegration;

namespace TraceMap.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = new List<string> {"tut.by", "google.com", "vk.com"};
            var cmdRunner = new TraceRouteExecutor();
            var result = cmdRunner.Run(data.ToList());
            new Painter(result).Draw();
        }

        private static List<Vertex> GenerateRandomGraph()
        {
            var vertexes = new List<Vertex> { new Vertex($"loruum ipsum 1", true) };
            var rand = new Random();

            for (var i = 1; i < rand.Next(100); i++)
            {
                var v = new Vertex($"loruum ipsum {i}");
                var edge = new Edge(v, vertexes[rand.Next(0, i)])
                {
                    Value = rand.NextDouble() * 5
                };
            }

            return vertexes;
        }
    }
}
