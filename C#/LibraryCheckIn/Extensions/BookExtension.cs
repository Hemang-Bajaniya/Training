using System;
using System.Collections.Generic;
using System.Linq;
using LibraryCheckIn.Domain;

namespace LibraryCheckIn.Extensions
{
    public static class BookExtensions
    {
        /// <summary>
        /// Returns the top N books ordered by the given key selector.
        /// </summary>
        public static IEnumerable<Book> TopBy<TKey>(
            this IEnumerable<Book> source,
            Func<Book, TKey> keySelector,
            int n)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (n <= 0) return Enumerable.Empty<Book>();

            return source
                .OrderByDescending(keySelector)  // highest first
                .Take(n);
        }

        /// <summary>
        /// Returns a dictionary of condition -> count of books.
        /// </summary>
        public static Dictionary<BookCondition, int> ToConditionCounts(
            this IEnumerable<Book> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source
                .GroupBy(b => b.Condition)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
