using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Services;
using Backups.Tools;

namespace Backups.Classes
{
    public class BackupJob
    {
        private int _currentRestorePointNumber = 1;
        private IBackupSaver _backupSaver;

        public BackupJob(DirectoryInfo backupDirectory, IBackupSaver backupSaver)
        {
            BackupDirectory = backupDirectory;
            _backupSaver = backupSaver;
        }

        public DirectoryInfo BackupDirectory { get; set; }
        public List<RestorePoint> RestorePoints { get; set; } = new List<RestorePoint>();

        public void AddObjectToBackupJob(FileInfo file)
        {
            file.CopyTo(Path.Combine(BackupDirectory.FullName, "JobObjects", file.Name));
        }

        public FileInfo FindObjectInBackupJob(string fileName)
        {
            string filePath = Path.Combine(BackupDirectory.FullName, "JobObjects", fileName);

            if (File.Exists(filePath))
                return new FileInfo(filePath);

            throw new FileDoesNotExistException();
        }

        public void DeleteObjectFromBackupJob(string fileName)
        {
            FileInfo file = FindObjectInBackupJob(fileName);
            file.Delete();
        }

        public RestorePoint CreateRestorePoint(string path)
        {
            int restorePointNumber = _currentRestorePointNumber;
            DirectoryInfo restorePointDirectory = Directory.CreateDirectory(Path.Combine(path, $"RestorePoint{_currentRestorePointNumber++}"));
            var restorePoint = new RestorePoint(restorePointDirectory, DateTime.Now);
            RestorePoints.Add(restorePoint);
            var directory = new DirectoryInfo(Path.Combine(BackupDirectory.FullName, "JobObjects"));
            AddFilesToRestorePoint(directory, restorePoint);
            return restorePoint;
        }

        private void AddFilesToRestorePoint(DirectoryInfo backupDirectory, RestorePoint restorePoint)
        {
            List<Storage> storages = _backupSaver.Save(backupDirectory);
            int id = 0;
            foreach (Storage storage in storages)
            {
                var directory = new DirectoryInfo(Path.Combine(BackupDirectory.FullName, "temp"));
                directory.Create();
                foreach (FileInfo file in storage.Files)
                {
                    file.CopyTo(Path.Combine(directory.FullName, file.Name));
                }

                ZipFile.CreateFromDirectory(directory.FullName, Path.Combine(restorePoint.RestorePointDirectory.ToString(), $"Files_{id++}.zip"));
                directory.Delete(true);
            }
        }
    }
}