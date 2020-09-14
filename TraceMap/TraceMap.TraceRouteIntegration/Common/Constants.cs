using System.Text.RegularExpressions;

namespace TraceMap.TraceRouteIntegration.Common
{
    public static class Constants
    {
        public static readonly Regex IpRegex = new Regex(@"\(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\)");
        
        public static readonly Regex PingRegex = new Regex(@"\d{1,10}\.\d{0,5}\sms");

    }
}
