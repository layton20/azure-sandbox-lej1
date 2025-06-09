namespace Sandbox.API.Extensions;

public static class ListExtensions
{
    public static bool IsEmpty<T>(this List<T> list)
    {
        return list == null || list.Count == 0;
    }
}