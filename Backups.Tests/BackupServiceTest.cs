﻿using System.IO;
using Backups.Classes;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class Tests
    {
        [Test]
        public void BackupJobTest1()
        {
            var backupService = new BackupService();
            BackupJob backupJob = backupService.CreateNewBackupJob(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups", "BackupJob");
            var fileA = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileA");
            var fileB = new FileInfo(@"C:\Users\Виктория\Desktop\OOP\viclown\Backups\FileB");
            backupJob.AddObjectToBackupJob(fileA);
            backupJob.AddObjectToBackupJob(fileB);
            RestorePoint restorePoint1 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName, Algorithm.Single);
            backupJob.DeleteObjectFromBackupJob("fileB");
            RestorePoint restorePoint2 = backupJob.CreateRestorePoint(backupJob.BackupDirectory.FullName, Algorithm.Single);
        }
    }
}