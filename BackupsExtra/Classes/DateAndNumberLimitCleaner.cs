using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using BackupsExtra.Tools;

namespace BackupsExtra.Classes
{
    public class DateAndNumberLimitCleaner : IBackupCleaner
    {
        public DateAndNumberLimitCleaner(DateTime dateLimit, int numberLimit)
        {
            DateLimit = dateLimit;
            NumberLimit = numberLimit;
        }

        public DateTime DateLimit { get; set; }
        public int NumberLimit { get; set; }

        public List<RestorePoint> Clear(List<RestorePoint> restorePoints)
        {
            var pointsToDelete = new List<RestorePoint>();

            var dateClean = restorePoints.Where(restorePoint => restorePoint.Date < DateLimit).ToList();

            if (restorePoints.Count > NumberLimit && restorePoints.Count > 0)
            {
                var numberClean = restorePoints.Take(restorePoints.Count - NumberLimit).ToList();
                pointsToDelete = dateClean.Intersect(numberClean).ToList();
            }

            if (pointsToDelete.Count == restorePoints.Count)
                throw new TryingToDeleteAllRestorePointsException("It is impossible to delete all existing restore points");

            if (pointsToDelete.Count == 0)
                throw new NoRestorePointsToDeleteException("No points for deletion found");

            return pointsToDelete;
        }
    }
}