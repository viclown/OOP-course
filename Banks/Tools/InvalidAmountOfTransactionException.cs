namespace Banks.Tools
{
    public class InvalidAmountOfTransactionException : BanksException
    {
        public InvalidAmountOfTransactionException()
        {
        }

        public InvalidAmountOfTransactionException(string message)
            : base(message)
        {
        }
    }
}