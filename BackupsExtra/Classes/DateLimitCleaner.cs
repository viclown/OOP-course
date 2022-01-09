using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using BackupsExtra.Tools;

namespace BackupsExtra.Classes
{
    public class DateLimitCleaner : IBackupCleaner
    {
        public DateLimitCleaner(DateTime dateLimit)
        {
            DateLimit = dateLimit;
        }

        public DateTime DateLimit { get; set; }

        public List<RestorePoint> Clear(List<RestorePoint> restorePoints)
        {
            var pointsToDelete = restorePoints.Where(restorePoint => restorePoint.Date < DateLimit).ToList();
            if (pointsToDelete.Count == 0)
                throw new NoRestorePointsToDeleteException("No points for deletion found");
            return pointsToDelete;
        }
    }
}