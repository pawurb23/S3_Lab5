using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class EnumerableExtensions
    {
        public static (TResult Min, TResult Max) MinMax<TSource, TResult>
            (this IEnumerable<TSource> source, Func<TSource, TResult> selector)
            where TResult : IComparable<TResult>
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            using var iterator = source.GetEnumerator();

            if (!iterator.MoveNext()) { throw new InvalidOperationException("Pusta kolekcja"); }

            TResult min = selector(iterator.Current);
            TResult max = min;

            while (iterator.MoveNext())
            {
                TResult wartosc = selector(iterator.Current);

                if (wartosc.CompareTo(min) < 0) min = wartosc;
                if (wartosc.CompareTo(max) > 0) max = wartosc;
            }

            return (min, max);
        }
    }
}
