namespace Banks.Tools
{
    public class BankWasNotFoundException : BanksException
    {
        public BankWasNotFoundException()
        {
        }

        public BankWasNotFoundException(string message)
            : base(message)
        {
        }
    }
}