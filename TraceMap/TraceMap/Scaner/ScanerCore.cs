using System.Net;
using System.Net.NetworkInformation;

namespace TraceMap.Scaner;

public class ScanerCore
{
    public List<string> Scan(string hostname)
    {
        var result = new List<IPAddress>();
        const int timeout = 10000;
        const int maxHopCount = 255;
        
        using var pinger = new Ping();
        for (var timeToLive = 1; timeToLive <= maxHopCount; timeToLive++)
        {
            var options = new PingOptions(timeToLive, true);
            var reply = pinger.Send(hostname, timeout, GetRandomContent(), options);

            if (reply.Status is IPStatus.Success or IPStatus.TtlExpired)
            {
                result.Add(reply.Address);
            }

            if (reply.Status != IPStatus.TtlExpired && reply.Status != IPStatus.TimedOut)
            {
                break;
            }
        }

        return result
            .Select(c => c.ToString())
            .ToList();
    }

    private static byte[] GetRandomContent()
    {
        const int bufferSize = 255;
        var buffer = new byte[bufferSize];
        new Random().NextBytes(buffer);

        return buffer;
    }
}