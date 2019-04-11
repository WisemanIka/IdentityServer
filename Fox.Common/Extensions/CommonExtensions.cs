using System;
using System.Collections.Generic;
using System.Reflection;
using Fox.Common.Models;

namespace Fox.Common.Extensions
{
    public static class CommonExtensions
    {
        //Not Equal Properties
        public static RevisionModel GetRevisionProperties<T>(this T source, T dest) where T : class
        {
            if (source.GetType() != dest.GetType())
                throw new InvalidOperationException("Two objects should be from the same type");

            var result = new RevisionModel
            {
                Id = string.Empty,
                Properties = new List<KeyValuePair<string, object>>()
            };

            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(T);
            var sourceProperties = sourceType.GetProperties(flags);

            foreach (var property in sourceProperties)
            {
                var propertyInfo = sourceType.GetProperty(property.Name, flags);
                var sourcePropertyValue = propertyInfo.GetValue(source, null);

                if (property.Name == "Id")
                {
                    result.Id = sourcePropertyValue.ToString();
                    continue;
                }

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
                    //result.Properties.Add(new DictionaryModel { Key = property.Name, Value = sourcePropertyValue.ToString() });
                    result.Properties.Add(new KeyValuePair<string, object>(property.Name, sourcePropertyValue.ToString()));
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
