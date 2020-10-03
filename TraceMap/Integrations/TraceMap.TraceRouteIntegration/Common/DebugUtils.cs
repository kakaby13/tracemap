namespace TraceMap.Integration.TraceRoute.Common
{
    internal static class DebugUtils
    {
        internal static string TutBy = @"traceroute to tut.by (178.172.160.5), 30 hops max, 60 byte packets
 1  _gateway (192.168.8.1)  0.941 ms  1.172 ms  1.478 ms
 2  192.168.100.1 (192.168.100.1)  2.151 ms  2.506 ms  2.781 ms
 3  mm-1-128-84-93.dynamic.pppoe.mgts.by (93.84.128.1)  5.254 ms  5.367 ms  5.670 ms
 4  mm-53-80-84-93.dynamic.pppoe.mgts.by (93.84.80.53)  8.354 ms mm-49-80-84-93.dynamic.pppoe.mgts.by (93.84.80.49)  4.670 ms  4.254 ms
 5  core2.net.belpak.by (93.85.80.57)  9.002 ms core1.net.belpak.by (93.85.80.45)  7.415 ms core2.net.belpak.by (93.85.80.57)  9.309 ms
 6  93.84.125.189 (93.84.125.189)  6.381 ms 93.84.125.193 (93.84.125.193)  3.602 ms  3.607 ms
 7  178.124.134.165 (178.124.134.165)  7.370 ms  7.029 ms  6.764 ms
 8  178-172-160-5.hosterby.com (178.172.160.5)  3.842 ms  4.095 ms  4.706 ms";


        internal static string Google = @"traceroute to google.com (216.58.209.14), 30 hops max, 60 byte packets
 1  _gateway (192.168.8.1)  1.552 ms  1.819 ms  2.073 ms
 2  192.168.100.1 (192.168.100.1)  2.382 ms  3.527 ms  3.810 ms
 3  mm-1-128-84-93.dynamic.pppoe.mgts.by (93.84.128.1)  6.309 ms  6.549 ms  6.748 ms
 4  mm-49-80-84-93.dynamic.pppoe.mgts.by (93.84.80.49)  9.058 ms mm-53-80-84-93.dynamic.pppoe.mgts.by (93.84.80.53)  7.073 ms  7.990 ms
 5  core2.net.belpak.by (93.85.80.57)  12.415 ms core1.net.belpak.by (93.85.80.45)  7.360 ms  7.647 ms
 6  ie2.net.belpak.by (93.85.80.42)  13.956 ms  7.893 ms  10.957 ms
 7  asbr9.net.belpak.by (93.85.80.242)  5.676 ms  5.363 ms  5.963 ms
 8  74.125.146.96 (74.125.146.96)  16.336 ms  15.314 ms  15.046 ms
 9  108.170.250.209 (108.170.250.209)  13.357 ms  14.222 ms  13.043 ms
10  172.253.68.31 (172.253.68.31)  15.335 ms 172.253.68.29 (172.253.68.29)  13.622 ms  13.898 ms
11  sof01s12-in-f14.1e100.net (216.58.209.14)  11.515 ms  11.961 ms  14.343 ms";
    }
}
