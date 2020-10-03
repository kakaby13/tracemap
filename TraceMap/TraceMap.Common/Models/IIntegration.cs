using System.Collections.Generic;

namespace TraceMap.Common.Models
{
    public interface IIntegrator
    {
        List<string> GetRawTraces(List<string> targets);

        Vertex ParseRawTraces(List<string> rawTraces);
    }
}
