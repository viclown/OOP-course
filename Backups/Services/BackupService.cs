using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Backups.Classes;

namespace Backups.Services
{
    public class BackupService
    {
        private List<BackupJob> BackupJobs { get; } = new List<BackupJob>();

        public BackupJob CreateNewBackupJob(string path, string directoryBackupJobName, IBackupSaver backupService)
        {
            DirectoryInfo directoryBackupJob = Directory.CreateDirectory(Path.Combine(path, directoryBackupJobName));
            Directory.CreateDirectory(Path.Combine(directoryBackupJob.FullName, "JobObjects"));
            var backupJob = new BackupJob(directoryBackupJob, backupService);
            BackupJobs.Add(backupJob);
            return backupJob;
        }
    }
}