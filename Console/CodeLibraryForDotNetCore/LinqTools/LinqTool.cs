using System;
using System.Collections.Generic;
using System.Text;

namespace CodeLibraryForDotNetCore.LinqTools
{
    public static class LinqTool
    {
        //more tool:https://github.com/morelinq/MoreLINQ
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
