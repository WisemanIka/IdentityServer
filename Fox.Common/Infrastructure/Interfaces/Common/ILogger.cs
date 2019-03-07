using System;
using Fox.Common.Constants;

namespace Fox.Common.Infrastructure
{
    public interface ILogger
    {
        void LogException(Exception exception, string source, LogLevels logLevel = LogLevels.Error);
        void LogMessage(string message, string source, LogLevels logLevel = LogLevels.Info);
    }
}
