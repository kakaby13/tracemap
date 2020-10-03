using System.Collections.Generic;
using TraceMap.Common.Models;
using TraceMap.Draw;
using TraceMap.Integration.Integrators;

namespace TraceMap.Integration
{
    public class IntegrationRunner
    {
        public void Run(
            TargetOperatingSystem os, 
            List<string> targets,
            string outputFileName = null, 
            string fileExtension = null, 
            string outputPath = null)
        {
            var integrator = IntegrationFactory.GetIntegrator(os);
            var rawTraces = integrator.GetRawTraces(targets);
            var rootNodeOfGraph = integrator.ParseRawTraces(rawTraces);
            var painter = new Painter(rootNodeOfGraph);
            painter.Draw(outputFileName, fileExtension, outputPath);
        }
    }
}
