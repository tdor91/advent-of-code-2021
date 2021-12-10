public static class ListExtensions
{
    public static string ExtractSingle(this IList<string> source, Func<string, bool> predicate)
    {
        var element = source.Single(predicate);
        source.Remove(element);

        return element;
    }

    public static string ExtractFirst(this IList<string> source, Func<string, bool> predicate)
    {
        var element = source.First(predicate);
        source.Remove(element);

        return element;
    }
}