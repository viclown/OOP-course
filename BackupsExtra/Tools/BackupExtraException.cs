using System;

namespace BackupsExtra.Tools
{
    public class BackupExtraException : Exception
    {
        public BackupExtraException()
        {
        }

        public BackupExtraException(string message)
            : base(message)
        {
        }

        public BackupExtraException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}