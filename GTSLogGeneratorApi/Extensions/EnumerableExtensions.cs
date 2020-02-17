using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSLogGeneratorApi.Extensions
{
    public static class EnumerableExtensions
    {

        public static List<T> GetRandom<T>(this List<T> list, int numItems)
        {
            var random = new Random();

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
        
        public static T GetRandomElement<T>(this List<T> items, int seedMultiply)
        {
            var random = new Random(Environment.TickCount * seedMultiply);
            return items[random.Next(0, items.Count)];
        }
    }
}