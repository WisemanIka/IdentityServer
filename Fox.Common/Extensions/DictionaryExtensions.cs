using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fox.Common.Models;

namespace Fox.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<SimpleDictionary> ToSimpleDictionary<TSource>(this IEnumerable<TSource> source, string propertyName = "Name") where TSource : class
        {
            if (source == null) return null;

            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            var result = source.Select(x => new SimpleDictionary
            {
                Id = x.GetType().GetProperty("Id", flags).GetValue(x).ToString(),
                Name = x.GetType().GetProperty(propertyName, flags).GetValue(x).ToString()
            }).ToList();

            return result;
        }
    }
}
