using TraceMap.Drawning;

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
        _drawningCore.Draw();
    }
}