using System;

namespace Shops.Tools
{
    public class ShopHasNotEnoughMoneyException : ShopsException
    {
        public ShopHasNotEnoughMoneyException()
        {
        }

        public ShopHasNotEnoughMoneyException(string message)
            : base(message)
        {
        }
    }
}