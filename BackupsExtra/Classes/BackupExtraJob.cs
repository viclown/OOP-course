using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Classes;

namespace BackupsExtra.Classes
{
    public class BackupExtraJob : BackupJob
    {
        private IBackupCleaner _backupCleaner;
        private int _mergerId = 1;

        public BackupExtraJob(DirectoryInfo backupDirectory, IBackupSaver backupSaver, IBackupCleaner backupCleaner)
            : base(backupDirectory, backupSaver)
        {
            _backupCleaner = backupCleaner;
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
        }
    }
}