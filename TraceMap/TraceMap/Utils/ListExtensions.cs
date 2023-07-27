namespace TraceMap.Utils;

public static class ListExtensions
{
    public static bool IsNullOrEmpty<T>(this List<T>? list)
    {
        return list is null || list.Count == 0;
    }
}