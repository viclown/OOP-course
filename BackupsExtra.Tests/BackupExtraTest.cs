using System.IO;
using Backups.Classes;
using BackupsExtra.Classes;
using BackupsExtra.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        [Ignore("local test")]
        public void MergeRestorePointsTest()
        {
            var backupLogger = new FileLogger();
            var backupService = new BackupExtraService(backupLogger);
            IBackupSaver saver = new SplitSaver();
            IBackupCleaner cleaner = new NumberLimitCleaner(1);
            BackupExtraJob backupJob = backupService.CreateNewBackupJob(@"C:\Users\Виктория\Desktop\OOP\viclown\BackupsExtra", "BackupJob", saver, cleaner);
            var fileA = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileA");
            var fileB = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileB");
            backupJob.AddObjectToBackupJob(fileA);
            backupJob.AddObjectToBackupJob(fileB);
            RestorePoint restorePoint1 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName);
            backupJob.DeleteObjectFromBackupJob("fileB");
            RestorePoint restorePoint2 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName);
            backupJob.MergeRestorePoints(restorePoint1, restorePoint2);
        }
        
        [Ignore("local test")]
        public void DeleteRestorePointsTest()
        {
            var backupLogger = new FileLogger();
            var backupService = new BackupExtraService(backupLogger);
            IBackupSaver saver = new SplitSaver();
            IBackupCleaner cleaner = new NumberLimitCleaner(1);
            BackupExtraJob backupJob = backupService.CreateNewBackupJob(@"C:\Users\Виктория\Desktop\OOP\viclown\BackupsExtra", "BackupJob", saver, cleaner);
            var fileA = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileA");
            var fileB = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileB");
            backupJob.AddObjectToBackupJob(fileA);
            backupJob.AddObjectToBackupJob(fileB);
            RestorePoint restorePoint1 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName);
            RestorePoint restorePoint2 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName);
            backupJob.DeleteRestorePointsToFixLimit();
        }
    }
}