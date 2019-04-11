using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fox.Common.Extensions
{
    public class KvpConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<string, object>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dictionary = (Dictionary<string, object>)value;
            writer.WriteStartObject();
            foreach (var dic in dictionary)
            {
                writer.WritePropertyName(dic.Key);
                serializer.Serialize(writer, dic.Value);
            }

            writer.WriteEndObject();
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
