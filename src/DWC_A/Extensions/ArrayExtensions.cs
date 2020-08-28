using System;
using System.Collections.Generic;
using System.Linq;

namespace DwC_A.Extensions
{
    internal static class ArrayExtensions
    {
        public static T[] ToArray<T>(this IEnumerable<T> list, int size)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            var array = size > 0 ? new T[size] : new T[list.Count()];
            int index = 0;
            using(var iter = list.GetEnumerator())
            {
                while (iter.MoveNext() && index < array.Length)
                {
                    array[index++] = iter.Current;
                }
            }
            return array;
        }
    }
}
