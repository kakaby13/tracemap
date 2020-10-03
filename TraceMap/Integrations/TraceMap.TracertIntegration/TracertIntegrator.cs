using System.Collections.Generic;
using System.Linq;
using TraceMap.Common.Models;
using TraceMap.Integration.Common.Models;
using TraceMap.Integration.Tracert.Helpers;

namespace TraceMap.Integration.Tracert
{
    public class TracertIntegrator : IIntegrator
    {
        public List<string> GetTraces(List<string> targets)
        {
            var traces = new List<string>();
            traces.AddRange(GetRawTraces(targets));

            return traces;
        }

        public Vertex ParseRawTraces(List<string> rawTraces)
        {
            var hostNode = new Vertex { Value = "You" };
            var rawGraph = rawTraces.Select(response => BuildBranch(response, hostNode)).ToList();

            return CompileMap(rawGraph, hostNode).ToList().Single(c => c.ParentVertex == null);
        }

        private List<Vertex> BuildBranch(string response, Vertex hostNode)
        {
            var result = new List<Vertex>();
            var allLines = response.Split('\n');

            if (allLines.Length < 2)
            {
                return null;
            }

            var lines = allLines.Skip(9).Take(allLines.Length - 13).ToList();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Contains("* * *"))
                {
                    continue;
                }
                var intervalInfo = ParseTraceRouteLine(line);
                var node = new Vertex
                {
                    Value = intervalInfo.NodeName,
                    DistanceToParentVertex = 1, //TODO: implement scaling and then use: intervalInfo.Distance,
                    ParentVertex = result.Count == 0 ? hostNode : result.Last()
                };
                node.ParentVertex.ChildVertexes.Add(node);

                result.Add(node);
            }

            return result;
        }

        private static TraceMapIntervalInfo ParseTraceRouteLine(string line)
        {
            return new TraceMapIntervalInfo
            {
                NodeName = ParseHelper.ParseIp(line),
                Distance = ParseHelper.ParsePing(line),
            };
        }

        private static IEnumerable<string> GetRawTraces(IEnumerable<string> targets)
        {
            return targets.Select(target => $"tracert {target}".Cmd()).ToList();
        }

        private static IEnumerable<Vertex> CompileMap(IEnumerable<List<Vertex>> amountOfTraces, Vertex rootNode)
        {
            var result = new List<Vertex> { rootNode };
            foreach (var trace in amountOfTraces)
            {
                result.AddRange(trace);
            }

            return result;
        }
    }
}
