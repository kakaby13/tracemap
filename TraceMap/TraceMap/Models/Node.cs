namespace TraceMap.Models;

public class Node
{
    public string Value { get; set; }
    
    public List<Node>? ChildrenNode { get; set; }
}