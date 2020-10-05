namespace TraceMap.Integration.Common.Models
{
    public class TraceInfo
    {
        public TraceInfo(string target, string rawTrace)
        {
            Target = target;
            RawTrace = rawTrace;
        }

        public string Target { get; }

        public string RawTrace { get; }
    }
}
