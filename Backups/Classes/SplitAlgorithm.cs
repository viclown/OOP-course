using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class SplitAlgorithm : IBackupService
    {
        public List<List<FileInfo>> Save(DirectoryInfo backupDirectory)
        {
            var returnFiles = new List<List<FileInfo>>();
            FileInfo[] readFiles = backupDirectory.GetFiles();

            foreach (FileInfo readFile in readFiles)
            {
                var file = new List<FileInfo>();
                file.Add(readFile);
                returnFiles.Add(file);
            }

            return returnFiles;
        }
    }
}