using System;
using System.IO;
using Serilog;
using Serilog.Core;

namespace BackupsExtra.Classes
{
    public class FileLogger : IBackupLogger
    {
        public Logger CreateLog()
        {
            string template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] [{SourceContext}] {Message}{NewLine}{Exception}";
            string path = Path.Combine(Environment.CurrentDirectory, @"\loggers\log.txt");
            return new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(path, outputTemplate: template).CreateLogger();
        }
    }
}