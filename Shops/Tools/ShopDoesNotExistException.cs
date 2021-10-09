using System;

namespace Shops.Tools
{
    public class ShopDoesNotExistException : ShopsException
    {
        public ShopDoesNotExistException()
        {
        }

        public ShopDoesNotExistException(string message)
            : base(message)
        {
        }
    }
}