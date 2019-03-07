using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fox.Common.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LogLevels
    {
        [EnumMember(Value = "error")] Error,
        [EnumMember(Value = "warn")] Warn,
        [EnumMember(Value = "info")] Info,
        [EnumMember(Value = "debug")] Debug,
        [EnumMember(Value = "trace")] Trace,
    }
}
