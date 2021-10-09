using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Classes
{
    public class Shop
    {
        private readonly int _lastId = 100000;
        private float _money;
        public Shop(string name, string address, float money, List<ShopProduct> products)
        {
            Name = name;
            Address = address;
            Money = money;
            ShopProducts = products;
            Id = _lastId++;
        }

        public Shop(string name, string address, float money)
            : this(name, address, money, new List<ShopProduct>()) { }

        public string Name { get; }
        public List<ShopProduct> ShopProducts { get; }
        public string Address { get; }
        public int Id { get; }
        public float Money
        {
            get => _money;
            private set
            {
                _money = value;
                if (_money < 0)
                    throw new InvalidMoneyException($"Trying to set invalid amount of money ({Money})");
            }
        }

        public void AddShopProduct(ShopProduct shopProduct)
        {
            ShopProducts.Add(shopProduct);
        }

        public void ChangePrice(ShopProduct shopProduct, float newPrice)
        {
            if (newPrice <= 0)
                throw new InvalidPriceException($"Trying to set invalid price ({newPrice}) for {shopProduct.Product.Name}");
            shopProduct.ShopPrice = newPrice;
        }

        public void PayForProduct(ShopProduct shopProduct)
        {
            if (Money < shopProduct.Quantity * shopProduct.ShopPrice)
                throw new ShopHasNotEnoughMoneyException($"Shop has {Money} money but needs {shopProduct.Quantity * shopProduct.ShopPrice}");
            Money -= shopProduct.Quantity * shopProduct.ShopPrice;
        }

        public void UpdateShopMoneyAfterSelling(ShopProduct shopProduct)
        {
            Money += shopProduct.ShopPrice * shopProduct.Quantity;
        }

        public ShopProduct FindProduct(string name)
        {
            ShopProduct shopProduct = ShopProducts.Find(shopProduct => shopProduct.Product.Name.Equals(name));
            return shopProduct;
        }

        public void RemoveProduct(ShopProduct shopProduct)
        {
            ShopProducts.Remove(shopProduct);
        }
    }
}