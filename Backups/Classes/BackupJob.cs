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
        private IBackupService _backupService;

        public BackupJob(DirectoryInfo backupDirectory, IBackupService backupService)
        {
            BackupDirectory = backupDirectory;
            _backupService = backupService;
        }

        public DirectoryInfo BackupDirectory { get; set; }
        public List<RestorePoint> RestorePoints { get; set; } = new List<RestorePoint>();

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

        public RestorePoint CreateRestorePoint(string path)
        {
            int restorePointNumber = _currentRestorePointNumber;
            DirectoryInfo restorePointDirectory = Directory.CreateDirectory(path + @"\RestorePoint" + _currentRestorePointNumber++);
            var restorePoint = new RestorePoint(restorePointDirectory, DateTime.Now);
            RestorePoints.Add(restorePoint);
            var directory = new DirectoryInfo(BackupDirectory.FullName + @"\JobObjects");
            AddFilesToRestorePoint(directory, restorePoint);
            return restorePoint;
        }

        private void AddFilesToRestorePoint(DirectoryInfo backupDirectory, RestorePoint restorePoint)
        {
            List<List<FileInfo>> files = _backupService.Save(backupDirectory);
            int id = 0;
            foreach (List<FileInfo> list in files)
            {
                var directory = new DirectoryInfo(BackupDirectory.FullName + @"\temp");
                directory.Create();
                foreach (FileInfo file in list)
                {
                    file.CopyTo(Path.Combine(directory.FullName, file.Name));
                }

                ZipFile.CreateFromDirectory(directory.FullName, Path.Combine(restorePoint.RestorePointDirectory.ToString(), $"Files_{id++}.zip"));
                directory.Delete(true);
            }
        }
    }
}