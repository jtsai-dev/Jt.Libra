using System;
using System.Collections.Generic;
using System.Linq;

namespace Jt.Libra.Infrastructure.Extension
{
    public static class EnumerableExtension
    {
        public static List<T> GetAndRemove<T>(this List<T> data, Func<T, bool> predicate)
        {
            lock (data)
            {
                var items = data.Where(predicate).ToList();
                data.RemoveAll(p => predicate(p));
                return items;
            }
        }
    }
}
