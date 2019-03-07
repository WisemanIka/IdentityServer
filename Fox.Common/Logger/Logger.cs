using System;
using Fox.Common.Constants;
using Fox.Common.Infrastructure;
using Fox.Common.Providers;

namespace Fox.Common.Logger
{
    public class Logger : ILogger
    {
        private readonly ElasticSearchProvider _elasticSearch;
        public Logger()
        {
        }

        public Logger(ElasticSearchProvider elasticSearch)
        {
            _elasticSearch = elasticSearch;
        }

        public void LogException(Exception exception, string source, LogLevels logLevel = LogLevels.Error)
        {
            var log = new LogModel
            {
                DateTime = DateTime.UtcNow,
                Level = logLevel,
                Category = source,
                Exception = exception.ToString(),
                ExceptionMessage = exception.Message,
                ExceptionType = exception.GetType().Name,
                StackTrace = exception.StackTrace,
            };

            _elasticSearch.Client.IndexDocument(log);
        }

        public void LogMessage(string message, string source, LogLevels logLevel = LogLevels.Info)
        {
            var log = new LogModel
            {
                DateTime = DateTime.UtcNow,
                Level = logLevel,
                Message = message,
                Category = source,
            };

            _elasticSearch.Client.IndexDocument(log);
        }
    }
}
