using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using BackupsExtra.Tools;

namespace BackupsExtra.Classes
{
    public class NumberLimitCleaner : IBackupCleaner
    {
        public NumberLimitCleaner(int numberLimit)
        {
            NumberLimit = numberLimit;
        }

        public int NumberLimit { get; set; }

        public List<RestorePoint> Clear(List<RestorePoint> restorePoints)
        {
            var pointsToDelete = new List<RestorePoint>();
            if (restorePoints.Count > NumberLimit && restorePoints.Count > 0)
                pointsToDelete = restorePoints.Take(restorePoints.Count - NumberLimit).ToList();

            if (pointsToDelete.Count == 0)
                throw new NoRestorePointsToDeleteException("No points for deletion found");

            return pointsToDelete;
        }
    }
}