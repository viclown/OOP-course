namespace Shops.Tools
{
    public class InvalidQuantityException : ShopsException
    {
        public InvalidQuantityException()
        {
        }

        public InvalidQuantityException(string message)
            : base(message)
        {
        }
    }
}