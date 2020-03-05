using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSLogGeneratorApi.Extensions
{
    public static class EnumerableExtensions
    {
        private static Random random = new Random();

        public static List<T> GetRandom<T>(this List<T> list, int numItems)
        {
            var items = new HashSet<T>();
            while (numItems > 0)
            {
                if (items.Add(list[random.Next(list.Count)]))
                {
                    numItems--;
                }
            }

            return items.ToList();
        }

        public static T GetRandomElement<T>(this List<T> items)
        {
            return items[random.Next(0, items.Count)];
        }
    }
}