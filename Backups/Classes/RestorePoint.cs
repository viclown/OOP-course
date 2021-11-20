using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Classes
{
    public class RestorePoint
    {
        public RestorePoint(DirectoryInfo restorePointDirectory, List<ZipArchive> zipArchives, DateTime date, Algorithm algorithm)
        {
            RestorePointDirectory = restorePointDirectory;
            ZipArchives = zipArchives;
            Date = date;
            Algorithm = algorithm;
        }

        public RestorePoint(DirectoryInfo restorePointDirectory, DateTime date, Algorithm algorithm)
            : this(restorePointDirectory, new List<ZipArchive>(), date, algorithm) { }

        public DirectoryInfo RestorePointDirectory { get; }
        public List<ZipArchive> ZipArchives { get; set; }
        public DateTime Date { get; }
        public Algorithm Algorithm { get; }
    }
}