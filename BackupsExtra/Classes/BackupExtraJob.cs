using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Classes;
using Serilog.Core;

namespace BackupsExtra.Classes
{
    public class BackupExtraJob : BackupJob
    {
        private IBackupCleaner _backupCleaner;
        private Logger _backupLogger;
        private int _mergerId = 1;

        public BackupExtraJob(DirectoryInfo backupDirectory, IBackupSaver backupSaver, IBackupCleaner backupCleaner, Logger logger)
            : base(backupDirectory, backupSaver)
        {
            _backupCleaner = backupCleaner;
            _backupLogger = logger;
        }

        public RestorePoint MergeRestorePoints(RestorePoint oldPoint, RestorePoint newPoint)
        {
            if (oldPoint.RestorePointDirectory.GetFiles().Length > 1)
            {
                foreach (FileInfo file in oldPoint.RestorePointDirectory.GetFiles())
                {
                    if (!newPoint.RestorePointDirectory.GetFiles().ToList().Contains(file))
                    {
                        file.CopyTo(Path.Combine(newPoint.RestorePointDirectory.FullName, $"Files_1_{_mergerId++}.zip"));
                    }
                }
            }

            oldPoint.RestorePointDirectory.Delete(true);
            RestorePoints.Remove(oldPoint);
            _backupLogger.Information($"Two restore point of backup job at {BackupDirectory.FullName} were successfully merged");
            return newPoint;
        }

        public void DeleteRestorePointsToFixLimit()
        {
            List<RestorePoint> restorePoints = _backupCleaner.Clear(RestorePoints);
            foreach (RestorePoint point in restorePoints)
            {
                point.RestorePointDirectory.Delete(true);
                RestorePoints.Remove(point);
            }

            _backupLogger.Information($"Some restore point of backup job at {BackupDirectory.FullName} were successfully deleted to fix limit of points");
        }

        public void RecoverFilesFromRestorePoint(string path, RestorePoint restorePoint)
        {
            var directory = new DirectoryInfo(path);
            directory.Create();
            foreach (ZipArchive archive in restorePoint.ZipArchives)
            {
                archive.ExtractToDirectory(directory.FullName);
            }

            _backupLogger.Information($"Files were successfully recovered to {path}");
        }
    }
}