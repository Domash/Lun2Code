using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Lun2Code.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _path;
        private static readonly object _lock = new object();

        public FileLogger(string path)
        {
            _path = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter == null) return;

            lock (_lock)
            {
                // Create logs style
                File.AppendAllText(_path, formatter(state, exception) + Environment.NewLine);
            }

        }
    }
}