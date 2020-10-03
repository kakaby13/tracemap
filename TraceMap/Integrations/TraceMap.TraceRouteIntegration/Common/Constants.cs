using System.Text.RegularExpressions;

namespace TraceMap.Integration.TraceRoute.Common
{
    internal static class Constants
    {
        internal static readonly Regex IpRegex = new Regex(@"\(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\)");
        
        internal static readonly Regex PingRegex = new Regex(@"\d{1,10}\.\d{0,5}\sms");
    }
}
