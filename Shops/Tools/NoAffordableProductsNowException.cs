using System;

namespace Shops.Tools
{
    public class NoAffordableProductsNowException : ShopsException
    {
        public NoAffordableProductsNowException()
        {
        }

        public NoAffordableProductsNowException(string message)
            : base(message)
        {
        }
    }
}