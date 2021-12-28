namespace Banks.Tools
{
    public class ReachedCreditLimitException : BanksException
    {
        public ReachedCreditLimitException()
        {
        }

        public ReachedCreditLimitException(string message)
            : base(message)
        {
        }
    }
}