using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Tools;

namespace Backups.Classes
{
    public class BackupJob
    {
        private int _currentRestorePointNumber = 1;

        public BackupJob(DirectoryInfo backupDirectory, List<RestorePoint> restorePoints)
        {
            BackupDirectory = backupDirectory;
            RestorePoints = restorePoints;
        }

        public BackupJob(DirectoryInfo backupDirectory)
            : this(backupDirectory, new List<RestorePoint>()) { }

        public DirectoryInfo BackupDirectory { get; set; }
        public List<RestorePoint> RestorePoints { get; set; }

        public void AddObjectToBackupJob(FileInfo file)
        {
            file.CopyTo(BackupDirectory.FullName + @"\JobObjects\" + file.Name);
        }

        public FileInfo FindObjectInBackupJob(string fileName)
        {
            string filePath = BackupDirectory.FullName + @"\JobObjects\" + fileName;
            if (File.Exists(filePath))
                return new FileInfo(filePath);
            throw new FileDoesNotExistException();
        }

        public void DeleteObjectFromBackupJob(string fileName)
        {
            FileInfo file = FindObjectInBackupJob(fileName);
            file.Delete();
        }

        public RestorePoint CreateRestorePoint(string path, Algorithm algorithm)
        {
            int restorePointNumber = _currentRestorePointNumber;
            DirectoryInfo restorePointDirectory = Directory.CreateDirectory(path + @"\RestorePoint" + _currentRestorePointNumber++);
            var restorePoint = new RestorePoint(restorePointDirectory, DateTime.Now, algorithm);
            RestorePoints.Add(restorePoint);

            if (algorithm == Algorithm.Single)
            {
                AddFilesToRestorePointSingleAlgorithm(restorePoint, restorePointNumber);
            }
            else
            {
                AddFilesToRestorePointSplitAlgorithm(restorePoint, restorePointNumber);
            }

            return restorePoint;
        }

        private void AddFilesToRestorePointSplitAlgorithm(RestorePoint restorePoint, int restorePointNumber)
        {
            FileInfo[] files = BackupDirectory.GetFiles();
            foreach (FileInfo file in files)
            {
                string backupPath = restorePoint.RestorePointDirectory.FullName + @"\" +
                                    file.Name + "_" + restorePointNumber + ".zip";
                ZipArchive zipArchive = ZipFile.Open(backupPath, ZipArchiveMode.Create);
                zipArchive.CreateEntryFromFile(file.FullName, file.Name);
                restorePoint.ZipArchives.Add(zipArchive);
            }
        }

        private void AddFilesToRestorePointSingleAlgorithm(RestorePoint restorePoint, int restorePointNumber)
        {
            string backupPath = restorePoint.RestorePointDirectory.FullName + @"\" + "RestorePoint" +
                                "_" + restorePointNumber + ".zip";
            ZipArchive zipArchive = ZipFile.Open(backupPath, ZipArchiveMode.Create);
            FileInfo[] files = BackupDirectory.GetFiles();
            foreach (FileInfo file in files)
            {
                zipArchive.CreateEntryFromFile(file.FullName, file.Name);
            }

            restorePoint.ZipArchives.Add(zipArchive);
        }
    }
}