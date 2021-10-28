using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Classes
{
    public class Person
    {
        private float _money;
        public Person(string name, float money, List<ShopProduct> products)
        {
            Name = name;
            Money = money;
            Products = products;
        }

        public Person(string name, float money)
            : this(name, money, new List<ShopProduct>()) { }

        public string Name { get; }
        public List<ShopProduct> Products { get; }

        public float Money
        {
            get => _money;
            private set
            {
                _money = value;
                if (_money < 0)
                    throw new InvalidMoneyException($"Trying to set invalid amount of money ({_money})");
            }
        }

        public void UpdatePersonMoneyAfterBuying(ShopProduct shopProduct, ProductToBuy productToBuy)
        {
            Money -= shopProduct.ShopPrice * productToBuy.Quantity;
        }
    }
}