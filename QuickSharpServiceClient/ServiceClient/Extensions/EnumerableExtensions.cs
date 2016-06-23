namespace Dell.Service.API.Client.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Tests whether a sequence is null, and returns an empty sequence if it is.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// an <see cref="IEnumerable{T}"/> to check for nullity.
        /// </param>
        /// <returns>
        /// The original sequence, or an empty sequence if the original sequence is null.
        /// </returns>
        public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> source)
        {
            return source ?? Enumerable.Empty<TSource>();
        }

        /// <summary>
        /// Tests whether a sequence is null, and returns an empty sequence if it is.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// an <see cref="IEnumerable"/> to check for nullity.
        /// </param>
        /// <returns>
        /// The original sequence cast to <see cref="IEnumerable{T}"/>, or an empty sequence if the original sequence is null.
        /// </returns>
        public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable source)
        {
            return source != null ? source.Cast<TSource>() : Enumerable.Empty<TSource>();
        }

        /// <summary>
        /// Creates a new sequence of non-null instances from a sequence that might contain nulls,
        /// or be null itself.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// an <see cref="IEnumerable{T}"/> to filter for nullity or null instances in the sequence.
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{T}"/> that contains the non-null elements from the input sequence.
        /// </returns>
        public static IEnumerable<TSource> NullSafe<TSource>(this IEnumerable<TSource> source)
        {
            return source.EmptyIfNull().Where(s => s != null);
        }

        /// <summary>
        /// Creates a new sequence of non-null instances from a sequence that might contain nulls,
        /// or be null itself.
        /// </summary>
        /// <typeparam name="TSource">
        /// The type of the elements of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// an <see cref="IEnumerable{T}"/> to filter for nullity or null instances in the sequence.
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{T}"/> that contains the non-null elements from the input sequence.
        /// </returns>
        public static IEnumerable<TSource> NullSafe<TSource>(this IEnumerable source) where TSource : class
        {
            return source.EmptyIfNull<TSource>().Where(s => s != null);
        }

        /// <summary>
        /// Check if the collection have only one value and that value is not null.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <typeparam name="TSource">
        /// The type of the elements.
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool HasOnlyOne<TSource>(this IEnumerable<TSource> enumerable)
        {
            return enumerable != null && enumerable.Any() && enumerable.Count() == 1;
        }

        /// <summary>
        /// Check if the collection have only one value and that value is not null.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable.
        /// </param>
        /// <typeparam name="TSource">
        /// The type of the elements.
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool HasOnlyOneNonNull<TSource>(this IEnumerable<TSource> enumerable) where TSource : class
        {
            return enumerable.HasOnlyOne() && enumerable.FirstOrDefault() != default(TSource);
        }

        public static T SafeFirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> selector)
        {
            if (source != null && source.NullSafe().Any())
            {
                return source.FirstOrDefault<T>(selector);
            }
            return default(T);
        }

    }
}