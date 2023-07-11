using TraceMap.Drawning;
using TraceMap.Models;

namespace TraceMap;

public class TraceMap
{
    private readonly IDrawningCore _drawningCore;

    public TraceMap(IDrawningCore drawningCore)
    {
        _drawningCore = drawningCore;
    }

    public void Run()
    {
        var node = new Node
        {
            ChildrenNode = new List<Node>
            {
                new Node(), 
                new Node(), 
                new Node
                {
                    ChildrenNode = new List<Node>
                    {
                        new Node()
                    }
                }
            }
        };
        
        _drawningCore.Draw(node);
    }
}