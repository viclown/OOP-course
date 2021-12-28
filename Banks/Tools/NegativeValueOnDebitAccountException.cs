namespace Banks.Tools
{
    public class NegativeValueOnDebitAccountException : BanksException
    {
        public NegativeValueOnDebitAccountException()
        {
        }

        public NegativeValueOnDebitAccountException(string message)
            : base(message)
        {
        }
    }
}