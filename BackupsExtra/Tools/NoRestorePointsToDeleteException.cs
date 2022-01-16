namespace BackupsExtra.Tools
{
    public class NoRestorePointsToDeleteException : BackupExtraException
    {
        public NoRestorePointsToDeleteException(string message)
            : base(message)
        {
        }
    }
}