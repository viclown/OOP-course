using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Backups.Classes;

namespace Backups.Services
{
    public class BackupService
    {
        private List<BackupJob> BackupJobs { get; } = new List<BackupJob>();

        public BackupJob CreateNewBackupJob(string path, string directoryBackupJobName, IBackupService backupService)
        {
            DirectoryInfo directoryBackupJob = Directory.CreateDirectory(path + @"\" + directoryBackupJobName);
            Directory.CreateDirectory(directoryBackupJob.FullName + @"\JobObjects");
            var backupJob = new BackupJob(directoryBackupJob, backupService);
            BackupJobs.Add(backupJob);
            return backupJob;
        }
    }
}