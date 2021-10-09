using System;

namespace Shops.Tools
{
    public class PersonDoesNotHaveEnoughMoneyException : ShopsException
    {
        public PersonDoesNotHaveEnoughMoneyException()
        {
        }

        public PersonDoesNotHaveEnoughMoneyException(string message)
            : base(message)
        {
        }
    }
}