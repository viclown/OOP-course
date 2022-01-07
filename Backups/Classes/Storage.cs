using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class Storage
    {
        public Storage(List<FileInfo> files)
        {
            Files = files;
        }

        public Storage()
            : this(new List<FileInfo>()) { }

        public List<FileInfo> Files { get; }

        public void AddFileToStorage(FileInfo file)
        {
            Files.Add(file);
        }
    }
}