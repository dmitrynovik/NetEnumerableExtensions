using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NetEnumerableExtensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            return items == null ? new HashSet<T>() : new HashSet<T>(items);
        }

        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> items)
        {
            return items == null ? new SortedSet<T>() : new SortedSet<T>(items);
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> items)
        {
            return items == null ? new Stack<T>() : new Stack<T>(items);
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> items)
        {
            return items == null ? new Queue<T>() : new Queue<T>(items);
        }

        public static ConcurrentQueue<T> AsConcurrent<T>(this Queue<T> queue)
        {
            return queue == null ? new ConcurrentQueue<T>() : new ConcurrentQueue<T>(queue);
        }

        public static ConcurrentStack<T> AsConcurrent<T>(this Stack<T> stack)
        {
            return stack == null ? new ConcurrentStack<T>() : new ConcurrentStack<T>(stack);
        }

        public static ConcurrentBag<T> AsConcurrent<T>(this IEnumerable<T> items)
        {
            return items == null ? new ConcurrentBag<T>() : new ConcurrentBag<T>(items);
        }

        public static ConcurrentDictionary<TKey, TVal> AsConcurrent<TKey, TVal>(this IDictionary<TKey, TVal> input)
        {
            return input == null ? new ConcurrentDictionary<TKey, TVal>() : new ConcurrentDictionary<TKey, TVal>(input);
        }

        public static SortedDictionary<TKey, TVal> AsSorted<TKey, TVal>(this IDictionary<TKey, TVal> input)
        {
            return input == null ? new SortedDictionary<TKey, TVal>() : new SortedDictionary<TKey, TVal>(input);
        }

        /// <summary>
        /// Similar to List.ForEach except it can be used on any iterator. 
        /// 
        /// USAGE
        /// 
        /// items.Each(x =>
        /// {
        ///     Do smth.                   
        /// });
        /// </summary>
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items != null && action != null)
                foreach (var i in items)
                    action(i);
        }

        /// <summary>Similar to List.AddRange except it can be used on any collection.</summary>
        public static void AddMany<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection == null || items == null)
                return;

            items.Each(collection.Add);
        }

        /// <summary>NULL-safe selector identical to Enumerable.Where</summary>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> collection, Func<T, bool> selector)
        {
            if (collection == null)
                return Enumerable.Empty<T>();
            if (selector == null)
                return collection;

            return collection.Where(selector);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, Func<T, bool> selector)
        {
            return Filter(collection, x => !selector(x));
        }

        /// <summary>
        /// Returns default value if key not found in the dictionary rather than throwing exception.
        /// </summary>
        public static TVal GetOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key)
        {
            if (dictionary == null || key == null)
                return default(TVal);

            return dictionary.ContainsKey(key) ? dictionary[key] : default(TVal);
        }

        /// <summary>
        /// Maps a single sequence into the series of sequences limited by size;
        /// This method is reverse to Enumerable.SelectMany
        /// </summary>
        public static IEnumerable<IEnumerable<T>> MapToMany<T>(this ICollection<T> input, int size)
        {
            if (size <= 0)
                throw new ArgumentException($"{nameof(size)} must be greater than 0", nameof(size));

            if (input == null)
            {
                yield return Enumerable.Empty<T>();
                yield break;
            }

            int offset = 0;
            while (offset < input.Count)
            {
                yield return input.Skip(offset).Take(size);
                offset += size;
            }
        }
    }
}
