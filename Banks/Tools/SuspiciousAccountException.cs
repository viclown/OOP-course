namespace Banks.Tools
{
    public class SuspiciousAccountException : BanksException
    {
        public SuspiciousAccountException()
        {
        }

        public SuspiciousAccountException(string message)
            : base(message)
        {
        }
    }
}