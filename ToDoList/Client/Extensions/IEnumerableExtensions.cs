namespace ToDoList.Client.Extensions
{
    public static class IEnumerableExtensions
    {
        public static int CountOrDefault<T>(this IEnumerable<T>? elements)
            => elements?.Count() ?? default;
    }
}
