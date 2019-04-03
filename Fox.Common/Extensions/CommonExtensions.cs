using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fox.Common.Models;

namespace Fox.Common.Extensions
{
    public static class CommonExtensions
    {
        public static List<RevisionModel> GetNonEqualProperties<T>(this T source, T dest) where T : class
        {
            if (source.GetType() != dest.GetType())
                throw new InvalidOperationException("Two objects should be from the same type");

            var result = new List<RevisionModel>();

            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(T);
            var sourceProperties = sourceType.GetProperties(flags);

            foreach (var property in sourceProperties)
            {
                var propertyInfo = sourceType.GetProperty(property.Name, flags);
                var sourcePropertyValue = propertyInfo.GetValue(source, null);
                var destPropertyValue = propertyInfo.GetValue(dest, null);

                if (sourcePropertyValue == null || destPropertyValue == null)
                    continue;

                //if (sourcePropertyValue.IsGenericList())
                //{
                //    var count = (int)sourcePropertyValue.GetType().GetProperty("Count").GetValue(sourcePropertyValue, null);

                //    for (var i = 0; i < count; i++)
                //    {
                //        object[] index = { i };

                //        var mySourceObject = sourcePropertyValue.GetType().GetProperty("Items").GetValue(sourcePropertyValue, index);
                //        var myDestObject = destPropertyValue.GetType().GetProperty("Items").GetValue(destPropertyValue, index);

                //        if (mySourceObject == null || myDestObject == null)
                //            break;

                //        if (mySourceObject.Equals())
                //    }
                //}
                //else

                if (!sourcePropertyValue.Equals(destPropertyValue))
                {
                    result.Add(new RevisionModel { Key = property.Name, Value = sourcePropertyValue });
                }
            }

            return result;
        }

        public static bool IsGenericList(this object o)
        {
            var oType = o.GetType();
            return oType.IsGenericType && oType.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
