namespace Shops.Tools
{
    public class InvalidMoneyException : ShopsException
    {
        public InvalidMoneyException()
        {
        }

        public InvalidMoneyException(string message)
            : base(message)
        {
        }
    }
}