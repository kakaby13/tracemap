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
            Value = "1.1.1",
            ChildrenNode = new List<Node>
            {
                new Node
                {
                    Value = "2333",
                    ChildrenNode = new List<Node>
                    {
                        new Node
                        {
                            Value = "2333",
                            ChildrenNode = new List<Node>
                            {
                                new Node
                                {
                                    Value = "2333",
                                    ChildrenNode = new List<Node>
                                    {
                                        new Node
                                        {
                                            Value = "2333",
                                            ChildrenNode = new List<Node>
                                            {
                                                new Node
                                                {
                                                    Value = "3111"
                                                },
                                            }
                                        },
                                    }
                                },
                                new Node
                                {
                                    Value = "2333",
                                    ChildrenNode = new List<Node>
                                    {
                                        new Node
                                        {
                                            Value = "2333",
                                            ChildrenNode = new List<Node>
                                            {
                                                new Node
                                                {
                                                    Value = "3111"
                                                },
                                            }
                                        },
                                    }
                                }
                            }
                        },
                    }
                }
            }
        };
        
        _drawningCore.Draw(node);
    }
}