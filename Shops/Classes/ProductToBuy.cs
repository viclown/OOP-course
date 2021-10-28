using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Classes
{
    public class ProductToBuy
    {
        private int _quantity;
        public ProductToBuy(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                if (_quantity < 0)
                    throw new InvalidQuantityException($"Trying to set invalid quantity ({_quantity}) for {Name}");
            }
        }
    }
}