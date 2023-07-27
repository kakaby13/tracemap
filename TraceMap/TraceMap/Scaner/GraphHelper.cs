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

    public static int CalculateDepth(this Node node, int currentDepth = 1)
    {
        return node.ChildrenNode.IsNullOrEmpty()
            ? currentDepth
            : node.ChildrenNode!
                .Select(c => CalculateDepth(c, currentDepth + 1))
                .Max();
    }

    private static Node JoinChildrenNodes(this Node node)
    {
        if (node.ChildrenNode.IsNullOrEmpty())
        {
            return node;
        }
        
        var gropedNodes = node.ChildrenNode!
            .GroupBy(c => c.Value)
            .ToList();

        var childrenNodes = JoinDuplicates(gropedNodes);

        var uniqueNodes = gropedNodes
            .Where(c => c.Count() == 1)
            .SelectMany(c => c.ToList());

        childrenNodes.AddRange(uniqueNodes);

        childrenNodes = childrenNodes
            .ToList()
            .Select(JoinChildrenNodes)
            .ToList();

        return new Node
        {
            Value = node.Value,
            ChildrenNode = childrenNodes
        };
    }

    private static List<Node> JoinDuplicates(IEnumerable<IGrouping<string, Node>> gropedNodes)
    {
        var result = new List<Node>();

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

            result.Add(subResult);
        }

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
}