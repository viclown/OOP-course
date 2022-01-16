namespace Banks.Tools
{
    public class InvalidPassportException : BanksException
    {
        public InvalidPassportException()
        {
        }

        public InvalidPassportException(string message)
            : base(message)
        {
        }
    }
}