using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using BackupsExtra.Classes;
using Serilog.Core;

namespace BackupsExtra.Services
{
    public class BackupExtraService
    {
        public BackupExtraService(IBackupLogger logger)
        {
            BackupLogger = logger.CreateLog();
        }

        public Logger BackupLogger { get; }
        private List<BackupExtraJob> BackupExtraJobs { get; } = new List<BackupExtraJob>();

        public BackupExtraJob CreateNewBackupJob(string path, string directoryBackupJobName, IBackupSaver backupSaver, IBackupCleaner backupCleaner)
        {
            DirectoryInfo directoryBackupExtraJob = Directory.CreateDirectory(Path.Combine(path, directoryBackupJobName));
            Directory.CreateDirectory(Path.Combine(directoryBackupExtraJob.FullName, "JobObjects"));
            var backupExtraJob = new BackupExtraJob(directoryBackupExtraJob, backupSaver, backupCleaner, BackupLogger);
            BackupExtraJobs.Add(backupExtraJob);
            BackupLogger.Information($"Backup job at {path} was successfully created");
            return backupExtraJob;
        }
    }
}