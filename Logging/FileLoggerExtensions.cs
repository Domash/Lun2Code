using Microsoft.Extensions.Logging;

namespace Lun2Code.Logging
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string path)
        {
            factory.AddProvider(provider: new FileLoggerProvider(path));
            return factory;
        }
    }
}