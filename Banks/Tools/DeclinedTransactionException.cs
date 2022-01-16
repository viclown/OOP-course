namespace Banks.Tools
{
    public class DeclinedTransactionException : BanksException
    {
        public DeclinedTransactionException()
        {
        }

        public DeclinedTransactionException(string message)
            : base(message)
        {
        }
    }
}