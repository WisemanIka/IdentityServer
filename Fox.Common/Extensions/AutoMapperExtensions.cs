using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Fox.Common.Extensions
{
    public static class AutoMapperExtensions
    {
        public static TDest Map<TSource, TDest>(this TSource source, TDest dest = null)
            where TSource : class
            where TDest : class, new()
        {
            if (source == null) return null;

            var result = Mapper.Map(source, dest ?? new TDest());

            return result;
        }

        public static IEnumerable<TDest> Map<TSource, TDest>(this IEnumerable<TSource> source)
            where TSource : class
            where TDest : class, new()
        {
            var result = source?.Select(i => i.Map<TSource, TDest>());
            return result;
        }

        public static IMappingExpression<TSource, TDest> IgnoreAll<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                var propertyInfo = sourceType.GetProperty(property.Name, flags);
                if (propertyInfo == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
