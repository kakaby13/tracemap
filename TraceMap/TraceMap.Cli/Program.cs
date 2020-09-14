using System.Linq;
using TraceMap.Draw;
using TraceMap.TraceRouteIntegration;

namespace TraceMap.Cli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cmdRunner = new TraceRouteExecutor();
            var result = cmdRunner.Run(args.ToList());
            new Painter(result).Draw();
        }
    }
}
