using TraceMap.Models;
using TraceMap.Utils;

namespace TraceMap.Scaner;

public static class GraphHelper
{
    public static Node MapToGraph(this List<string> rawNodes)
    {
        rawNodes.Reverse();
        var currentNode = new Node();

        foreach (var rawNode in rawNodes)
        {
            currentNode.Value = rawNode;
            var newNode = new Node
            {
                ChildrenNode = new List<Node> { currentNode }
            };
            currentNode = newNode;
        }

        return currentNode;
    }

    public static Node MergeRoutes(this List<Node> nodes)
    {
        return nodes
            .MergeRoot()
            .JoinChildrenNodes();
    }

    private static Node JoinChildrenNodes(this Node node)
    {
        if (node.ChildrenNode.IsNullOrEmpty())
        {
            return node;
        }
        var result = new Node
        {
            Value = node.Value,
            ChildrenNode = new List<Node>()
        };

        var gropedNodes = node.ChildrenNode!
            .GroupBy(c => c.Value)
            .ToList();
        
        foreach (var sameValue in gropedNodes.Where(c => c.Count() > 1))
        {
            var subResult = new Node
            {
                Value = sameValue.Key,
                ChildrenNode = new List<Node>()
            };

            subResult.ChildrenNode = sameValue
                .SelectMany(c => c.ChildrenNode!)
                .ToList();

            result.ChildrenNode.Add(subResult);
        }

        var uniqueNodes = gropedNodes
            .Where(c => c.Count() == 1)
            .SelectMany(c => c.ToList());
        
        result.ChildrenNode.AddRange(uniqueNodes);
        
        var joinedChildren = result
            .ChildrenNode.Select(JoinChildrenNodes)
            .ToList();

        result.ChildrenNode = joinedChildren;

        return result;
    }

    private static Node MergeRoot(this IEnumerable<Node> nodes)
    {
        return new Node
        {
            Value = "root",
            ChildrenNode = nodes.SelectMany(c => c.ChildrenNode!).ToList()
        };
    }

    public static int CalculateDepth(this Node node, int currentDepth = 1)
    {
        return node.ChildrenNode.IsNullOrEmpty()
            ? currentDepth
            : node.ChildrenNode!
                .Select(c => CalculateDepth(c, currentDepth + 1))
                .Max();
    }
}