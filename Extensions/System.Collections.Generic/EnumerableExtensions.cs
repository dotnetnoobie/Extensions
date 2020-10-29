using System.Linq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    { 
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> filterParam)
        {
            return source.Where(filterParam);
        }

        public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int start, int count)
        { 
            return source?.Skip(start).Take(count);
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        {
            return condition ? source.Where(predicate) : source; 
        }

        public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        { 
            return source.OrderBy(x => new Random().Next());
        }

        public static bool None<T>(this IEnumerable<T> source)
        {
            return source.Any() == false;
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Any(query) == false;
        }

        public static bool Many<T>(this IEnumerable<T> source)
        {
            return source.Count() > 1;
        }

        public static bool Many<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) > 1;
        }

        public static bool OneOf<T>(this IEnumerable<T> source)
        {
            return source.Count() == 1;
        }

        public static bool OneOf<T>(this IEnumerable<T> source, Func<T, bool> query)
        {
            return source.Count(query) == 1;
        }

        public static bool XOf<T>(this IEnumerable<T> source, int count)
        {
            return source.Count() == count;
        }

        public static bool XOf<T>(this IEnumerable<T> source, Func<T, bool> query, int count)
        {
            return source.Count(query) == count;
        }


        /// <summary>
        /// Continues processing items in a collection until the end condition is true.
        /// </summary>
        /// <typeparam name="T">The type of the collection.</typeparam>
        /// <param name="source">The collection to iterate.</param>
        /// <param name="endCondition">The condition that returns true if iteration should stop.</param>
        /// <returns>Iterator of sub-list.</returns>
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Predicate<T> endCondition)
        {
            return source.TakeWhile(item => !endCondition(item));
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }
    }
}
