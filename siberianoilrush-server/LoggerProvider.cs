using Microsoft.Extensions.Logging;
using siberianoilrush_server.Service;

namespace siberianoilrush_server
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly LoggerService _loggerService;
        public LoggerProvider(LoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public ILogger CreateLogger(string categoryName)
            => new Logger(_loggerService);

        public void Dispose() { }
    }
}
