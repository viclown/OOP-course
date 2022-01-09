using System.Collections.Generic;
using Backups.Classes;

namespace BackupsExtra.Classes
{
    public interface IBackupCleaner
    {
        List<RestorePoint> Clear(List<RestorePoint> restorePoints);
    }
}