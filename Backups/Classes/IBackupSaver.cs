using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public interface IBackupSaver
    {
        List<Storage> Save(DirectoryInfo backupDirectory);
    }
}