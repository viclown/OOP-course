using System.IO;
using Backups.Classes;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        [Ignore("local tests")]
        public void BackupJobTest()
        {
            var backupService = new BackupService();
            IBackupSaver backup = new SplitSaver();
            BackupJob backupJob = backupService.CreateNewBackupJob(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups", "BackupJob", backup);
            var fileA = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileA");
            var fileB = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileB");
            backupJob.AddObjectToBackupJob(fileA);
            backupJob.AddObjectToBackupJob(fileB);
            RestorePoint restorePoint1 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName);
            backupJob.DeleteObjectFromBackupJob("fileB");
            RestorePoint restorePoint2 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName);
        }
    }
}