using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Dg.Framework
{
    public static class IEnumberableExtensions
    {
        [Pure]
        public static IEnumerable<T> Replace<T>(this IEnumerable<T> enumerable, Func<T, bool> matcher, Func<T, T> replaceFunc) 
        {
            var replacedAny = false;

            foreach (var current in enumerable)
            {
                if (matcher(current) && !replacedAny)
                {
                    replacedAny = true;
                    yield return replaceFunc(current);
                }
                else
                {
                    yield return current;
                }
            }
        }
    }
}