using Microsoft.Extensions.Logging;

namespace Lun2Code.Logging
{
    public class Logger
    {
        public static ILoggerFactory LoggerFactory { get; set; }
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
        public static ILogger CreateLogger(string name) => LoggerFactory.CreateLogger(name);
    }
}