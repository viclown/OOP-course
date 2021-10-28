using System;

namespace Shops.Tools
{
    public class ShopDoesNotHaveEnoughProductException : ShopsException
    {
        public ShopDoesNotHaveEnoughProductException()
        {
        }

        public ShopDoesNotHaveEnoughProductException(string message)
            : base(message)
        {
        }
    }
}