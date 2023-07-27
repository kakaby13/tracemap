using TraceMap.Models;
using TraceMap.Scaner;
using TraceMap.Drawning;

namespace TraceMap;

public class TraceMap
{
    private readonly ScanerCore _scanerCore;
    private readonly IDrawningCore _drawningCore;

    public TraceMap(ScanerCore scanerCore, IDrawningCore drawningCore)
    {
        _scanerCore = scanerCore;
        _drawningCore = drawningCore;
    }
    
    public void Run()
    {
        var result = ScanRange(new List<string> { "google.com", "youtube.com" });
        _drawningCore.Draw(result);
    }

    private Node ScanRange(IEnumerable<string> hosts)
    {
        return hosts
            .Distinct()
            .Select(host => _scanerCore
                .Scan(host)
                .MapToGraph())
            .ToList()
            .MergeRoutes();
    }
}