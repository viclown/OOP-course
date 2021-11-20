using System.IO;
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
        }
    }
}