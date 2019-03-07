using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using ProtoBuf;

namespace Fox.Common.Configurations
{
    public class Configuration
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Environment { get; set; }
        public ConfigurationDocument ConfigurationDocument { get; set; }
    }

    [ProtoContract]
    public class ConfigurationDocument
    {
        [ProtoMember(1, DynamicType = true)]
        public Dictionary<string, object> Document { get; set; }

        public ConfigurationDocument()
        {
            Document = new Dictionary<string, object>();
        }

        public T GetAs<T>(string key)
        {
            if (!Document.ContainsKey(key))
                throw new Exception("Key not found in Configuration");

            var value = Document[key];

            if (value is T variable)
                return variable;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public T GetAsOrDefault<T>(string key)
        {
            if (!Document.ContainsKey(key))
                return default(T);

            var value = Document[key];

            if (value is T variable)
                return variable;

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public void Add(string key, object value) => Document.Add(key, value);
    }
}
