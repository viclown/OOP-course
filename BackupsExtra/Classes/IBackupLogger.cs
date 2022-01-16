using Serilog.Core;

namespace BackupsExtra.Classes
{
    public interface IBackupLogger
    {
        Logger CreateLog();
    }
}