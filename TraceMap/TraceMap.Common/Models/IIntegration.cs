using System.Collections.Generic;
using TraceMap.Integration.Common.Models;

namespace TraceMap.Common.Models
{
    public interface IIntegrator
    {
        List<TraceInfo> GetTraces(List<string> targets);

        Vertex ParseRawTraces(List<TraceInfo> rawTraces);
    }
}
