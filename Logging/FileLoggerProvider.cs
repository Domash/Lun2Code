using Microsoft.Extensions.Logging;

namespace Lun2Code.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _path;

        public FileLoggerProvider(string path)
        {
            _path = path;
        }

        public void Dispose()
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return  new FileLogger(_path);
        }
    }
}