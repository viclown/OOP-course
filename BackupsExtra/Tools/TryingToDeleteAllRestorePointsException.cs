namespace BackupsExtra.Tools
{
    public class TryingToDeleteAllRestorePointsException : BackupExtraException
    {
        public TryingToDeleteAllRestorePointsException(string message)
            : base(message)
        {
        }
    }
}