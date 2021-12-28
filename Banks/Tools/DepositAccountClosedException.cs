namespace Banks.Tools
{
    public class DepositAccountClosedException : BanksException
    {
        public DepositAccountClosedException()
        {
        }

        public DepositAccountClosedException(string message)
            : base(message)
        {
        }
    }
}