using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Classes
{
    public class RestorePoint
    {
        public RestorePoint(DirectoryInfo restorePointDirectory, List<ZipArchive> zipArchives, DateTime date)
        {
            RestorePointDirectory = restorePointDirectory;
            ZipArchives = zipArchives;
            Date = date;
        }

        public RestorePoint(DirectoryInfo restorePointDirectory, DateTime date)
            : this(restorePointDirectory, new List<ZipArchive>(), date) { }

        public DirectoryInfo RestorePointDirectory { get; }
        public List<ZipArchive> ZipArchives { get; }
        public DateTime Date { get; }
    }
}