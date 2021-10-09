namespace Shops.Tools
{
    public class InvalidPriceException : ShopsException
    {
        public InvalidPriceException()
        {
        }

        public InvalidPriceException(string message)
            : base(message)
        {
        }
    }
}