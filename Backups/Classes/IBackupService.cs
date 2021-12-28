using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public interface IBackupService
    {
        List<List<FileInfo>> Save(DirectoryInfo backupDirectory);
    }
}