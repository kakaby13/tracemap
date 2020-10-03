using System.Collections.Generic;

namespace TraceMap.Common.Models
{
    public interface IIntegrator
    {
        List<string> GetTraces(List<string> targets);

        Vertex ParseRawTraces(List<string> rawTraces);
    }
}
