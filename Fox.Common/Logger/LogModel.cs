using System;
using Fox.Common.Constants;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fox.Common.Logger
{
    public class LogModel
    {
        public DateTime DateTime { get; set; }
        [Keyword]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogLevels Level { get; set; }
        [Keyword]
        public string Category { get; set; }
        public string Message { get; set; }
        [Keyword]
        public string UserName { get; set; }
        [Keyword]
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string Exception { get; set; }
        public bool HasException => Exception != null;
        public string StackTrace { get; set; }
    }
}
