namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            if (items == null)
            {
                //Diagnostics.Debug.WriteLine("Do extension metody AddRange byly poslany items == null");
                return;
            }

            foreach (var item in items )
            {
                source.Add(item);
            }
        }
    }
}
