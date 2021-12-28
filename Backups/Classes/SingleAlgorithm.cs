using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class SingleAlgorithm : IBackupService
    {
        public List<List<FileInfo>> Save(DirectoryInfo backupDirectory)
        {
            var files = new List<FileInfo>();
            FileInfo[] readFiles = backupDirectory.GetFiles();

            foreach (FileInfo readFile in readFiles)
            {
                files.Add(readFile);
            }

            var returnFiles = new List<List<FileInfo>>();
            returnFiles.Add(files);
            return returnFiles;
        }
    }
}