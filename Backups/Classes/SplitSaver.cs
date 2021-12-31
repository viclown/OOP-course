using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class SplitSaver : IBackupSaver
    {
        public List<Storage> Save(DirectoryInfo backupDirectory)
        {
            var returnFiles = new List<Storage>();
            FileInfo[] readFiles = backupDirectory.GetFiles();

            foreach (FileInfo readFile in readFiles)
            {
                var storage = new Storage();
                storage.AddFileToStorage(readFile);
                returnFiles.Add(storage);
            }

            return returnFiles;
        }
    }
}