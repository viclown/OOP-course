using Serilog;
using Serilog.Core;

namespace BackupsExtra.Classes
{
    public class ConsoleLogger : IBackupLogger
    {
        public Logger CreateLog()
        {
            string template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] [{SourceContext}] {Message}{NewLine}{Exception}";
            return new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console(outputTemplate: template).CreateLogger();
        }
    }
}