using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class SingleSaver : IBackupSaver
    {
        public List<Storage> Save(DirectoryInfo backupDirectory)
        {
            var storage = new Storage();
            FileInfo[] readFiles = backupDirectory.GetFiles();

            foreach (FileInfo readFile in readFiles)
            {
                storage.AddFileToStorage(readFile);
            }

            var returnFiles = new List<Storage> { storage };
            return returnFiles;
        }
    }
}