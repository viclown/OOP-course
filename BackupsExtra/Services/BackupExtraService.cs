using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using BackupsExtra.Classes;

namespace BackupsExtra.Services
{
    public class BackupExtraService
    {
        private List<BackupExtraJob> BackupExtraJobs { get; } = new List<BackupExtraJob>();

        public BackupExtraJob CreateNewBackupJob(string path, string directoryBackupJobName, IBackupSaver backupSaver, IBackupCleaner backupCleaner)
        {
            DirectoryInfo directoryBackupExtraJob = Directory.CreateDirectory(Path.Combine(path, directoryBackupJobName));
            Directory.CreateDirectory(Path.Combine(directoryBackupExtraJob.FullName, "JobObjects"));
            var backupExtraJob = new BackupExtraJob(directoryBackupExtraJob, backupSaver, backupCleaner);
            BackupExtraJobs.Add(backupExtraJob);
            return backupExtraJob;
        }
    }
}